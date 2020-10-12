using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace serveur.Models
{
    public class Token
    {
        [Key]
        public int IdToken { get; set; }

        public int IdUser { get; set; }
    }
}