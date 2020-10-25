using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Ajax.Utilities;
using serveur.Data;
using serveur.Models;

namespace serveur.Controllers
{
    public class TokenWalletsController : ApiController
    {
        private IBuyContext db = new IBuyContext();

        // GET: api/TokenWallets
        public IHttpActionResult GetTokens([FromUri] string pseudo, [FromUri] string mdp)
        {
            try
            {
                if (pseudo.IsNullOrWhiteSpace() || mdp.IsNullOrWhiteSpace())
                {
                    return BadRequest("L'identifiant de connexion ou le mot de passe n'a pas été fourni !");
                }

                // Finds User in Database + Checks password Hash
                User User = db.Users.FirstOrDefault<User>(user => user.Pseudo == pseudo);
                if (User != null && BCrypt.CheckPassword(mdp, User.Mdp))
                {
                    // Checks if the user doesn't already have a token active and delete it if yes
                    TokenWallet TokenWallet = db.TokenWallets.FirstOrDefault<TokenWallet>(tokw => tokw.IdUser == User.IdUser);
                    if (TokenWallet != null)
                    {
                        db.TokenWallets.Remove(TokenWallet);
                    }

                    // Creates new token
                    string Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                    TokenWallet tw = new TokenWallet
                    {
                        Token = Token,
                        User = User
                    };

                    db.TokenWallets.Add(tw);
                    db.SaveChanges();

                    return Ok(db.TokenWallets.FirstOrDefault(t => t.IdTokenWallet == tw.IdTokenWallet));
                }

                // END Find User in Database + Check password Hash
                return Unauthorized();
            }
            catch
            {
                return InternalServerError();
            }
        }

        // GET: api/TokenWallets/5
        [ResponseType(typeof(TokenWallet))]
        public IHttpActionResult GetTokenWallet(int id)
        {
            return Unauthorized();
        }

        // PUT: api/TokenWallets
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTokenWallet()
        {
            return Unauthorized();
        }

        // POST: api/TokenWallets
        [ResponseType(typeof(TokenWallet))]
        public IHttpActionResult PostTokenWallet(TokenWallet tokenWallet)
        {
            return Unauthorized();
        }

        // DELETE: api/TokenWallets/AxdfegRghyTr...
        [ResponseType(typeof(TokenWallet))]
        public IHttpActionResult DeleteTokenWallet(int id)
        {
            return Unauthorized();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TokenWalletExists(int id)
        {
            return db.TokenWallets.Count(e => e.IdTokenWallet == id) > 0;
        }
    }
}