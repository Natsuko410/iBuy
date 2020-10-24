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
    public class Illustration
    {
        [Key]
        public int IdIllu { get; set; }
        [
         MaxLength(2048, ErrorMessage ="Ce champs ne peut pas excéder 2048 caractères")
        ]
        public string Path { get; set; }

        [ForeignKey("Annonce")]
        public int IdAnno { get; set; }
        [JsonIgnore, XmlIgnore]
        public virtual Annonce Annonce { get; set; }
    }
}