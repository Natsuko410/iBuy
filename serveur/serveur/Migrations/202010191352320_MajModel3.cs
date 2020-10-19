namespace serveur.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MajModel3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Avis", "Note", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Avis", "Note");
        }
    }
}
