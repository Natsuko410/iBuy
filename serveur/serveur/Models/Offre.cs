using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace serveur.Models
{
    public class Offre
    {
        [Key]
        public int IdOffr { get;  set; }

        [Required(ErrorMessage = "Le champs montant est obligatoire.")]
        public double Montant { get; set; }

        [ForeignKey("User")]
        public int IdUser { get; set; }
        [XmlIgnore, JsonIgnore]
        public virtual User User { get; set; }

        [ForeignKey("Enchere")]
        public int IdEnch { get; set; }
        public virtual Enchere Enchere { get; set; }

    }
}