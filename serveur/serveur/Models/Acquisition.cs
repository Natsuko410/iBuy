using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace serveur.Models
{
    public class Acquisition
    {
        [Key]
        public int IdAcqu { get; set; }

        public int Quantite { get; set; }

        [ForeignKey("Achat")]
        public int IdAchat { get; set; }
        public virtual Achat Achat { get; set; }

        [ForeignKey("User")]
        public int? IdUser { get; set; }
        public virtual User User { get; set; }
    }
}