namespace serveur.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MajModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Annonces", "Titre", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Annonces", "Titre");
        }
    }
}
