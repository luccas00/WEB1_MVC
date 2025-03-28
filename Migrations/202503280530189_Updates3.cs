namespace LuccasCorpVX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updates3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comentarios", "ProfessorNome", c => c.String());
            AddColumn("dbo.Comentarios", "DisciplinaNome", c => c.String());
            AddColumn("dbo.Comentarios", "DisciplinaCodigo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comentarios", "DisciplinaCodigo");
            DropColumn("dbo.Comentarios", "DisciplinaNome");
            DropColumn("dbo.Comentarios", "ProfessorNome");
        }
    }
}
