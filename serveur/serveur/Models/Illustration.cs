using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace serveur.Models
{
    public class Illustration
    {
        [Key]
        public int IdIllu { get; set; }
        [
         Required(ErrorMessage = "Le champs path est obligatoire"),
         MaxLength(2048, ErrorMessage ="Ce champs ne peut pas excéder 2048 caractères")
        ]
        public string Path { get; set; }
    }
}