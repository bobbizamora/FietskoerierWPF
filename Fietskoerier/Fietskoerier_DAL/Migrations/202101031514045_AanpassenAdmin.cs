namespace Fietskoerier_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AanpassenAdmin : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Zendingen", "Admin_AdminID", "dbo.Admins");
            DropIndex("dbo.Admins", new[] { "Email" });
            DropIndex("dbo.Zendingen", new[] { "Admin_AdminID" });
            AddColumn("dbo.Klanten", "IsAdmin", c => c.Boolean(nullable: false));
            DropColumn("dbo.Zendingen", "Admin_AdminID");
            DropTable("dbo.Admins");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminID = c.Int(nullable: false, identity: true),
                        Achternaam = c.String(nullable: false, maxLength: 150),
                        Voornaam = c.String(nullable: false, maxLength: 150),
                        Wachtwoord = c.String(nullable: false, maxLength: 150),
                        Email = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.AdminID);
            
            AddColumn("dbo.Zendingen", "Admin_AdminID", c => c.Int());
            DropColumn("dbo.Klanten", "IsAdmin");
            CreateIndex("dbo.Zendingen", "Admin_AdminID");
            CreateIndex("dbo.Admins", "Email", unique: true);
            AddForeignKey("dbo.Zendingen", "Admin_AdminID", "dbo.Admins", "AdminID");
        }
    }
}
