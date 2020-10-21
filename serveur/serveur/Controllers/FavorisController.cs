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
using SecureAPIExemple.Services;
using serveur.Data;
using serveur.Models;

namespace serveur.Controllers
{
    public class FavorisController : ApiController
    {
        private IBuyContext db = new IBuyContext();

        // GET: api/Favoris
        public IQueryable<Favori> GetFavoris()
        {
            return db.Favoris;
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
        public IHttpActionResult PutFavori(int id, Favori favori)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Checks token validity
                int IdUser = TokenService.GetIdUserByToken(db.TokenWallets);
                if (IdUser.Equals(-1))
                {
                    return BadRequest("Vous n'avez pas accès à cette page.");
                }

                if (id != favori.IdFavo)
                {
                    return BadRequest();
                }

                db.Entry(favori).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FavoriExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return StatusCode(HttpStatusCode.NoContent);
            }catch
            {
                return InternalServerError();
            }
        }

            

        // POST: api/Favoris
        [ResponseType(typeof(Favori))]
        public IHttpActionResult PostFavori(Favori favori)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Checks token validity
                int IdUser = TokenService.GetIdUserByToken(db.TokenWallets);
                if (IdUser.Equals(-1))
                {
                    return BadRequest("Vous n'avez pas accès à cette page.");
                }

                // Checks if this favori isn't already in the table 
                if (db.Favoris.Count(u => u.IdUser.Equals(favori.IdUser) && u.IdAnno.Equals(favori.IdAnno)) > 0)
                {
                    return BadRequest("Ce favoris est déjà présent.");
                }

                db.Favoris.Add(favori);
                db.SaveChanges();
            }catch
            {
                return InternalServerError();
            }
            

            return CreatedAtRoute("DefaultApi", new { id = favori.IdFavo }, favori);
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
                    return BadRequest("Vous n'avez pas accès à cette page.");
                }

                //Find favori
                Favori favori = db.Favoris.Find(id);
                if (favori == null)
                {
                    return NotFound();
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