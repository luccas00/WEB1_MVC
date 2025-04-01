
INSERT INTO Disciplinas (CodigoUFOP, Nome, Campus, Departamento, Ativo, Media, AvaliacaoGeral)
VALUES 
('CEA036', 'FUNDAMENTOS DE GEOMETRIA ANALITICA E ALGEBRA LINEAR', 'ICEA', 'DECEA', 1, 0, ''),
('CEA423', 'FUNDAMENTOS DE CALCULO', 'ICEA', 'DECEA', 1, 0, ''),
('CSI101', 'PROGRAMACAO DE COMPUTADORES I', 'ICEA', 'DECSI', 1, 0, ''),
('CSI601', 'FUNDAMENTOS DE SISTEMAS DE INFORMACAO', 'ICEA', 'DECSI', 1, 0, ''),
('CSI901', 'INFORMATICA E SOCIEDADE', 'ICEA', 'DECSI', 1, 0, ''),
('CSI902', 'METODOLOGIA DE PESQUISA', 'ICEA', 'DECSI', 1, 0, ''),
('CSI011', 'MATEMATICA DISCRETA', 'ICEA', 'DECSI', 1, 0, ''),
('CSI102', 'PROGRAMACAO DE COMPUTADORES II', 'ICEA', 'DECSI', 1, 0, ''),
('CSI103', 'ALGORITMOS E ESTRUTURAS DE DADOS I', 'ICEA', 'DECSI', 1, 0, ''),
('CSI801', 'GESTAO DA INFORMACAO', 'ICEA', 'DECSI', 1, 0, ''),
('ENP144', 'TEORIA GERAL DA ADMINISTRACAO', 'ICEA', 'DEENP', 1, 0, ''),
('CEA307', 'ESTATISTICA E PROBABILIDADE', 'ICEA', 'DECEA', 1, 0, ''),
('CSI104', 'ALGORITMOS E ESTRUTURAS DE DADOS II', 'ICEA', 'DECSI', 1, 0, ''),
('CSI105', 'ALGORITMOS E ESTRUTURAS DE DADOS III', 'ICEA', 'DECSI', 1, 0, ''),
('CSI202', 'ORGANIZACAO E ARQUITETURA DE COMPUTADORES I', 'ICEA', 'DECSI', 1, 0, ''),
('ENP473', 'COMPORTAMENTO ORGANIZACIONAL', 'ICEA', 'DEENP', 1, 0, ''),
('CSI204', 'SISTEMAS OPERACIONAIS', 'ICEA', 'DECSI', 1, 0, ''),
('CSI403', 'ENGENHARIA DE SOFTWARE I', 'ICEA', 'DECSI', 1, 0, ''),
('CSI602', 'BANCO DE DADOS I', 'ICEA', 'DECSI', 1, 0, ''),
('ENP012', 'PROGRAMACAO LINEAR E INTEIRA', 'ICEA', 'DEENP', 1, 0, ''),
('ENP150', 'ECONOMIA', 'ICEA', 'DEENP', 1, 0, ''),
('CSI106', 'FUNDAMENTOS TEORICOS DA COMPUTACAO', 'ICEA', 'DECSI', 1, 0, ''),
('CSI301', 'REDES DE COMPUTADORES I', 'ICEA', 'DECSI', 1, 0, ''),
('CSI404', 'ENGENHARIA DE SOFTWARE II', 'ICEA', 'DECSI', 1, 0, ''),
('CSI603', 'BANCO DE DADOS II', 'ICEA', 'DECSI', 1, 0, ''),
('CSI701', 'INTELIGENCIA ARTIFICIAL', 'ICEA', 'DECSI', 1, 0, ''),
('CSI107', 'LINGUAGENS DE PROGRAMACAO', 'ICEA', 'DECSI', 1, 0, ''),
('CSI302', 'SISTEMAS DISTRIBUIDOS', 'ICEA', 'DECSI', 1, 0, ''),
('CSI405', 'GERENCIA DE PROJETOS DE SOFTWARE', 'ICEA', 'DECSI', 1, 0, ''),
('CSI504', 'INTERACAO HUMANO-COMPUTADOR', 'ICEA', 'DECSI', 1, 0, ''),
('CSI606', 'SISTEMAS WEB I', 'ICEA', 'DECSI', 1, 0, ''),
('CSI802', 'GESTAO DA TECNOLOGIA DA INFORMACAO', 'ICEA', 'DECSI', 1, 0, ''),
('CSI998', 'TRABALHO DE CONCLUSAO DE CURSO I', 'ICEA', 'DECSI', 1, 0, ''),
('ENP026', 'ADMINISTRACAO DE RECURSOS HUMANOS', 'ICEA', 'DEENP', 1, 0, ''),
('ENP493', 'EMPREENDEDORISMO', 'ICEA', 'DEENP', 1, 0, ''),
('CSI303', 'SEGURANCA E AUDITORIA DE SISTEMAS', 'ICEA', 'DECSI', 1, 0, ''),
('CSI605', 'SISTEMAS DE APOIO A DECISAO', 'ICEA', 'DECSI', 1, 0, ''),
('CSI999', 'TRABALHO DE CONCLUSAO DE CURSO II', 'ICEA', 'DECSI', 1, 0, '');

