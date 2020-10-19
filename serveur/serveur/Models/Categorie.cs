using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace serveur.Models
{
    public class Categorie
    {
        [Key]
        public int IdCat { get; set; }
        [
         Required(ErrorMessage = "Le champs nom est obligatoire."),
         MaxLength(32, ErrorMessage = "ce champs ne peut pas excédé 32 caractères.")
        ]
        public string Nom { get; set; }
        [
         Required(ErrorMessage = "Le champs description est obligatoire."),
         MaxLength(256, ErrorMessage = "ce champs ne peut pas excédé 256 caractères.")
        ]
        public string Desc { get; set; }
    }
}