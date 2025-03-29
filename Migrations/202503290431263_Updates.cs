namespace LuccasCorpVX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Disciplinas", "TotalComentarios", c => c.Int(nullable: false));
            AddColumn("dbo.Professores", "TotalComentarios", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Professores", "TotalComentarios");
            DropColumn("dbo.Disciplinas", "TotalComentarios");
        }
    }
}
