namespace LuccasCorpVX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Disciplinas", "CodigoUFOP", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Disciplinas", "CodigoUFOP");
        }
    }
}
