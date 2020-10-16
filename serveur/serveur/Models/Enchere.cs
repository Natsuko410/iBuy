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
    public class Enchere
    {
        [Key]
        public int IdEnch { get; set; }
        [Required(ErrorMessage = "Le champs montant minimum est obligatoire")]
        public double MontantMin { get; set; }
        [Required(ErrorMessage = "Le champs date de début est obligatoire")]
        public DateTime DateDebut { get; set; }
        [Required(ErrorMessage = "Le champs date de fin est obligatoire")]
        public DateTime DateFin { get; set; }

        [ForeignKey("Annonce")]
        public int IdAnno { get; set; }
        public virtual Annonce Annonce { get; set; }
    }
}