using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Azure.AI.OpenAI;
using Azure.AI.TextAnalytics;
using Azure;
using LuccasCorpVX.Models;
using Microsoft.AspNet.Identity;

namespace LuccasCorpVX.Controllers
{
    [Authorize]
    public class ComentariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComentariosController()
        {
            _context = new ApplicationDbContext();
        }

        
        public async Task<ActionResult> Index()
        {
            var userId = User.Identity.GetUserId();
            var fullName = await ApplicationDbContext.GetFullNameAsync(userId);

            ViewBag.FullName = fullName;

            List<Comentario> comentarios = new List<Comentario>();
            ViewBag.User = User.Identity.GetUserName();

            if (User.Identity.GetUserName() == "professor@ufop.edu.br")
            {

                comentarios = _context.Comentarios.ToList();

            } else {

                comentarios = _context.Comentarios.Where(c => c.Autor == User.Identity.Name).ToList();
                
            }

            comentarios = comentarios.FindAll(c => c.Ativo == true);
            comentarios = comentarios.FindAll(c => c.Insulto == false);

            comentarios.Sort((x, y) => DateTime.Compare(y.CreatedOn, x.CreatedOn));
            return View(comentarios);
        }


        public async Task<ActionResult> IndexInsultos()
        {
            var userId = User.Identity.GetUserId();
            var fullName = await ApplicationDbContext.GetFullNameAsync(userId);

            ViewBag.FullName = fullName;

            List<Comentario> comentarios = new List<Comentario>();

            if (User.Identity.GetUserName() == "professor@ufop.edu.br")
            {

                comentarios = _context.Comentarios.ToList();
                comentarios = comentarios.FindAll(c => c.Insulto == true);

            }
            else
            {
                return RedirectToAction("Index");
            }

            comentarios.Sort((x, y) => DateTime.Compare(y.CreatedOn, x.CreatedOn));
            return View(comentarios);

        }

        // GET: Comentarios/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            var userId = User.Identity.GetUserId();
            var fullName = await ApplicationDbContext.GetFullNameAsync(userId);

