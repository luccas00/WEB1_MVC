function formatarTelefone(input) {
    let telefone = input.value.replace(/\D/g, ''); // Remove todos os caracteres não numéricos
    if (telefone.length > 10) {
        telefone = telefone.substring(0, 11); // Limita o número a 11 caracteres
    }
    if (telefone.length > 6) {
        telefone = telefone.replace(/(\d{2})(\d{1})(\d{4})(\d{4})/, "($1) $2 $3-$4");
    } else if (telefone.length > 2) {
        telefone = telefone.replace(/(\d{2})(\d{1})(\d{4})/, "($1) $2 $3-");
    } else if (telefone.length > 0) {
        telefone = telefone.replace(/(\d{2})(\d{1})/, "($1) $2 ");
    }
    input.value = telefone;
}
