namespace LuccasCorpVX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updates2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ContactMessages", "Nome", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ContactMessages", "Nome", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
