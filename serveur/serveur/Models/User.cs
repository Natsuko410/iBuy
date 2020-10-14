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
    public class User
    {
        [Key]
        public int IdUser { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Pseudo { get; set; }
        public string Email { get; set; }
        public string Mdp { get; set; }
        public string Tel { get; set; }
        public string AddrPays { get; set; }
        public string AddrLocalite { get; set; }
        public string AddrCodePostal { get; set; }
        public string AddrRue { get; set; }
        public string AddrNumero { get; set; }

    }
}