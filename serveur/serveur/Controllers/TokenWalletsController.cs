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
            if (pseudo.IsNullOrWhiteSpace() || mdp.IsNullOrWhiteSpace())
            {
                return BadRequest("L'identifiant de connexion ou le mot de passe n'a pas été fourni !");
            }

            // Find User in Database + Check password Hash
            User User = db.Users.FirstOrDefault(user => user.Pseudo == pseudo);
            if (BCrypt.CheckPassword(mdp, User.Mdp))
            {
                // Check if the user doesn't already have a token active and delete it if yes
                TokenWallet TokenWallet = db.TokenWallets.FirstOrDefault<TokenWallet>(tokw => tokw.IdUser == User.IdUser);
                if (TokenWallet != null)
                {
                    db.TokenWallets.Remove(TokenWallet);
                }

                // Create new token
                string Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                TokenWallet tw = new TokenWallet
                {
                    Token = Token,
                    User = User
                };

                db.TokenWallets.Add(tw);
                db.SaveChanges();

                return Ok(db.TokenWallets.FirstOrDefault(T => T.IdTokenWallet == tw.IdTokenWallet));
            }

            // END Find User in Database + Check password Hash
            return Unauthorized();
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
        public IHttpActionResult DeleteTokenWallet(string token)
        {
            TokenWallet TokenWallet = db.TokenWallets.FirstOrDefault<TokenWallet>(tokw => tokw.Token == token);
            if (TokenWallet == null)
            {
                return NotFound();
            }

            db.TokenWallets.Remove(TokenWallet);
            db.SaveChanges();

            return Ok(TokenWallet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /*private bool TokenWalletExists(int id)
        {
            return db.TokenWallets.Count(e => e.IdTokenWallet == id) > 0;
        }*/
    }
}