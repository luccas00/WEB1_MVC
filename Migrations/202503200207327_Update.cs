namespace LuccasCorpVX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false),
                        Message = c.String(nullable: false),
                        DateSent = c.DateTime(nullable: false),
                        Sentimento = c.String(),
                        Positivo = c.Double(nullable: false),
                        Negativo = c.Double(nullable: false),
                        Neutro = c.Double(nullable: false),
                        Insulto = c.Boolean(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ContactMessages");
        }
    }
}
