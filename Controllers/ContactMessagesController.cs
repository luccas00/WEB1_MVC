using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Web;
using System.Web.Mvc;
using Azure.AI.OpenAI;
using Azure;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Configuration;
using Newtonsoft.Json.Linq;
using Azure.AI.TextAnalytics;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using LuccasCorpVX.Models;
using Microsoft.AspNet.Identity;

namespace LuccasCorpVX.Controllers
{
    public class ContactMessagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public async Task<ActionResult> Index()
        {
            var userId = User.Identity.GetUserId();
            var fullName = await ApplicationDbContext.GetFullNameAsync(userId);

            ViewBag.FullName = fullName;

            return View();
        }

        public async Task<ActionResult> Create()
        {
            var userId = User.Identity.GetUserId();
            var fullName = await ApplicationDbContext.GetFullNameAsync(userId);

            ViewBag.FullName = fullName;

            return View();
        }

        public async Task<ActionResult> ConteudoImproprio()
        {
            var userId = User.Identity.GetUserId();
            var fullName = await ApplicationDbContext.GetFullNameAsync(userId);

            ViewBag.FullName = fullName;

            return View();
        }

        // POST: ContactMessages/Create
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Email,Message,DateSent")] ContactMessage contactMessage)
        {
            if (ModelState.IsValid)
            {
                JsonResult result = await CheckForInsults(contactMessage.Message);

                // Inspecione o resultado do JSON
                //dynamic jsonData = result.Data;
                //var test = result.Data;
                bool flagInsulto = false;
                bool flagPreconceito = false;
                bool flagJulgamento = false;

                if (result.Data is JsonElement jsonElement1 && jsonElement1.TryGetProperty("tem_insultos", out JsonElement temInsultosElement) && temInsultosElement.GetBoolean())
                {
                    flagInsulto = true;
                }

                if (result.Data is JsonElement jsonElement2 && jsonElement2.TryGetProperty("tem_preconceitos", out JsonElement temPreconceitosElement) && temPreconceitosElement.GetBoolean())
                {
                    flagPreconceito = true;
                }

                if (result.Data is JsonElement jsonElement3 && jsonElement3.TryGetProperty("tem_julgamentos", out JsonElement temJulgamentosElement) && temJulgamentosElement.GetBoolean())
                {
                    flagJulgamento = true;
                }

                if (result.Data is JsonElement jsonElement4 && jsonElement4.TryGetProperty("flags", out JsonElement temFlagsElement) && temFlagsElement.ValueKind == JsonValueKind.Array && temFlagsElement.GetArrayLength() <= 0)
                {
                    flagJulgamento = false;
                }


                var dataString = result.Data.ToString();
                // var data = JsonSerializer.Deserialize<Dictionary<string, object>>(dataString);
                //var temInsultos = data["tem_insultos"];
                //var insultos = data["flags"];
                //var texto = data["texto"];

                ContactMessage aux = null;

                if (flagInsulto || flagPreconceito || flagJulgamento)
                {
                    aux = await SentimentAnalysis(contactMessage.Message);
                    if (aux != null)
                    {
                        contactMessage.Sentimento = aux.Sentimento;
                        contactMessage.Positivo = aux.Positivo;
                        contactMessage.Negativo = aux.Negativo;
                        contactMessage.Neutro = aux.Neutro;
                    }

                    contactMessage.CreatedOn = DateTime.Now;
                    contactMessage.Insulto = true;
                    db.ContactMessages.Add(contactMessage);
                    db.SaveChanges();

                    ViewBag.Message = "A mensagem contém conteúdo impróprio, incluindo insultos, preconceitos ou julgamentos.";
                    ViewBag.Texto = dataString;
                    return View("ConteudoImproprio");
                } 
                else
                {
                    aux = await SentimentAnalysis(contactMessage.Message);
                    if (aux != null)
                    {
                        contactMessage.Sentimento = aux.Sentimento;
                        contactMessage.Positivo = aux.Positivo;
                        contactMessage.Negativo = aux.Negativo;
                        contactMessage.Neutro = aux.Neutro;
                    }
                }
                
                contactMessage.CreatedOn = DateTime.Now;
                contactMessage.Insulto = false;
                db.ContactMessages.Add(contactMessage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contactMessage);
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
                    new ChatRequestSystemMessage("Você é um assistente de IA especializado em analisar reclamações e opiniões de usuários em um site. Sua principal tarefa é identificar insultos, preconceitos, julgamentos e análises subjetivas nos textos, além de detectar opiniões positivas. Algumas frases podem ser disfarçadas com linguajar formal ou críticas indiretas, portanto, suas verificações devem ser muito sensíveis. Insultos são termos ofensivos, pejorativos ou humilhantes, e podem ser implícitos em declarações subjetivas ou comparações entre pessoas. Preconceitos envolvem discriminação relacionada à cor de pele, etnia, raça, orientação sexual ou qualquer outra característica pessoal. Além disso, frases que implicam julgamentos sobre comportamentos sexuais ou de relacionamento de outra pessoa (principalmente quando negativos ou desrespeitosos) devem ser tratadas com cuidado. Exemplos de julgamento incluem comentários sobre comportamentos íntimos, como \"ficar com alguém\", \"beijar alguém\" ou fazer referência a qualquer interação sexual. Comentários sobre o comportamento social em geral (como \"fala muito devagar\" ou \"a matéria é chata\") serão considerados como opiniões e não como julgamentos. A IA também deve identificar expressões que possam sugerir moralidade ou comportamentos questionáveis relacionados ao comportamento sexual ou de relacionamento, como o uso de \"ficar com alguém\" em um contexto de rejeição, que pode ser visto como julgamento sobre a moralidade de uma pessoa. Sempre que um texto for enviado, sua resposta será um JSON com os seguintes campos: texto: O texto original enviado pelo usuário. tem_insultos: Um valor booleano (true ou false) que indica se o texto contém insultos. Se houver insultos, o valor será true; caso contrário, será false. tem_preconceitos: Um valor booleano (true ou false) que indica se o texto contém preconceitos (ex.: discriminação sobre cor de pele, etnia, raça ou orientação sexual). tem_julgamentos: Um valor booleano (true ou false) que indica se o texto contém julgamentos sobre comportamentos sexuais ou de relacionamento de outra pessoa (ex.: comentários sobre beijos, ficadas, relações sexuais, etc.). flags: Um array contendo os insultos, preconceitos ou julgamentos encontrados no texto, ou outras frases que impliquem julgamento de caráter ou comportamento. Caso haja julgamentos, sempre os mostre no array. Caso não haja insultos, preconceitos ou julgamentos, o array será vazio []."),

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


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
