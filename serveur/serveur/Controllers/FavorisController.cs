using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using serveur.Data;
using serveur.Models;
using serveur.Services;

namespace serveur.Controllers
{
    public class FavorisController : ApiController
    {
        private IBuyContext db = new IBuyContext();

        // GET: api/Favoris
        public IHttpActionResult GetFavorisFromUser()
        {
            try
            {
                // Checks token validity
                int IdUser = TokenService.GetIdUserByToken(db.TokenWallets);
                if (IdUser.Equals(-1))
                {
                    return BadRequest("Vous n'avez pas accès à cette page.");
                }

                return Ok(db.Favoris.Where(f => f.IdUser == IdUser));
            }
            catch
            {
                return InternalServerError();
            }
        }

        // GET: api/Favoris/5
        [ResponseType(typeof(Favori))]
        public IHttpActionResult GetFavori(int id)
        {
            try
            {
                // Checks token validity
                int IdUser = TokenService.GetIdUserByToken(db.TokenWallets);
                if (IdUser.Equals(-1))
                {
                    return BadRequest("Vous n'avez pas accès à cette page.");
                }

                Favori favori = db.Favoris.Find(id);
                if (favori == null)
                {
                    return NotFound();
                }

                return Ok(favori);
            }
            catch
            {
                return InternalServerError();
            }
        }

        // PUT: api/Favoris/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFavori()
        {
            return Unauthorized();
        }

        // POST: api/Favoris
        [ResponseType(typeof(Favori))]
        public IHttpActionResult PostFavori(Favori favori)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest($"{String.Join(" ", ModelState.Keys.First().Split('.')).ToLower()} est vide ou mal défini."
                                + $" {ModelState.Values.Select(x => x.Errors).First().First().ErrorMessage}"
                            );
                }

                // Checks token validity
                if (!TokenService.IsTokenValid(db.TokenWallets))
                {
                    return Unauthorized();
                }

                // Checks if this favori isn't already in the table 
                if (db.Favoris.Count(f => f.IdUser.Equals(favori.IdUser) && f.IdAnno.Equals(favori.IdAnno)) > 0)
                {
                    return BadRequest("Ce favoris est déjà présent.");
                }

                favori.Annonce = db.Annonces.Find(favori.IdAnno);

                db.Favoris.Add(favori);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = favori.IdFavo }, favori);

            }catch
            {
                return InternalServerError();
            }
        }

        // DELETE: api/Favoris/5
        [ResponseType(typeof(Favori))]
        public IHttpActionResult DeleteFavori(int id)
        {
            try
            {
                // Checks token validity
                int IdUser = TokenService.GetIdUserByToken(db.TokenWallets);
                if (IdUser.Equals(-1))
                {
                    return Unauthorized();
                }

                //Find favori
                Favori favori = db.Favoris.Find(id);
                if (favori == null)
                {
                    return NotFound();
                }

                if (!favori.IdUser.Equals(IdUser))
                {
                    return Unauthorized();
                }

                db.Favoris.Remove(favori);
                db.SaveChanges();

                return Ok(favori);
            }
            catch
            {
                return InternalServerError();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FavoriExists(int id)
        {
            return db.Favoris.Count(e => e.IdFavo == id) > 0;
        }
    }
}