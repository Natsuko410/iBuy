using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace serveur.Models
{
    public class Avis
    {
        [Key]
        public int IdAvis { get; set; }
        public string Texte { get; set; }
        public DateTime DatePoste { get; set; }
        public int IdConcerne { get; set; }

        [ForeignKey("User")]
        public int IdUser { get; set; }
        public virtual User User { get; set; }

    }
}