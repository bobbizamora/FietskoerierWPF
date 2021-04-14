namespace Fietskoerier_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Klanten", "WachtwoordHash", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Klanten", "Wachtwoord");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Klanten", "Wachtwoord", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Klanten", "WachtwoordHash");
        }
    }
}
