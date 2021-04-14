namespace Fietskoerier_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VerwijderAdminBijZending : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Zendingen", "AdminID", "dbo.Admins");
            DropForeignKey("dbo.Zendingen", "KoerierID", "dbo.Koeriers");
            DropIndex("dbo.Zendingen", new[] { "KoerierID" });
            DropIndex("dbo.Zendingen", new[] { "AdminID" });
            RenameColumn(table: "dbo.Zendingen", name: "AdminID", newName: "Admin_AdminID");
            AlterColumn("dbo.Zendingen", "KoerierID", c => c.Int());
            AlterColumn("dbo.Zendingen", "Admin_AdminID", c => c.Int());
            CreateIndex("dbo.Zendingen", "KoerierID");
            CreateIndex("dbo.Zendingen", "Admin_AdminID");
            AddForeignKey("dbo.Zendingen", "Admin_AdminID", "dbo.Admins", "AdminID");
            AddForeignKey("dbo.Zendingen", "KoerierID", "dbo.Koeriers", "KoerierID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Zendingen", "KoerierID", "dbo.Koeriers");
            DropForeignKey("dbo.Zendingen", "Admin_AdminID", "dbo.Admins");
            DropIndex("dbo.Zendingen", new[] { "Admin_AdminID" });
            DropIndex("dbo.Zendingen", new[] { "KoerierID" });
            AlterColumn("dbo.Zendingen", "Admin_AdminID", c => c.Int(nullable: false));
            AlterColumn("dbo.Zendingen", "KoerierID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Zendingen", name: "Admin_AdminID", newName: "AdminID");
            CreateIndex("dbo.Zendingen", "AdminID");
            CreateIndex("dbo.Zendingen", "KoerierID");
            AddForeignKey("dbo.Zendingen", "KoerierID", "dbo.Koeriers", "KoerierID", cascadeDelete: true);
            AddForeignKey("dbo.Zendingen", "AdminID", "dbo.Admins", "AdminID", cascadeDelete: true);
        }
    }
}
