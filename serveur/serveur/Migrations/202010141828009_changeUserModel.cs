namespace serveur.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeUserModel : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.TokenWallets", name: "UserId", newName: "IsUser");
            RenameIndex(table: "dbo.TokenWallets", name: "IX_UserId", newName: "IX_IsUser");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.TokenWallets", name: "IX_IsUser", newName: "IX_UserId");
            RenameColumn(table: "dbo.TokenWallets", name: "IsUser", newName: "UserId");
        }
    }
}
