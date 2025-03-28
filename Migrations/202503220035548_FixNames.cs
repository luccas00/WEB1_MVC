namespace LuccasCorpVX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixNames : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Alunoes", newName: "Alunos");
            RenameTable(name: "dbo.Professors", newName: "Professores");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Professores", newName: "Professors");
            RenameTable(name: "dbo.Alunos", newName: "Alunoes");
        }
    }
}
