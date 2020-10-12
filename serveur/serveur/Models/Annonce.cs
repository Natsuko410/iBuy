using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace serveur.Models
{
    public class Annonce
    {
        [Key]
        public int IdAnno { get; set; }
        public DateTime DateAjout { get; set; }
        public string Etat { get; set; }
        public bool IsEnchere { get; set; }
        public bool IsAchat { get; set; }

        [ForeignKey("User")]
        public int? IdUser { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Produit")]
        public int IdProd { get; set; }
        public virtual Produit Produit { get; set; }

    }
}