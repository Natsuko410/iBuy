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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProd { get; set; }
        public String Nom { get; set; }
        public String Description { get; set; }
        public String Etat { get; set; }

        [ForeignKey("IdCat")]
        public Categorie Categorie { get; set; }
        public int IdCat { get; set; }

    }
}