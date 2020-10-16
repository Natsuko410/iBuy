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
    public class Achat
    {
        [Key]
        public int IdAchat { get; set; }
        [Required(ErrorMessage = "Le champs prix est obligatoire")]
        public double Prix { get; set; }
        [Required(ErrorMessage = "Le champs quantité est obligatoire")]
        public int Quantite { get; set; }

        [ForeignKey("User")]
        public int IdUser { get; set; }
        [XmlIgnore, JsonIgnore]
        public virtual User User { get; set; }

        [ForeignKey("Annonce")]
        public int IdAnno { get; set; }
        public virtual Annonce Annonce { get; set; }

    }
}