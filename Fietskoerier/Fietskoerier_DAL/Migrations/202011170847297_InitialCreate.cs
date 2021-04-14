namespace Fietskoerier_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.AdminID)
                .Index(t => t.Email, unique: true);
            
            CreateTable(
                "dbo.Zendingen",
                c => new
                    {
                        ZendingID = c.Int(nullable: false, identity: true),
                        Aanmaakdatum = c.DateTime(),
                        Afhaaldatum = c.DateTime(nullable: false),
                        AantalPaketten = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Klantnummer = c.Int(nullable: false),
                        OntvangerID = c.Int(nullable: false),
                        LeveringstermijnID = c.Int(nullable: false),
                        GewichtID = c.Int(nullable: false),
                        KoerierID = c.Int(nullable: false),
                        AdminID = c.Int(nullable: true),
                    })
                .PrimaryKey(t => t.ZendingID)
                .ForeignKey("dbo.Admins", t => t.AdminID, cascadeDelete: true)
                .ForeignKey("dbo.Gewichten", t => t.GewichtID, cascadeDelete: true)
                .ForeignKey("dbo.Klanten", t => t.Klantnummer, cascadeDelete: true)
                .ForeignKey("dbo.Koeriers", t => t.KoerierID, cascadeDelete: true)
                .ForeignKey("dbo.Leveringstermijnen", t => t.LeveringstermijnID, cascadeDelete: true)
                .ForeignKey("dbo.Ontvangers", t => t.OntvangerID, cascadeDelete: true)
                .Index(t => t.Klantnummer)
                .Index(t => t.OntvangerID)
                .Index(t => t.LeveringstermijnID)
                .Index(t => t.GewichtID)
                .Index(t => t.KoerierID)
                .Index(t => t.AdminID);
            
            CreateTable(
                "dbo.Gewichten",
                c => new
                    {
                        GewichtID = c.Int(nullable: false, identity: true),
                        Gewichtscategorie = c.String(nullable: false),
                        Bedrag = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.GewichtID);
            
            CreateTable(
                "dbo.Klanten",
                c => new
                    {
                        Klantnummer = c.Int(nullable: false, identity: true),
                        Achternaam = c.String(nullable: false, maxLength: 250),
                        Voornaam = c.String(nullable: false, maxLength: 250),
                        Wachtwoord = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 150),
                        Straat = c.String(nullable: false, maxLength: 250),
                        Huisnummer = c.String(nullable: false, maxLength: 10),
                        Gemeente = c.String(nullable: false, maxLength: 60),
                        Postcode = c.String(nullable: false, maxLength: 10),
                        Telefoonnummer = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Klantnummer)
                .Index(t => t.Email, unique: true);
            
            CreateTable(
                "dbo.KlantAfrekeningen",
                c => new
                    {
                        KlantAfrekeningID = c.Int(nullable: false, identity: true),
                        BetalingsType = c.String(),
                        Klantnummer = c.Int(nullable: false),
                        ZendingID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.KlantAfrekeningID)
                .ForeignKey("dbo.Klanten", t => t.Klantnummer, cascadeDelete: false)
                .ForeignKey("dbo.Zendingen", t => t.ZendingID, cascadeDelete: false)
                .Index(t => new { t.Klantnummer, t.ZendingID }, unique: true, name: "IX_KlantnummerZendingID");
            
            CreateTable(
                "dbo.Koeriers",
                c => new
                    {
                        KoerierID = c.Int(nullable: false, identity: true),
                        Achternaam = c.String(nullable: false, maxLength: 250),
                        Voornaam = c.String(nullable: false, maxLength: 250),
                        Telefoon = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.KoerierID);
            
            CreateTable(
                "dbo.Leveringstermijnen",
                c => new
                    {
                        LeveringstermijnID = c.Int(nullable: false, identity: true),
                        Naam = c.String(nullable: false),
                        Bedrag = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.LeveringstermijnID);
            
            CreateTable(
                "dbo.Ontvangers",
                c => new
                    {
                        OntvangerID = c.Int(nullable: false, identity: true),
                        Naam = c.String(nullable: false, maxLength: 250),
                        Adres = c.String(nullable: false, maxLength: 250),
                        Gemeente = c.String(nullable: false, maxLength: 250),
                        Telefoon = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.OntvangerID)
                .Index(t => t.Email, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Zendingen", "OntvangerID", "dbo.Ontvangers");
            DropForeignKey("dbo.Zendingen", "LeveringstermijnID", "dbo.Leveringstermijnen");
            DropForeignKey("dbo.Zendingen", "KoerierID", "dbo.Koeriers");
            DropForeignKey("dbo.Zendingen", "Klantnummer", "dbo.Klanten");
            DropForeignKey("dbo.KlantAfrekeningen", "ZendingID", "dbo.Zendingen");
            DropForeignKey("dbo.KlantAfrekeningen", "Klantnummer", "dbo.Klanten");
            DropForeignKey("dbo.Zendingen", "GewichtID", "dbo.Gewichten");
            DropForeignKey("dbo.Zendingen", "AdminID", "dbo.Admins");
            DropIndex("dbo.Ontvangers", new[] { "Email" });
            DropIndex("dbo.KlantAfrekeningen", "IX_KlantnummerZendingID");
            DropIndex("dbo.Klanten", new[] { "Email" });
            DropIndex("dbo.Zendingen", new[] { "AdminID" });
            DropIndex("dbo.Zendingen", new[] { "KoerierID" });
            DropIndex("dbo.Zendingen", new[] { "GewichtID" });
            DropIndex("dbo.Zendingen", new[] { "LeveringstermijnID" });
            DropIndex("dbo.Zendingen", new[] { "OntvangerID" });
            DropIndex("dbo.Zendingen", new[] { "Klantnummer" });
            DropIndex("dbo.Admins", new[] { "Email" });
            DropTable("dbo.Ontvangers");
            DropTable("dbo.Leveringstermijnen");
            DropTable("dbo.Koeriers");
            DropTable("dbo.KlantAfrekeningen");
            DropTable("dbo.Klanten");
            DropTable("dbo.Gewichten");
            DropTable("dbo.Zendingen");
            DropTable("dbo.Admins");
        }
    }
}
