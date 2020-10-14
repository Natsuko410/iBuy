using serveur.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.WebPages;

namespace SecureAPIExemple.Services
{
    public class TokenService
    {

        public static bool IsTokenValid(DbSet<TokenWallet> TokenWallets)
        {
            string Token = HttpContext.Current.Request.Headers.Get("x-auth-token");
            if (Token.IsEmpty())
            {
                return false;
            }

            TokenWallet tw = TokenWallets.FirstOrDefault(T => T.Token == Token);
            if (tw == null)
            {
                return false;
            }
            return true;
        }
    }
}