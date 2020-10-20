namespace serveur.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MajModel5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "MoyenneNote", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "MoyenneNote");
        }
    }
}
