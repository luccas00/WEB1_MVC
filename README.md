﻿# **CSI606-24.2 - Proposta de Trabalho Final**

## *Discente: Luccas Vinicius P. A. Santos Carneiro*

<!-- Descrever um resumo sobre o trabalho. -->

### Resumo

   Este projeto consiste na implementação de um site para o gerenciamento de opiniões e comentários sobre professores e disciplinas do ICEA (Instituto de Ciências Exatas e Aplicadas) da UFOP. A plataforma permitirá que os alunos registrem feedbacks sobre suas experiências acadêmicas, garantindo um ambiente de avaliações construtivas. Para manter a qualidade e a ética dos comentários, o sistema utilizará Inteligência Artificial para analisar o conteúdo, identificando possíveis insultos, preconceitos ou julgamentos subjetivos. O acesso será restrito a alunos da UFOP, exigindo cadastro e confirmação via e-mail institucional.

<!-- Apresentar o tema. -->
### 1. Tema

  Gerenciamento de Opiniões e Comentários sobre Professores e Disciplinas do ICEA

<!-- Descrever e limitar o escopo da aplicação. -->
### 2. Escopo

  - Cadastro e login de usuários utilizando e-mail institucional da UFOP.
  - Publicação de comentários sobre professores e disciplinas do ICEA.
  - Sistema de análise de sentimentos e filtragem de conteúdo inadequado usando IA.
  - Detecção automática de insultos, preconceitos e julgamentos subjetivos.
  - Exibição de comentários aprovados de forma organizada e acessível.
  - Moderação automatizada para evitar discursos ofensivos ou discriminatórios.

     - Funcionalidades Implementadas
		- Todo o Escopo foi implementado com sucesso.
   

<!-- Apresentar restrições de funcionalidades e de escopo. -->
### 3. Restrições

  - Comentários anônimos ou de usuários sem e-mail institucional da UFOP.
  - Avaliações quantitativas, como notas ou estrelas, focando exclusivamente em feedbacks textuais.
  - Edições de comentários após a publicação, permitindo apenas exclusão e repostagem.
  - Moderação manual, sendo a filtragem realizada exclusivamente por IA.
  - Suporte a outras unidades da UFOP além do ICEA.

<!-- Construir alguns protótipos para a aplicação, disponibilizá-los no Github e descrever o que foi considerado. //-->

### 4. Links

  Links de acesso

  - [Site Final - MVC](https://luccascorpvx.azurewebsites.net/)
  - [Protótipo Static Web App](https://brave-stone-0c5a98610.4.azurestaticapps.net/)
  
  Apresentação YouTube

  - [Apresentação](https://www.youtube.com/watch?v=1mgI4znS15g)


### 5. Tratamento de Erros

Possível Erro: Banco de Dados Offline

O site utiliza um banco de dados gratuito no Azure, o que significa que há uma configuração nativa que pausa automaticamente o banco de dados caso não haja acessos por um longo período. Se você visualizar a mensagem abaixo ao tentar acessar o site, aguarde de 30 a 60 segundos e atualize a página para tentar novamente. O banco de dados será iniciado automaticamente.

﻿![Diagrama de Classe DB Diagram](Images/ErroBDOffline.png)
 
### 6. Código Fonte

  Código fonte dos protótipos desenvolvidos no modelo MVC e em um aplicativo web estático.

  - [Site Final MVC](https://github.com/luccas00/WEB1_MVC)
  - [Protótipo Static Web App](https://github.com/luccas00/WEB1_StaticWebApp)
	

### 7. Referências

  - [Introdução ao ASP.NET MVC](https://learn.microsoft.com/pt-br/aspnet/mvc/overview/getting-started/introduction/getting-started)
  - [Confirmação da conta e recuperação de senha com ASP.NET Identity (C#)](https://learn.microsoft.com/pt-br/aspnet/identity/overview/features-api/account-confirmation-and-password-recovery-with-aspnet-identity)
  - ~~[Criar um aplicativo Web seguro do ASP.NET MVC](https://learn.microsoft.com/pt-br/aspnet/mvc/overview/security/create-an-aspnet-mvc-5-web-app-with-email-confirmation-and-password-reset)~~
  
  - [Criar seu primeiro aplicativo Web estático](https://learn.microsoft.com/pt-br/azure/static-web-apps/get-started-portal?tabs=vanilla-javascript&pivots=github)
  
  - [HTML Tutorial](https://www.w3schools.com/html/default.asp)
  - [CSS Tutorial](https://www.w3schools.com/css/default.asp)

### 8. Diagrama de Classe

﻿![Diagrama de Classe DB Diagram](Images/WebI_MVC_LuccasCorpVX.svg)



