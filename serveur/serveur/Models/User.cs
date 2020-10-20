using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace serveur.Models
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }
        [
         Required(ErrorMessage = "Le champs nom est obligatoire."),
         MaxLength(90, ErrorMessage = "ce champs ne peut pas excédé 90 caractères.")
        ]
        public string Nom { get; set; }
        [
         Required(ErrorMessage = "Le champs prénom est obligatoire."),
         MaxLength(90, ErrorMessage = "ce champs ne peut pas excédé 90 caractères.")
        ]
        public string Prenom { get; set; }
        [
         Required(ErrorMessage = "Le champs pseudo est obligatoire."),
         MinLength(8, ErrorMessage = "ce champs doit faire au moins 8 caractères."),
         MaxLength(48, ErrorMessage = "ce champs ne peut pas excédé 48 caractères.")
        ]
        public string Pseudo { get; set; }
        [DefaultValue(0)]
        public double MoyenneNote { get; set; }
        [
         Required(ErrorMessage = "Le champs email est obligatoire."),
         MaxLength(320, ErrorMessage = "ce champs ne peut pas excédé 320 caractères.")
        ]
        public string Email { get; set; }
        [
         Required(ErrorMessage = "Le champs mot de passe est obligatoire."),
         MinLength(8, ErrorMessage = "ce champs doit faire au moins 8 caractères."),
         MaxLength(1024, ErrorMessage = "ce champs ne peut pas excédé 90 caractères.")
        ]
        public string Mdp { get; set; }
        [
         MaxLength(16, ErrorMessage = "ce champs ne peut pas excédé 16 caractères.")
        ]
        public string Tel { get; set; }
        [
         Required(ErrorMessage = "Le champs pays est obligatoire."),
         MaxLength(90, ErrorMessage = "ce champs ne peut pas excédé 16 caractères.")
        ]
        public string AddrPays { get; set; }
        [
         Required(ErrorMessage = "Le champs localité est obligatoire."),
         MaxLength(90, ErrorMessage = "ce champs ne peut pas excédé 16 caractères.")
        ]
        public string AddrLocalite { get; set; }
        [
         Required(ErrorMessage = "Le champs code postal est obligatoire."),
         MaxLength(16, ErrorMessage = "ce champs ne peut pas excédé 16 caractères.")
        ]
        public string AddrCodePostal { get; set; }
        [
         Required(ErrorMessage = "Le champs rue est obligatoire."),
         MaxLength(255, ErrorMessage = "ce champs ne peut pas excédé 16 caractères.")
        ]
        public string AddrRue { get; set; }
        [
         Required(ErrorMessage = "Le champs numéro est obligatoire."),
         MaxLength(16, ErrorMessage = "ce champs ne peut pas excédé 16 caractères.")
        ]
        public string AddrNumero { get; set; }

    }
}