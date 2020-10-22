namespace serveur.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmptyMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Offres", "Etat", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Offres", "Etat");
        }
    }
}
