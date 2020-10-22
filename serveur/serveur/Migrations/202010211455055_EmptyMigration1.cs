namespace serveur.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmptyMigration1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Offres", "Etat");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Offres", "Etat", c => c.String(nullable: false));
        }
    }
}
