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
    public class Acquisition
    {
        [Key]
        public int IdAcqu { get; set; }
        [Required(ErrorMessage ="Le champs quantité est obligatoire.")]
        public int Quantite { get; set; }

        [ForeignKey("Achat")]
        public int IdAchat { get; set; }
        [XmlIgnore, JsonIgnore]
        public virtual Achat Achat { get; set; }

        [ForeignKey("User")]
        public int? IdUser { get; set; }
        [XmlIgnore, JsonIgnore]
        public virtual User User { get; set; }
    }
}