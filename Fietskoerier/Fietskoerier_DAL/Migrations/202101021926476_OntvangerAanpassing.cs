namespace Fietskoerier_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OntvangerAanpassing : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Ontvangers", new[] { "Email" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Ontvangers", "Email", unique: true);
        }
    }
}
