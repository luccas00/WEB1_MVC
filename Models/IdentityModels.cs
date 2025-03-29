using LuccasCorpVX.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LuccasCorpVX.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string NumeroMatricula { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Tipo { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now; // Data e hora de criação

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DatabaseConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<ContactMessage> ContactMessages { get; set; } // Adicionando o DbSet para as mensagens de contato
        public DbSet<Comentario> Comentarios { get; set; } // Adicionando o DbSet para os Comentarios
        public DbSet<Alunos> Alunos { get; set; } // Adicionando o DbSet para os Comentarios
        public DbSet<Professores> Professores { get; set; } // Adicionando o DbSet para os Comentarios

        //static ApplicationDbContext()
        //{
        //    // Set the database intializer which is run once during application start
        //    // This seeds the database with admin user credentials and admin role
        //    Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        //}

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public async Task<double> GetMediaAsync(string userId)
        {
            // Supondo que você tenha uma tabela associada ou um campo na tabela de usuários
            var user = await this.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var professor = await this.Professores.FirstOrDefaultAsync(p => p.Email == user.Email);
            return professor.Media; // Retorna a média do Professor
        }

        public async Task<string> GetAvaliacaoGeralAsync(string userId)
        {
            // Supondo que você tenha uma tabela associada ou um campo na tabela de usuários
            var user = await this.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var professor = await this.Professores.FirstOrDefaultAsync(p => p.Email == user.Email);
            return professor?.AvaliacaoGeral; // Retorna a avaliação geral do Professor
        }

        public async Task<string> GetDepartamentoAsync(string userId)
        {
            // Supondo que você tenha uma tabela associada ou um campo na tabela de usuários
            var user = await this.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var professor = await this.Professores.FirstOrDefaultAsync(p => p.Email == user.Email);
            return professor?.Departamento; // Retorna o departamento do Professor
        }

        public async Task<string> GetCampusAsync(string userId)
        {
            // Supondo que você tenha uma tabela associada ou um campo na tabela de usuários
            var user = await this.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var professor = await this.Professores.FirstOrDefaultAsync(p => p.Email == user.Email);
            return professor?.Campus; // Retorna o campus do Professor
        }

        public async Task<string> GetTipoAsync(string userId)
        {
            // Supondo que você tenha uma tabela associada ou um campo na tabela de usuários
            var user = await this.Users.FirstOrDefaultAsync(u => u.Id == userId);
            return user?.Tipo; // Retorna o tipo do Usuario
        }

        public async Task<string> GetNumeroMatriculaAsync(string userId)
        {
            // Supondo que você tenha uma tabela associada ou um campo na tabela de usuários
            var user = await this.Users.FirstOrDefaultAsync(u => u.Id == userId);
            return user?.NumeroMatricula; // Retorna o número de matrícula ou null
        }

        public async Task<string> GetFirstNameAsync(string userId)
        {
            // Supondo que você tenha uma tabela associada ou um campo na tabela de usuários
            var user = await this.Users.FirstOrDefaultAsync(u => u.Id == userId);
            return user?.FirstName; // Retorna o número de matrícula ou null
        }

        public async Task<string> GetLastNameAsync(string userId)
        {
            // Supondo que você tenha uma tabela associada ou um campo na tabela de usuários
            var user = await this.Users.FirstOrDefaultAsync(u => u.Id == userId);
            return user?.LastName; // Retorna o número de matrícula ou null
        }

        public static async Task<string> GetFullNameAsync(string userId)
        {
            // Supondo que você tenha um contexto do banco de dados como ApplicationDbContext
            using (var context = new ApplicationDbContext())
            {
                // Obtém o usuário com o ID especificado
                var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

                // Verifica se o usuário foi encontrado e retorna o nome completo
                //if (user != null)
                //{
                //    return $"{user.FirstName} {user.LastName}"; // Retorna o nome completo
                //}

                if (user != null)
                {
                    // Formata a primeira letra de cada nome para maiúscula
                    var cultureInfo = new CultureInfo("pt-BR"); // Define a cultura para português (Brasil)
                    var firstName = cultureInfo.TextInfo.ToTitleCase(user.FirstName.ToLower());
                    var lastName = cultureInfo.TextInfo.ToTitleCase(user.LastName.ToLower());

                    // Retorna o nome completo com a formatação correta
                    return $"{firstName} {lastName}";
                }

                return null; // Caso o usuário não seja encontrado
            }

        }

        public async Task<string> GetAvaliacaoGeralDisciplina(int Codigo)
        {
            var comentarios = await this.Comentarios.Where(c => c.Disciplina == Codigo).ToListAsync();
            if (!comentarios.Any()) return "Neutro";

            var sentimentoFrequencia = comentarios
                .GroupBy(c => c.Sentimento)
                .Select(g => new { Sentimento = g.Key, Quantidade = g.Count() })
                .OrderByDescending(g => g.Quantidade)
                .FirstOrDefault();

            return sentimentoFrequencia?.Sentimento ?? "Neutro";
        }

        public async Task<string> GetAvaliacaoGeralProfessor(int id)
        {
            var comentarios = await this.Comentarios.Where(c => c.Professor == id).ToListAsync();
            if (!comentarios.Any()) return "Neutro";

            var sentimentoFrequencia = comentarios
                .GroupBy(c => c.Sentimento)
                .Select(g => new { Sentimento = g.Key, Quantidade = g.Count() })
                .OrderByDescending(g => g.Quantidade)
                .FirstOrDefault();

            return sentimentoFrequencia?.Sentimento ?? "Neutro";
        }

        public System.Data.Entity.DbSet<LuccasCorpVX.Models.Disciplinas> Disciplinas { get; set; }
    }
}