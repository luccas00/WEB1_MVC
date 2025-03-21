using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LuccasCorpVX.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Código")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Lembrar deste navegador?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Lembrar-me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@(aluno.ufop.edu.br|ufop.edu.br)$", ErrorMessage = "O e-mail deve ser do domínio @aluno.ufop.edu.br ou @ufop.edu.br")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        // Adicionando a propriedade PhoneNumber
        [Required]
        [Phone]
        [Display(Name = "Número de telefone")]
        [RegularExpression(@"^\(\d{2}\) 9 \d{4}-\d{4}$", ErrorMessage = "Número de telefone inválido. O formato esperado é (31) 9 9988-7766.")]
        public string PhoneNumber { get; set; }

        // Adicionando a propriedade NumeroMatricula
        [Required]
        [RegularExpression(@"^\d{2}\.\d{1}\.\d{4}$", ErrorMessage = "O número de matrícula deve estar no formato correto (ex: 11.2.3344).")]
        [Display(Name = "Número da Matrícula")]
        public string NumeroMatricula { get; set; }

        // Adicionando o campo de Nome
        [Required]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        [Display(Name = "Nome")]
        public string FirstName { get; set; }

        // Adicionando o campo de Sobrenome
        [Required]
        [StringLength(100, ErrorMessage = "O sobrenome deve ter no máximo 100 caracteres.")]
        [Display(Name = "Sobrenome")]
        public string LastName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O {0} deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("Password", ErrorMessage = "A senha e a senha de confirmação não coincidem.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O {0} deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("Password", ErrorMessage = "A senha e a senha de confirmação não coincidem.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}