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
        public string Path { get; set; }
    }
}