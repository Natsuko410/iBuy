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

        [ForeignKey("IdAchat")]
        public Achat Achat { get; set; }
        public int? IdAchat { get; set; }

        [ForeignKey("IdUser")]
        public User User { get; set; }
        public int? IdUser { get; set; }
    }
}