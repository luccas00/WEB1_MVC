function formatarMatricula(input) {
    // Remove qualquer caractere que não seja número
    var valor = input.value.replace(/\D/g, "");

    // Aplica a formatação no formato 20.1.8015
    if (valor.length > 4) {
        valor = valor.substring(0, 2) + '.' + valor.substring(2, 3) + '.' + valor.substring(3, 7);
    } else if (valor.length > 2) {
        valor = valor.substring(0, 2) + '.' + valor.substring(2);
    }

    // Atualiza o valor no campo de entrada com a formatação
    input.value = valor;
}