INSERT INTO Professores (Id, Email, FirstName, LastName, Campus, Departamento, CreatedOn, Ativo, Media, AvaliacaoGeral)  
VALUES  
('1', 'alexandre.sousa@ufop.edu.br', 'Alexandre Magno', 'de Souza', 'UFOP', 'DECSI', GETDATE(), 1, 0, ''),
('2', 'brhott@ufop.edu.br', 'Bruno', 'Cerqueira Hott', 'UFOP', 'DECSI', GETDATE(), 1, 0, ''),
('3', 'bruno.ps@ufop.edu.br', 'Bruno', 'Pereira dos Santos', 'UFOP', 'DECSI', GETDATE(), 1, 0, ''),
('4', 'bruno@ufop.edu.br', 'Bruno', 'Rabello Monteiro', 'UFOP', 'DECSI', GETDATE(), 1, 0, ''),
('5', 'chgferreira@ufop.edu.br', 'Carlos Henrique', 'Gomes Ferreira', 'UFOP', 'DECSI', GETDATE(), 1, 0, ''),
('6', 'darlan@ufrop.edu.br', 'Darlan', 'Nunes de Brito', 'UFOP', 'DECSI', GETDATE(), 1, 0, ''),
('7', 'diego@ufrop.edu.br', 'Diego Zuquim', 'Guimarães Garcia', 'UFOP', 'DECSI', GETDATE(), 1, 0, ''),
('8', 'eduardo.ribeiro@ufrop.edu.br', 'Eduardo', 'da Silva Ribeiro', 'UFOP', 'DECSI', GETDATE(), 1, 0, ''),
('9', 'eltonmc@ufop.edu.br', 'Elton Máximo', 'Cardoso', 'UFOP', 'DECSI', GETDATE(), 1, 0, ''),
('10', 'euler@ufop.edu.br', 'Euler', 'Horta Marinho', 'UFOP', 'DECSI', GETDATE(), 1, 0, ''),
('11', 'fboliveira@ufop.edu.br', 'Fernando Bernardes', 'de Oliveira', 'UFOP', 'DECSI', GETDATE(), 1, 0, ''),
('12', 'filipe.ribeiro@ufop.edu.br', 'Filipe', 'Nunes Ribeiro', 'UFOP', 'DECSI', GETDATE(), 1, 0, ''),
('13', 'george@ufop.edu.br', 'George Henrique', 'Godim da Fonseca', 'UFOP', 'DECSI', GETDATE(), 1, 0, ''),
('14', 'gildaaa@ufop.edu.br', 'Gilda Aparecida', 'de Assis', 'UFOP', 'DECSI', GETDATE(), 1, 0, ''),
('15', 'harlei@ufop.edu.br', 'Harlei Miguel', 'Arruda Leite', 'UFOP', 'DECSI', GETDATE(), 1, 0, ''),
('16', 'helen@ufop.edu.br', 'Helen de Cássia', 'Sousa da Costa Lima', 'UFOP', 'DECSI', GETDATE(), 1, 0, ''),
('17', 'igormuzetti@ufop.edu.br', 'Igor Muzetti', 'Pereira', 'UFOP', 'DECSI', GETDATE(), 1, 0, ''),
('18', 'janniele@ufop.edu.br', 'Janniele Aparecida', 'Soares', 'UFOP', 'DECSI', GETDATE(), 1, 0, ''),
('19', 'lucineia@ufop.edu.br', 'Lucinéia Souza', 'Maia', 'UFOP', 'DECSI', GETDATE(), 1, 0, ''),
('20', 'luiz.torres@ufop.edu.br', 'Luiz Carlos', 'Bambirra Torres', 'UFOP', 'DECSI', GETDATE(), 1, 0, ''),
('21', 'marlon@ufop.edu.br', 'Marlon Paolo', 'Lima', 'UFOP', 'DECSI', GETDATE(), 1, 0, ''),
('22', 'mateus@ufop.edu.br', 'Mateus Ferreira', 'Satler', 'UFOP', 'DECSI', GETDATE(), 1, 0, ''),
('23', 'rfalexandre@ufop.edu.br', 'Rafael Frederico', 'Alexandre', 'UFOP', 'DECSI', GETDATE(), 1, 0, ''),
('24', 'samuelbrito@ufop.edu.br', 'Samuel Souza', 'Brito', 'UFOP', 'DECSI', GETDATE(), 1, 0, ''),
('25', 'talles@ufop.edu.br', 'Talles Henrique', 'de Medeiros', 'UFOP', 'DECSI', GETDATE(), 1, 0, ''),
('26', 'tatiana@ufop.edu.br', 'Tatiana', 'Alves Costa', 'UFOP', 'DECSI', GETDATE(), 1, 0, ''),
('27', 'theo@ufop.edu.br', 'Theo', 'Silva Lins', 'UFOP', 'DECSI', GETDATE(), 1, 0, ''),
('28', 'thiagoluange@ufop.edu.br', 'Thiago Luange', 'Gomes', 'UFOP', 'DECSI', GETDATE(), 1, 0, ''),
('29', 'tiagolima@decsi.ufop.edu.br', 'Tiago França Melo', 'de Lima', 'UFOP', 'DECSI', GETDATE(), 1, 0, ''),
('30', 'vjpamorim@ufop.edu.br', 'Vicente José', 'Peixoto de Amorim', 'UFOP', 'DECSI', GETDATE(), 1, 0, ''),
('31', 'viniciusvdias@ufop.edu.br', 'Vinícius Vitor', 'dos Santos Dias', 'UFOP', 'DECSI', GETDATE(), 1, 0, ''),
('33', 'adam@ufop.edu.br', 'Adam James', 'Sargeant', 'ICEA', 'DECEA', GETDATE(), 1, 0, ''),
('34', 'alana.felippe@ufop.edu.br', 'Alana Cavalcante', 'Felippe', 'ICEA', 'DECEA', GETDATE(), 1, 0, ''),
('35', 'anliy@ufop.edu.br', 'Anliy Natsuyo Nashimoto', 'Sargeant', 'ICEA', 'DECEA', GETDATE(), 1, 0, ''),
('36', 'carlosrpontesf@ufop.edu.br', 'Carlos Renato', 'Pontes', 'ICEA', 'DECEA', GETDATE(), 1, 0, ''),
('37', 'cristiano.benjamin@ufop.edu.br', 'Cristiano Santos', 'Benjamin', 'ICEA', 'DECEA', GETDATE(), 1, 0, ''),
('38', 'diego.barros@ufop.edu.br', 'Diego da Silva', 'Barros', 'ICEA', 'DECEA', GETDATE(), 1, 0, ''),
('39', 'fbacani@ufop.edu.br', 'Felipo', 'Bacani', 'ICEA', 'DECEA', GETDATE(), 1, 0, ''),
('40', 'fernanda.cruz@ufop.edu.br', 'Fernanda Tatia', 'Cruz', 'ICEA', 'DECEA', GETDATE(), 1, 0, ''),
('41', 'herson@ufop.edu.br', 'Herson de Oliveira', 'Peixoto', 'ICEA', 'DECEA', GETDATE(), 1, 0, ''),
('42', 'jennyffer.barrera@ufop.edu.br', 'Jennyffer Smith Bohorquez', 'Barrera', 'ICEA', 'DECEA', GETDATE(), 1, 0, ''),
('43', 'juvenil@ufop.edu.br', 'Juvenil Siqueira de Oliveira', 'Filho', 'ICEA', 'DECEA', GETDATE(), 1, 0, ''),
('44', 'vieirakarla@ufop.edu.br', 'Karla Moreira', 'Vieira', 'ICEA', 'DECEA', GETDATE(), 1, 0, ''),
('45', 'lucilia@ufop.edu.br', 'Lucília Alves', 'Linhares', 'ICEA', 'DECEA', GETDATE(), 1, 0, ''),
('46', 'marcosgoulart@ufop.edu.br', 'Marcos Goulart', 'Lima', 'ICEA', 'DECEA', GETDATE(), 1, 0, ''),
('47', 'rafael.thebaldi@ufop.edu.br', 'Rafael Santos', 'Thebaldi', 'ICEA', 'DECEA', GETDATE(), 1, 0, ''),
('48', 'ronan.ferreira@ufop.edu.br', 'Ronan Silva', 'Ferreira', 'ICEA', 'DECEA', GETDATE(), 1, 0, ''),
('49', 'savio.correa@ufop.edu.br', 'Savio Figueira', 'Correa', 'ICEA', 'DECEA', GETDATE(), 1, 0, ''),
('50', 'shirley@ufop.edu.br', 'Shirley da Silva', 'Macedo', 'ICEA', 'DECEA', GETDATE(), 1, 0, '');


