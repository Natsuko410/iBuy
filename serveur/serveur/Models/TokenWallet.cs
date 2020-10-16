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
    public class TokenWallet
    {
        [Key]
        public int IdTokenWallet { get; set; }
        [
         Required,
        ]
        public string Token { get; set; }

        [ForeignKey("User")]
        public int IdUser { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual User User { get; set; }
    }
}