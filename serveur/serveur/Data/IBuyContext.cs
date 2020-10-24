using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace serveur.Data
{
    public class IBuyContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public IBuyContext() : base("name=IBuyContext")
        {
        }

        public System.Data.Entity.DbSet<serveur.Models.User> Users { get; set; }

        public System.Data.Entity.DbSet<serveur.Models.Categorie> Categories { get; set; }

        public System.Data.Entity.DbSet<serveur.Models.Illustration> Illustrations { get; set; }

        public System.Data.Entity.DbSet<serveur.Models.Enchere> Encheres { get; set; }

        public System.Data.Entity.DbSet<serveur.Models.Annonce> Annonces { get; set; }

        public System.Data.Entity.DbSet<serveur.Models.Avis> Avis { get; set; }

        public System.Data.Entity.DbSet<serveur.Models.TokenWallet> TokenWallets { get; set; }

        public System.Data.Entity.DbSet<serveur.Models.Favori> Favoris { get; set; }

        public System.Data.Entity.DbSet<serveur.Models.Offre> Offres { get; set; }
    }
}
