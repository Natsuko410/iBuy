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
        public string Nom { get; set; }
        public string Desc { get; set; }
    }
}