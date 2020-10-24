using Microsoft.Ajax.Utilities;
using serveur.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.WebPages;

namespace serveur.Services
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

        public static int GetIdUserByToken(DbSet<TokenWallet> TokenWallets)
        {

            string Token = HttpContext.Current.Request.Headers.Get("x-auth-token");

            if (Token.IsNullOrWhiteSpace())
            {
                return -1;
            }

            // Checks token validity
            TokenWallet TokenWallet = TokenWallets.FirstOrDefault(t => t.Token == Token);
            if (TokenWallet == null || TokenWallet.Token != Token)
            {
                return -1;
            }

            return TokenWallet.IdUser;
        }
    }
}