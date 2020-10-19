using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace serveur.Models
{
    public class Avis
    {
        [Key]
        public int IdAvis { get; set; }
        [
         Required(ErrorMessage = "Le champs texte est obligatoire."),
         MinLength(32, ErrorMessage = "ce champs doit faire au moins 32 caractères."),
         MaxLength(1048, ErrorMessage = "ce champs ne peut pas excédé 1048 caractères.")
        ]
        public string Texte { get; set; }
        [Required(ErrorMessage = "Le champs note est obligatoire.")]
        public int Note { get; set; }
        [Required(ErrorMessage = "Le champs date de poste est obligatoire.")]
        public DateTime DatePoste { get; set; }
        [Required(ErrorMessage = "Le champs id du concerné est obligatoire.")]
        public int IdConcerne { get; set; }

        [ForeignKey("User")]
        public int IdUser { get; set; }
        [XmlIgnore, JsonIgnore]
        public virtual User User { get; set; }

    }
}