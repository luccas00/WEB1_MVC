function validarEmail(input) {
    var email = input.value;
    var regex = /^[a-zA-Z0-9._%+-]+@aluno.ufop.edu.br$/;

    // Se o e-mail for inválido, exibe uma mensagem de erro
    if (!regex.test(email)) {
        input.setCustomValidity("Por favor, insira um e-mail válido com o domínio @aluno.edu.ufop.br.");
    } else {
        input.setCustomValidity("");  // Limpa a mensagem de erro caso o e-mail seja válido
    }

    // Aqui estamos forçando a validação do formulário novamente
    var form = input.form;
    if (form) {
        form.classList.add('was-validated'); // Garante que as validações sejam executadas no formulário
    }
}