INSERT INTO Professores (Id, Email, FirstName, LastName, Campus, Departamento, CreatedOn, Ativo, Media, AvaliacaoGeral)  
VALUES  
('0', 'professor@ufop.edu.br', 'Professor', 'Teste', 'UFOP', 'DECSI', GETDATE(), 1, 0, '')

INSERT INTO Professores (Id, Email, FirstName, LastName, Campus, Departamento, CreatedOn, Ativo, Media, AvaliacaoGeral)  
VALUES  
('32', 'freddy.vicente@ufop.edu.br', 'Frddy Pablo', 'Castro Vicente', 'UFOP', 'DECEA', GETDATE(), 1, 0, '')

update Professores
set FirstName = 'Freddy'
where id = 32

select * from AspNetUsers

update AspNetUsers
set EmailConfirmed = 1
where Id = '058ccae5-dedf-4a8b-b96e-f23781dc2336'

update Professores
set TotalComentarios = 0
where id = 0

select * from Comentarios

update Comentarios
set Ativo = 1

update Professores
set Media = 0,
AvaliacaoGeral = '',
TotalComentarios = 0

update Disciplinas
set Media = 0,
AvaliacaoGeral = '',
TotalComentarios = 0

update Professores
set Id = 999
where Id = 0

update Professores
set Campus = 'ICEA'

update Professores
set Ativo = 0
where Id = 999