namespace serveur.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MajModel4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Favoris",
                c => new
                    {
                        IdFavo = c.Int(nullable: false, identity: true),
                        IdUser = c.Int(nullable: false),
                        IdAnno = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdFavo)
                .ForeignKey("dbo.Annonces", t => t.IdAnno, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.IdUser, cascadeDelete: true)
                .Index(t => t.IdUser)
                .Index(t => t.IdAnno);
            
            CreateTable(
                "dbo.Offres",
                c => new
                    {
                        IdOffr = c.Int(nullable: false, identity: true),
                        Montant = c.Double(nullable: false),
                        IdUser = c.Int(nullable: false),
                        IdEnch = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdOffr)
                .ForeignKey("dbo.Encheres", t => t.IdEnch, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.IdUser, cascadeDelete: true)
                .Index(t => t.IdUser)
                .Index(t => t.IdEnch);
            
            AddColumn("dbo.Annonces", "MontantMin", c => c.Double(nullable: false));
            AddColumn("dbo.Illustrations", "IdProd", c => c.Int(nullable: false));
            CreateIndex("dbo.Illustrations", "IdProd");
            AddForeignKey("dbo.Illustrations", "IdProd", "dbo.Produits", "IdProd", cascadeDelete: true);
            DropColumn("dbo.Annonces", "IsAchat");
            DropColumn("dbo.Encheres", "MontantMin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Encheres", "MontantMin", c => c.Double(nullable: false));
            AddColumn("dbo.Annonces", "IsAchat", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Offres", "IdUser", "dbo.Users");
            DropForeignKey("dbo.Offres", "IdEnch", "dbo.Encheres");
            DropForeignKey("dbo.Illustrations", "IdProd", "dbo.Produits");
            DropForeignKey("dbo.Favoris", "IdUser", "dbo.Users");
            DropForeignKey("dbo.Favoris", "IdAnno", "dbo.Annonces");
            DropIndex("dbo.Offres", new[] { "IdEnch" });
            DropIndex("dbo.Offres", new[] { "IdUser" });
            DropIndex("dbo.Illustrations", new[] { "IdProd" });
            DropIndex("dbo.Favoris", new[] { "IdAnno" });
            DropIndex("dbo.Favoris", new[] { "IdUser" });
            DropColumn("dbo.Illustrations", "IdProd");
            DropColumn("dbo.Annonces", "MontantMin");
            DropTable("dbo.Offres");
            DropTable("dbo.Favoris");
        }
    }
}
