using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace serveur.Models
{
    public class Achat
    {
        [Key]
        public int IdAchat { get; set; }
        public double Prix { get; set; }
        public int Quantite { get; set; }

        [ForeignKey("User")]
        public int IdUser { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Annonce")]
        public int IdAnno { get; set; }
        public virtual Annonce Annonce { get; set; }

    }
}