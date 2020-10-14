namespace serveur.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Achats",
                c => new
                    {
                        IdAchat = c.Int(nullable: false, identity: true),
                        Prix = c.Double(nullable: false),
                        Quantite = c.Int(nullable: false),
                        IdUser = c.Int(nullable: false),
                        IdAnno = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdAchat)
                .ForeignKey("dbo.Annonces", t => t.IdAnno, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.IdUser, cascadeDelete: true)
                .Index(t => t.IdUser)
                .Index(t => t.IdAnno);
            
            CreateTable(
                "dbo.Annonces",
                c => new
                    {
                        IdAnno = c.Int(nullable: false, identity: true),
                        DateAjout = c.DateTime(nullable: false),
                        Etat = c.String(),
                        IsEnchere = c.Boolean(nullable: false),
                        IsAchat = c.Boolean(nullable: false),
                        IdUser = c.Int(),
                        IdProd = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdAnno)
                .ForeignKey("dbo.Produits", t => t.IdProd, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.IdUser)
                .Index(t => t.IdUser)
                .Index(t => t.IdProd);
            
            CreateTable(
                "dbo.Produits",
                c => new
                    {
                        IdProd = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Description = c.String(),
                        Etat = c.String(),
                        IdCat = c.Int(),
                    })
                .PrimaryKey(t => t.IdProd)
                .ForeignKey("dbo.Categories", t => t.IdCat)
                .Index(t => t.IdCat);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        IdCat = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Desc = c.String(),
                    })
                .PrimaryKey(t => t.IdCat);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        IdUser = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Prenom = c.String(),
                        Pseudo = c.String(),
                        Email = c.String(),
                        Mdp = c.String(),
                        Tel = c.String(),
                        AddrPays = c.String(),
                        AddrLocalite = c.String(),
                        AddrCodePostal = c.String(),
                        AddrRue = c.String(),
                        AddrNumero = c.String(),
                    })
                .PrimaryKey(t => t.IdUser);
            
            CreateTable(
                "dbo.Acquisitions",
                c => new
                    {
                        IdAcqu = c.Int(nullable: false, identity: true),
                        Quantite = c.Int(nullable: false),
                        IdAchat = c.Int(nullable: false),
                        IdUser = c.Int(),
                    })
                .PrimaryKey(t => t.IdAcqu)
                .ForeignKey("dbo.Achats", t => t.IdAchat, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.IdUser)
                .Index(t => t.IdAchat)
                .Index(t => t.IdUser);
            
            CreateTable(
                "dbo.Avis",
                c => new
                    {
                        IdAvis = c.Int(nullable: false, identity: true),
                        Texte = c.String(),
                        DatePoste = c.DateTime(nullable: false),
                        IdConcerne = c.Int(nullable: false),
                        IdUser = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdAvis)
                .ForeignKey("dbo.Users", t => t.IdUser, cascadeDelete: true)
                .Index(t => t.IdUser);
            
            CreateTable(
                "dbo.Encheres",
                c => new
                    {
                        IdEnch = c.Int(nullable: false, identity: true),
                        MontantMin = c.Double(nullable: false),
                        DateDebut = c.DateTime(nullable: false),
                        DateFin = c.DateTime(nullable: false),
                        IdAnno = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdEnch)
                .ForeignKey("dbo.Annonces", t => t.IdAnno, cascadeDelete: true)
                .Index(t => t.IdAnno);
            
            CreateTable(
                "dbo.Illustrations",
                c => new
                    {
                        IdIllu = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                    })
                .PrimaryKey(t => t.IdIllu);
            
            CreateTable(
                "dbo.TokenWallets",
                c => new
                    {
                        IdTokenWallet = c.Int(nullable: false, identity: true),
                        Token = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdTokenWallet)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TokenWallets", "UserId", "dbo.Users");
            DropForeignKey("dbo.Encheres", "IdAnno", "dbo.Annonces");
            DropForeignKey("dbo.Avis", "IdUser", "dbo.Users");
            DropForeignKey("dbo.Acquisitions", "IdUser", "dbo.Users");
            DropForeignKey("dbo.Acquisitions", "IdAchat", "dbo.Achats");
            DropForeignKey("dbo.Achats", "IdUser", "dbo.Users");
            DropForeignKey("dbo.Achats", "IdAnno", "dbo.Annonces");
            DropForeignKey("dbo.Annonces", "IdUser", "dbo.Users");
            DropForeignKey("dbo.Annonces", "IdProd", "dbo.Produits");
            DropForeignKey("dbo.Produits", "IdCat", "dbo.Categories");
            DropIndex("dbo.TokenWallets", new[] { "UserId" });
            DropIndex("dbo.Encheres", new[] { "IdAnno" });
            DropIndex("dbo.Avis", new[] { "IdUser" });
            DropIndex("dbo.Acquisitions", new[] { "IdUser" });
            DropIndex("dbo.Acquisitions", new[] { "IdAchat" });
            DropIndex("dbo.Produits", new[] { "IdCat" });
            DropIndex("dbo.Annonces", new[] { "IdProd" });
            DropIndex("dbo.Annonces", new[] { "IdUser" });
            DropIndex("dbo.Achats", new[] { "IdAnno" });
            DropIndex("dbo.Achats", new[] { "IdUser" });
            DropTable("dbo.TokenWallets");
            DropTable("dbo.Illustrations");
            DropTable("dbo.Encheres");
            DropTable("dbo.Avis");
            DropTable("dbo.Acquisitions");
            DropTable("dbo.Users");
            DropTable("dbo.Categories");
            DropTable("dbo.Produits");
            DropTable("dbo.Annonces");
            DropTable("dbo.Achats");
        }
    }
}