            ViewBag.FullName = fullName;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentario comentario = _context.Comentarios.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            return View(comentario);
        }

        // GET: Comentarios/Create
        public async Task<ActionResult> Create(int? id, string tipo)
        {
            var userId = User.Identity.GetUserId();
            var fullName = await ApplicationDbContext.GetFullNameAsync(userId);

            ViewBag.FullName = fullName;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string disciplinaString = string.Empty;
            string professorString = string.Empty;

            if (tipo.Equals("Disciplina"))
            {
                Disciplinas disciplina = _context.Disciplinas.Find(id);
                disciplinaString = disciplina.Nome;
                if (disciplina == null)
                {
                    return HttpNotFound();
                }
            }
            else
            {
                Professores professor = _context.Professores.Find(id.ToString());
                professorString = professor.FirstName + " " + professor.LastName;
                if (professor == null)
                {
                    return HttpNotFound();
                }
            }

            ViewBag.Id = id;
            ViewBag.Tipo = tipo;
            ViewBag.AutorName = await ApplicationDbContext.GetFullNameAsync(User.Identity.GetUserId());
            ViewBag.AutorID = User.Identity.GetUserId();
            ViewBag.AutorEmail = User.Identity.GetUserName();
            ViewBag.Disciplina = disciplinaString;
            ViewBag.Professor = professorString;

            Comentario comentario = new Comentario();
            comentario.CreatedOn = DateTime.Now;
            comentario.Ativo = true;
            comentario.Autor = User.Identity.GetUserName();

            return View("Create", comentario);
        }

        public static async Task<JsonResult> CheckForInsults(string message)
        {
            string endpoint = ConfigurationManager.AppSettings["OpenAIEndpoint"];
            string key = ConfigurationManager.AppSettings["OpenAIKey"];

            OpenAIClient client = new OpenAIClient(new Uri(endpoint), new AzureKeyCredential(key));

            var chatCompletionsOptions = new ChatCompletionsOptions()
            {
                DeploymentName = "gpt-35-turbo-16k",
                Messages =
                {
                    new ChatRequestSystemMessage("Você é um assistente de IA especializado em analisar reclamações e opiniões de usuários em um site. Sua principal tarefa é identificar insultos, preconceitos, julgamentos e análises subjetivas nos textos, além de detectar opiniões positivas. Algumas frases podem ser disfarçadas com linguajar formal. Insultos são termos ofensivos, pejorativos ou humilhantes, e podem ser implícitos em declarações subjetivas ou comparações entre pessoas. Preconceitos envolvem discriminação relacionada à cor de pele, etnia, raça, orientação sexual ou qualquer outra característica pessoal. Além disso, frases que implicam julgamentos sobre comportamentos sexuais ou de relacionamento de outra pessoa (principalmente quando negativos ou desrespeitosos) devem ser tratadas com cuidado. Exemplos de julgamento incluem comentários sobre comportamentos íntimos, como \"ficar com alguém\", \"beijar alguém\" ou fazer referência a qualquer interação sexual. Comentários sobre o comportamento social em geral (como \"fala muito devagar\" ou \"a matéria é chata\") serão considerados como opiniões e não como julgamentos. A IA também deve identificar expressões que possam sugerir moralidade ou comportamentos questionáveis relacionados ao comportamento sexual ou de relacionamento, como o uso de \"ficar com alguém\" em um contexto de rejeição, que pode ser visto como julgamento sobre a moralidade de uma pessoa. Sempre que um texto for enviado, sua resposta será um JSON com os seguintes campos: texto: O texto original enviado pelo usuário. tem_insultos: Um valor booleano (true ou false) que indica se o texto contém insultos. Se houver insultos, o valor será true; caso contrário, será false. tem_preconceitos: Um valor booleano (true ou false) que indica se o texto contém preconceitos (ex.: discriminação sobre cor de pele, etnia, raça ou orientação sexual). tem_julgamentos: Um valor booleano (true ou false) que indica se o texto contém julgamentos sobre comportamentos sexuais ou de relacionamento de outra pessoa (ex.: comentários sobre beijos, ficadas, relações sexuais, etc.). flags: Um array contendo os insultos, preconceitos ou julgamentos encontrados no texto, ou outras frases que impliquem julgamento de caráter ou comportamento. Caso haja julgamentos, sempre os mostre no array. Caso não haja insultos, preconceitos ou julgamentos, o array será vazio []."),

                    //new ChatRequestSystemMessage("Você é um assistente de IA especializado em analisar reclamações e opiniões de usuários em um site. Sua principal tarefa é identificar insultos, preconceitos, julgamentos e análises subjetivas nos textos, além de detectar opiniões positivas. Algumas frases podem ser disfarçadas com linguajar formal ou críticas indiretas, portanto, suas verificações devem ser muito sensíveis. Insultos são termos ofensivos, pejorativos ou humilhantes, e podem ser implícitos em declarações subjetivas ou comparações entre pessoas. Preconceitos envolvem discriminação relacionada à cor de pele, etnia, raça, orientação sexual ou qualquer outra característica pessoal. Além disso, frases que implicam julgamentos sobre comportamentos sexuais ou de relacionamento de outra pessoa (principalmente quando negativos ou desrespeitosos) devem ser tratadas com cuidado. Exemplos de julgamento incluem comentários sobre comportamentos íntimos, como \"ficar com alguém\", \"beijar alguém\" ou fazer referência a qualquer interação sexual. Comentários sobre o comportamento social em geral (como \"fala muito devagar\" ou \"a matéria é chata\") serão considerados como opiniões e não como julgamentos. A IA também deve identificar expressões que possam sugerir moralidade ou comportamentos questionáveis relacionados ao comportamento sexual ou de relacionamento, como o uso de \"ficar com alguém\" em um contexto de rejeição, que pode ser visto como julgamento sobre a moralidade de uma pessoa. Sempre que um texto for enviado, sua resposta será um JSON com os seguintes campos: texto: O texto original enviado pelo usuário. tem_insultos: Um valor booleano (true ou false) que indica se o texto contém insultos. Se houver insultos, o valor será true; caso contrário, será false. tem_preconceitos: Um valor booleano (true ou false) que indica se o texto contém preconceitos (ex.: discriminação sobre cor de pele, etnia, raça ou orientação sexual). tem_julgamentos: Um valor booleano (true ou false) que indica se o texto contém julgamentos sobre comportamentos sexuais ou de relacionamento de outra pessoa (ex.: comentários sobre beijos, ficadas, relações sexuais, etc.). flags: Um array contendo os insultos, preconceitos ou julgamentos encontrados no texto, ou outras frases que impliquem julgamento de caráter ou comportamento. Caso haja julgamentos, sempre os mostre no array. Caso não haja insultos, preconceitos ou julgamentos, o array será vazio []."),

                    //new ChatRequestSystemMessage("Você é um assistente de IA especializado em analisar textos de usuários em um site, com foco em identificar insultos, preconceitos, julgamentos e análises subjetivas. Insultos incluem termos ofensivos, pejorativos ou humilhantes, bem como declarações implícitas ou comparações negativas. Preconceitos envolvem discriminação por cor de pele, etnia, raça, orientação sexual ou características pessoais. Frases que fazem julgamentos negativos sobre atitudes ou comportamentos de outra pessoa devem ser tratadas como insultos, mesmo que não haja palavras ofensivas. Expressões como “ficar com ele” no contexto de rejeição também devem ser consideradas como possíveis julgamentos. Sua resposta será um JSON com os seguintes campos: texto: O texto original enviado. tem_insultos: Indica se o texto contém insultos (true ou false). tem_preconceitos: Indica se o texto contém preconceitos (true ou false). tem_julgamentos: Indica se o texto contém julgamentos (true ou false). flags: Array com insultos, preconceitos ou julgamentos encontrados. Se não houver, será vazio []."),
                    new ChatRequestUserMessage(message),
                },
                MaxTokens = 4000
            };

            try
            {
                // Obter resposta completa
                var response = await client.GetChatCompletionsAsync(chatCompletionsOptions);

                // Concatene as mensagens do conteúdo
                string responseString = response.Value.Choices[0].Message.Content;

                // Validar se é um JSON válido
                var parsedJson = JsonDocument.Parse(responseString);

                // Retornar o JSON completo como está
                return new JsonResult
                {
                    Data = JsonDocument.Parse(responseString).RootElement,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (JsonException ex)
            {
                // Em caso de erro no parsing, retorne erro no JSON
                return new JsonResult
                {
                    Data = new
                    {
                        error = "Erro ao processar a resposta do modelo.",
                        details = ex.Message
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception ex)
            {
                // Qualquer outro erro
                return new JsonResult
                {
                    Data = new
                    {
                        error = "Erro inesperado.",
                        details = ex.Message
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        public static async Task<ContactMessage> SentimentAnalysis(string message)
        {
            string languageKey = ConfigurationManager.AppSettings["LanguageKey"];
            string languageEndpoint = ConfigurationManager.AppSettings["LanguageEndpoint"];

            AzureKeyCredential credentials = new AzureKeyCredential(languageKey);
            Uri endpoint = new Uri(languageEndpoint);

            var client = new TextAnalyticsClient(endpoint, credentials);

            var documents = new List<string>
            {
                message
            };

            AnalyzeSentimentResultCollection reviews = await client.AnalyzeSentimentBatchAsync(documents, options: new AnalyzeSentimentOptions()
            {
                IncludeOpinionMining = true
            });

            ContactMessage contactMessage = new ContactMessage();

            foreach (AnalyzeSentimentResult review in reviews)
            {
                contactMessage.Sentimento = review.DocumentSentiment.Sentiment.ToString();
                contactMessage.Positivo = Math.Round(review.DocumentSentiment.ConfidenceScores.Positive, 2);
                contactMessage.Negativo = Math.Round(review.DocumentSentiment.ConfidenceScores.Negative, 2);
                contactMessage.Neutro = Math.Round(review.DocumentSentiment.ConfidenceScores.Neutral, 2);

            }

            try
            {
                return contactMessage;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Comentario comentario)
        {
            JsonResult result = await CheckForInsults(comentario.Descricao);

            // Inspecione o resultado do JSON
            //dynamic jsonData = result.Data;
            //var test = result.Data;
            bool flagInsulto = false;
            bool flagPreconceito = false;
            //bool flagJulgamento = false;

            if (result.Data is JsonElement jsonElement1 && jsonElement1.TryGetProperty("tem_insultos", out JsonElement temInsultosElement) && temInsultosElement.GetBoolean())
            {
                flagInsulto = true;
            }

            if (result.Data is JsonElement jsonElement2 && jsonElement2.TryGetProperty("tem_preconceitos", out JsonElement temPreconceitosElement) && temPreconceitosElement.GetBoolean())
            {
                flagPreconceito = true;
            }

            var dataString = result.Data.ToString();

            ContactMessage aux = null;

            if (flagInsulto || flagPreconceito)
            {
                aux = await SentimentAnalysis(comentario.Descricao);
                if (aux != null)
                {
                    comentario.Sentimento = aux.Sentimento;
                    comentario.Positivo = aux.Positivo;
                    comentario.Negativo = aux.Negativo;
                    comentario.Neutro = aux.Neutro;
                }

                comentario.Insulto = true;
                _context.Comentarios.Add(comentario);
                _context.SaveChanges();

                ViewBag.Message = "A mensagem contém conteúdo impróprio, incluindo insultos, preconceitos ou julgamentos.";
                ViewBag.Texto = dataString;
                return View("ConteudoImproprio");
            }
            else
            {
                aux = await SentimentAnalysis(comentario.Descricao);
                if (aux != null)
                {
                    comentario.Sentimento = aux.Sentimento;
                    comentario.Positivo = aux.Positivo;
                    comentario.Negativo = aux.Negativo;
                    comentario.Neutro = aux.Neutro;
                }
            }

            comentario.Insulto = false;
            _context.Comentarios.Add(comentario);
            _context.SaveChanges();

            await UpdateMetrics(comentario);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task UpdateMetrics(Comentario comentario)
        {
            if (comentario.DisciplinaNome != null)
            {
                var disciplina = _context.Disciplinas.FirstOrDefault(d => d.Codigo == comentario.Disciplina);
                if (disciplina != null)
                {
                    disciplina.TotalComentarios++; // Incrementa antes de calcular a média
                    if (disciplina.TotalComentarios > 0) // Evita divisão por zero
                    {
                        disciplina.Media = ((disciplina.Media * (disciplina.TotalComentarios - 1)) + comentario.Positivo) / disciplina.TotalComentarios;
                    }
                    else
                    {
                        disciplina.Media = comentario.Positivo;
                    }

                    disciplina.AvaliacaoGeral = await _context.GetAvaliacaoGeralDisciplina(comentario.Disciplina);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                var professor = _context.Professores.FirstOrDefault(p => p.Id == comentario.Professor.ToString());
                if (professor != null)
                {
                    professor.TotalComentarios++; // Incrementa antes de calcular a média
                    if (professor.TotalComentarios > 0) // Evita divisão por zero
                    {
                        professor.Media = ((professor.Media * (professor.TotalComentarios - 1)) + comentario.Positivo) / professor.TotalComentarios;
                    }
                    else
                    {
                        professor.Media = comentario.Positivo;
                    }

                    professor.AvaliacaoGeral = await _context.GetAvaliacaoGeralProfessor(comentario.Professor);
                    await _context.SaveChangesAsync();
                }
            }
        }

        // GET: Comentarios/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {

            var userId = User.Identity.GetUserId();
            var fullName = await ApplicationDbContext.GetFullNameAsync(userId);

            ViewBag.FullName = fullName;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentario comentario = _context.Comentarios.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            return View(comentario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var comentario = _context.Comentarios.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }

            // Primeiro, remova o comentário
            _context.Comentarios.Remove(comentario);
            await _context.SaveChangesAsync(); // Salve a remoção primeiro

            // Agora, corrija o valor de TotalComentarios
            if (comentario.DisciplinaNome != null)
            {
                Disciplinas disciplinas = _context.Disciplinas.FirstOrDefault(d => d.Codigo == comentario.Disciplina);
                if (disciplinas != null && disciplinas.TotalComentarios > 0)
                {
                    // Decrementa o total de comentários da disciplina
                    disciplinas.TotalComentarios--;

                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                Professores professores = _context.Professores.FirstOrDefault(p => p.Id == comentario.Professor.ToString());
                if (professores != null && professores.TotalComentarios > 0)
                {
                    // Decrementa o total de comentários do professor
                    professores.TotalComentarios--;

                    await _context.SaveChangesAsync();
                }
            }

            // Atualiza as métricas
            await UpdateMetrics(comentario); // Chama a função que já atualiza a média e avaliação geral

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
