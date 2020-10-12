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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAnno { get; set; }
        public DateTime DateAjout { get; set; }
        public int Etat { get; set; }
        public bool IsEnchere { get; set; }
        public bool IsAchat { get; set; }

        [ForeignKey("IdUser")]
        public User User { get; set; }
        public int? IdUser { get; set; }

        [ForeignKey("IdProd")]
        public Produit Produit { get; set; }
        public int? IdProd { get; set; }

    }
}