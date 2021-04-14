namespace Fietskoerier_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Datumnull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Zendingen", "Aanmaakdatum", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Zendingen", "Aanmaakdatum", c => c.String());
        }
    }
}
