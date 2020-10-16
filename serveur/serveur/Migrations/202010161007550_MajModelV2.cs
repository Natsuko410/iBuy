namespace serveur.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MajModelV2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Annonces", "Titre", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Annonces", "Etat", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.Produits", "Nom", c => c.String(nullable: false, maxLength: 512));
            AlterColumn("dbo.Produits", "Description", c => c.String(maxLength: 1024));
            AlterColumn("dbo.Produits", "Etat", c => c.String(nullable: false, maxLength: 56));
            AlterColumn("dbo.Categories", "Nom", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.Categories", "Desc", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Users", "Nom", c => c.String(nullable: false, maxLength: 90));
            AlterColumn("dbo.Users", "Prenom", c => c.String(nullable: false, maxLength: 90));
            AlterColumn("dbo.Users", "Pseudo", c => c.String(nullable: false, maxLength: 48));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false, maxLength: 320));
            AlterColumn("dbo.Users", "Mdp", c => c.String(nullable: false, maxLength: 1024));
            AlterColumn("dbo.Users", "Tel", c => c.String(maxLength: 16));
            AlterColumn("dbo.Users", "AddrPays", c => c.String(nullable: false, maxLength: 90));
            AlterColumn("dbo.Users", "AddrLocalite", c => c.String(nullable: false, maxLength: 90));
            AlterColumn("dbo.Users", "AddrCodePostal", c => c.String(nullable: false, maxLength: 16));
            AlterColumn("dbo.Users", "AddrRue", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Users", "AddrNumero", c => c.String(nullable: false, maxLength: 16));
            AlterColumn("dbo.Avis", "Texte", c => c.String(nullable: false, maxLength: 1048));
            AlterColumn("dbo.Illustrations", "Path", c => c.String(nullable: false, maxLength: 2048));
            AlterColumn("dbo.TokenWallets", "Token", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TokenWallets", "Token", c => c.String());
            AlterColumn("dbo.Illustrations", "Path", c => c.String());
            AlterColumn("dbo.Avis", "Texte", c => c.String());
            AlterColumn("dbo.Users", "AddrNumero", c => c.String());
            AlterColumn("dbo.Users", "AddrRue", c => c.String());
            AlterColumn("dbo.Users", "AddrCodePostal", c => c.String());
            AlterColumn("dbo.Users", "AddrLocalite", c => c.String());
            AlterColumn("dbo.Users", "AddrPays", c => c.String());
            AlterColumn("dbo.Users", "Tel", c => c.String());
            AlterColumn("dbo.Users", "Mdp", c => c.String());
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Users", "Pseudo", c => c.String());
            AlterColumn("dbo.Users", "Prenom", c => c.String());
            AlterColumn("dbo.Users", "Nom", c => c.String());
            AlterColumn("dbo.Categories", "Desc", c => c.String());
            AlterColumn("dbo.Categories", "Nom", c => c.String());
            AlterColumn("dbo.Produits", "Etat", c => c.String());
            AlterColumn("dbo.Produits", "Description", c => c.String());
            AlterColumn("dbo.Produits", "Nom", c => c.String());
            AlterColumn("dbo.Annonces", "Etat", c => c.String());
            AlterColumn("dbo.Annonces", "Titre", c => c.String());
        }
    }
}
