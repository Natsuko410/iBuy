﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace serveur.Models
{
    public class Annonce
    {
        [Key]
        public int IdAnno { get; set; }
        [
         Required(ErrorMessage = "Le champs titre est obligatoire."),
         MinLength(8, ErrorMessage = "ce champs doit faire au moins 8 caractères."),
         MaxLength(50, ErrorMessage = "ce champs ne peut pas excédé 50 caractères.")
        ]
        public string Titre { get; set; }
        [Required(ErrorMessage = "Le champs montant minimum est obligatoire.")] 
        public double MontantMin { get; set; }
        [Required(ErrorMessage = "Le champs date d'ajout est obligatoire.")]
        public DateTime DateAjout { get; set; }
        [
         Required(ErrorMessage = "Le champs état est obligatoire."),
         MaxLength(32, ErrorMessage = "ce champs ne peut pas excédé 32 caractères.")
        ]
        public string EtatAnno { get; set; }

        [Required(ErrorMessage = "Le champs est une enchère est obligatoire.")]
        public bool IsEnchere { get; set; }


        [
         Required(ErrorMessage = "Le champs nom est obligatoire."),
         MaxLength(512, ErrorMessage = "ce champs ne peut pas excédé 512 caractères.")
        ]
        public string NomProd { get; set; }
        [
         MaxLength(1024, ErrorMessage = "ce champs ne peut pas excédé 1024 caractères.")
        ]
        public string DescriptionProd { get; set; }
        [
         Required(ErrorMessage = "Le champs nom est obligatoire."),
         MaxLength(56, ErrorMessage = "ce champs ne peut pas excédé 56 caractères.")
        ]
        public string EtatProd { get; set; }

        [ForeignKey("Categorie")]
        public int? IdCat { get; set; }
        public virtual Categorie Categorie { get; set; }

        [ForeignKey("User")]
        public int? IdUser { get; set; }
        [XmlIgnore, JsonIgnore]
        public virtual User User { get; set; }

    }
}