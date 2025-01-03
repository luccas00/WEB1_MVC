using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using LuccasCorp.Models;
using SendGrid.Helpers.Mail;
using System.Diagnostics;
using System.Net;
using SendGrid;
using System.Configuration;
using Twilio.Rest.Verify.V2.Service;
using Twilio;

namespace LuccasCorp
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            // Conecte seu serviço de email aqui para enviar um email.
            await configSendGridasync(message);
        }

        private async Task configSendGridasync(IdentityMessage message)
        {
            // Obtém a chave da API do SendGrid a partir do arquivo de configuração (Web.config ou App.config)
            var apiKey = ConfigurationManager.AppSettings["SendGridApiKey"];
            if (string.IsNullOrEmpty(apiKey))
            {
                Trace.TraceError("SendGrid API Key is not configured.");
                throw new ConfigurationErrorsException("Missing SendGrid API Key.");
            }

            // Configura o cliente SendGrid
            var client = new SendGridClient(apiKey);

            // Cria o e-mail
            var from = new EmailAddress("atendimento@11h1gb.onmicrosoft.com", "Atendimento Luccas Corp");
            var to = new EmailAddress(message.Destination);
            var subject = message.Subject;
            var plainTextContent = message.Body;
            var htmlContent = $"<p>{message.Body}</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            // Envia o e-mail e verifica a resposta
            var response = await client.SendEmailAsync(msg);
            if (response.StatusCode != System.Net.HttpStatusCode.OK &&
                response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                Trace.TraceError($"Failed to send email. Response code: {response.StatusCode}");
            }
        }

    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Exemplo de integração com Twilio
            var accountSid = ConfigurationManager.AppSettings["TwilioAccountSid"];
            var authToken = ConfigurationManager.AppSettings["TwilioAuthToken"];
            var serviceSid = ConfigurationManager.AppSettings["TwilioServiceSid"];

            TwilioClient.Init(accountSid, authToken);

            // Solicitar o envio do código de verificação via Twilio
            var verification = VerificationResource.Create(
                to: message.Destination, // Número do destinatário
                channel: "sms",
                pathServiceSid: serviceSid
            );

            return Task.CompletedTask;
        }

    }

    // Configure o gerenciador de usuários do aplicativo usado nesse aplicativo. O UserManager está definido no ASP.NET Identity e é usado pelo aplicativo.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configurar a lógica de validação para nomes de usuário
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configurar a lógica de validação para senhas
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = false,
            };

            // Configurar padrões de bloqueio de usuário
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Registre provedores de autenticação de dois fatores. Este aplicativo usa Telefone e Emails como uma etapa de recebimento de código para verificar o usuário
            // Você pode gravar seu próprio provedor e conectá-lo aqui.
            manager.RegisterTwoFactorProvider("Código de telefone", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Seu código de segurança é {0}"
            });
            manager.RegisterTwoFactorProvider("Código de e-mail", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Código de segurança",
                BodyFormat = "Seu código de segurança é {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = 
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure o gerenciador de login do aplicativo que é usado neste aplicativo.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
