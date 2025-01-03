using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LuccasCorp.Models
{
    // É possível adicionar dados do perfil do usuário adicionando mais propriedades na sua classe ApplicationUser, visite https://go.microsoft.com/fwlink/?LinkID=317594 para obter mais informações.
    public class ApplicationUser : IdentityUser
    {
        public string NumeroMatricula { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Observe que a authenticationType deve corresponder a uma definida em CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Adicionar declarações do usuário personalizadas aqui
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


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
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
    }
}