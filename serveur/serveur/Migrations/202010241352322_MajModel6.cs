namespace serveur.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MajModel6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Produits", "IdCat", "dbo.Categories");
            DropForeignKey("dbo.Annonces", "IdProd", "dbo.Produits");
            DropForeignKey("dbo.Achats", "IdAnno", "dbo.Annonces");
            DropForeignKey("dbo.Achats", "IdUser", "dbo.Users");
            DropForeignKey("dbo.Acquisitions", "IdAchat", "dbo.Achats");
            DropForeignKey("dbo.Acquisitions", "IdUser", "dbo.Users");
            DropForeignKey("dbo.Illustrations", "IdProd", "dbo.Produits");
            DropIndex("dbo.Achats", new[] { "IdUser" });
            DropIndex("dbo.Achats", new[] { "IdAnno" });
            DropIndex("dbo.Annonces", new[] { "IdProd" });
            DropIndex("dbo.Produits", new[] { "IdCat" });
            DropIndex("dbo.Acquisitions", new[] { "IdAchat" });
            DropIndex("dbo.Acquisitions", new[] { "IdUser" });
            DropIndex("dbo.Illustrations", new[] { "IdProd" });
            AddColumn("dbo.Annonces", "EtatAnno", c => c.String(nullable: false, maxLength: 32));
            AddColumn("dbo.Annonces", "NomProd", c => c.String(nullable: false, maxLength: 512));
            AddColumn("dbo.Annonces", "DescriptionProd", c => c.String(maxLength: 1024));
            AddColumn("dbo.Annonces", "EtatProd", c => c.String(nullable: false, maxLength: 56));
            AddColumn("dbo.Annonces", "IdCat", c => c.Int());
            AddColumn("dbo.Users", "IsAdmin", c => c.Boolean(nullable: false));
            AddColumn("dbo.Illustrations", "IdAnno", c => c.Int(nullable: false));
            AlterColumn("dbo.Illustrations", "Path", c => c.String(maxLength: 2048));
            CreateIndex("dbo.Annonces", "IdCat");
            CreateIndex("dbo.Illustrations", "IdAnno");
            AddForeignKey("dbo.Annonces", "IdCat", "dbo.Categories", "IdCat");
            AddForeignKey("dbo.Illustrations", "IdAnno", "dbo.Annonces", "IdAnno", cascadeDelete: true);
            DropColumn("dbo.Annonces", "Etat");
            DropColumn("dbo.Annonces", "IdProd");
            DropColumn("dbo.Illustrations", "IdProd");
            DropTable("dbo.Achats");
            DropTable("dbo.Produits");
            DropTable("dbo.Acquisitions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Acquisitions",
                c => new
                    {
                        IdAcqu = c.Int(nullable: false, identity: true),
                        Quantite = c.Int(nullable: false),
                        IdAchat = c.Int(nullable: false),
                        IdUser = c.Int(),
                    })
                .PrimaryKey(t => t.IdAcqu);
            
            CreateTable(
                "dbo.Produits",
                c => new
                    {
                        IdProd = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false, maxLength: 512),
                        Description = c.String(maxLength: 1024),
                        Etat = c.String(nullable: false, maxLength: 56),
                        IdCat = c.Int(),
                    })
                .PrimaryKey(t => t.IdProd);
            
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
                .PrimaryKey(t => t.IdAchat);
            
            AddColumn("dbo.Illustrations", "IdProd", c => c.Int(nullable: false));
            AddColumn("dbo.Annonces", "IdProd", c => c.Int(nullable: false));
            AddColumn("dbo.Annonces", "Etat", c => c.String(nullable: false, maxLength: 32));
            DropForeignKey("dbo.Illustrations", "IdAnno", "dbo.Annonces");
            DropForeignKey("dbo.Annonces", "IdCat", "dbo.Categories");
            DropIndex("dbo.Illustrations", new[] { "IdAnno" });
            DropIndex("dbo.Annonces", new[] { "IdCat" });
            AlterColumn("dbo.Illustrations", "Path", c => c.String(nullable: false, maxLength: 2048));
            DropColumn("dbo.Illustrations", "IdAnno");
            DropColumn("dbo.Users", "IsAdmin");
            DropColumn("dbo.Annonces", "IdCat");
            DropColumn("dbo.Annonces", "EtatProd");
            DropColumn("dbo.Annonces", "DescriptionProd");
            DropColumn("dbo.Annonces", "NomProd");
            DropColumn("dbo.Annonces", "EtatAnno");
            CreateIndex("dbo.Illustrations", "IdProd");
            CreateIndex("dbo.Acquisitions", "IdUser");
            CreateIndex("dbo.Acquisitions", "IdAchat");
            CreateIndex("dbo.Produits", "IdCat");
            CreateIndex("dbo.Annonces", "IdProd");
            CreateIndex("dbo.Achats", "IdAnno");
            CreateIndex("dbo.Achats", "IdUser");
            AddForeignKey("dbo.Illustrations", "IdProd", "dbo.Produits", "IdProd", cascadeDelete: true);
            AddForeignKey("dbo.Acquisitions", "IdUser", "dbo.Users", "IdUser");
            AddForeignKey("dbo.Acquisitions", "IdAchat", "dbo.Achats", "IdAchat", cascadeDelete: true);
            AddForeignKey("dbo.Achats", "IdUser", "dbo.Users", "IdUser", cascadeDelete: true);
            AddForeignKey("dbo.Achats", "IdAnno", "dbo.Annonces", "IdAnno", cascadeDelete: true);
            AddForeignKey("dbo.Annonces", "IdProd", "dbo.Produits", "IdProd", cascadeDelete: true);
            AddForeignKey("dbo.Produits", "IdCat", "dbo.Categories", "IdCat");
        }
    }
}
