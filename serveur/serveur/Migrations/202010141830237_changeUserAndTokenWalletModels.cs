namespace serveur.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeUserAndTokenWalletModels : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.TokenWallets", name: "IsUser", newName: "IdUser");
            RenameIndex(table: "dbo.TokenWallets", name: "IX_IsUser", newName: "IX_IdUser");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.TokenWallets", name: "IX_IdUser", newName: "IX_IsUser");
            RenameColumn(table: "dbo.TokenWallets", name: "IdUser", newName: "IsUser");
        }
    }
}
