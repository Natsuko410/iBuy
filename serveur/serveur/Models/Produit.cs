using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace serveur.Models
{
    public class Produit
    {
        [Key]
        public int IdProd { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public string Etat { get; set; }

        [ForeignKey("Categorie")]
        public int? IdCat { get; set; }
        public virtual Categorie Categorie { get; set; }

    }
}