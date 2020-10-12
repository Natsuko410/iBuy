using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace serveur.Models
{
    public class Enchere
    {
        [Key]
        public int IdEnch { get; set; }
        public double MontantMin { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }

        [ForeignKey("Annonce")]
        public int IdAnno { get; set; }
        public virtual Annonce Annonce { get; set; }
    }
}