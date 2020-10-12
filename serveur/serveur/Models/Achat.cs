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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAchat { get; set; }
        public double Prix { get; set; }
        public int Quantite { get; set; }

        [ForeignKey("IdUser")]
        public User User { get; set; }
        public int? IdUser { get; set; }

        [ForeignKey("IdAnno")]
        public Annonce Annonce { get; set; }
        public int? IdAnno { get; set; }

    }
}