namespace LuccasCorpVX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatesMedia : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alunos",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        NumeroMatricula = c.String(nullable: false, maxLength: 100),
                        CreatedOn = c.DateTime(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comentarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(nullable: false, maxLength: 100),
                        Descricao = c.String(nullable: false),
                        Autor = c.String(nullable: false),
                        Professor = c.Int(nullable: false),
                        Disciplina = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        Sentimento = c.String(),
                        Positivo = c.Double(nullable: false),
                        Negativo = c.Double(nullable: false),
                        Neutro = c.Double(nullable: false),
                        Insulto = c.Boolean(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Disciplinas",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100),
                        Campus = c.String(nullable: false, maxLength: 100),
                        Departamento = c.String(nullable: false, maxLength: 100),
                        Ativo = c.Boolean(nullable: false),
                        Media = c.Double(nullable: false),
                        AvaliacaoGeral = c.String(),
                    })
                .PrimaryKey(t => t.Codigo);
            
            CreateTable(
                "dbo.Professores",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        Campus = c.String(nullable: false, maxLength: 100),
                        Departamento = c.String(maxLength: 100),
                        CreatedOn = c.DateTime(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        Media = c.Double(nullable: false),
                        AvaliacaoGeral = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ContactMessages", "Nome", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.ContactMessages", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "Tipo", c => c.String());
            AddColumn("dbo.AspNetUsers", "CreatedOn", c => c.DateTime(nullable: false));
            DropColumn("dbo.ContactMessages", "Name");
            DropColumn("dbo.ContactMessages", "DateSent");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContactMessages", "DateSent", c => c.DateTime(nullable: false));
            AddColumn("dbo.ContactMessages", "Name", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.AspNetUsers", "CreatedOn");
            DropColumn("dbo.AspNetUsers", "Tipo");
            DropColumn("dbo.ContactMessages", "CreatedOn");
            DropColumn("dbo.ContactMessages", "Nome");
            DropTable("dbo.Professores");
            DropTable("dbo.Disciplinas");
            DropTable("dbo.Comentarios");
            DropTable("dbo.Alunos");
        }
    }
}
