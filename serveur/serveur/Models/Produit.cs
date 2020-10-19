using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace serveur.Models
{
    public class Produit
    {
        [Key]
        public int IdProd { get; set; }
        [
         Required(ErrorMessage = "Le champs nom est obligatoire."),
         MaxLength(512, ErrorMessage = "ce champs ne peut pas excédé 512 caractères.")
        ]
        public string Nom { get; set; }
        [
         MaxLength(1024, ErrorMessage = "ce champs ne peut pas excédé 1024 caractères.")
        ]
        public string Description { get; set; }
        [
         Required(ErrorMessage = "Le champs nom est obligatoire."),
         MaxLength(56, ErrorMessage = "ce champs ne peut pas excédé 56 caractères.")
        ]
        public string Etat { get; set; }

        [ForeignKey("Categorie")]
        public int? IdCat { get; set; }
        public virtual Categorie Categorie { get; set; }

    }
}