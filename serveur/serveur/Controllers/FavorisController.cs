using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
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
            Favori favori = db.Favoris.Find(id);
            if (favori == null)
            {
                return NotFound();
            }

            return Ok(favori);
        }

        // PUT: api/Favoris/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFavori(int id, Favori favori)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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
        }

        // POST: api/Favoris
        [ResponseType(typeof(Favori))]
        public IHttpActionResult PostFavori(Favori favori)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Favoris.Add(favori);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = favori.IdFavo }, favori);
        }

        // DELETE: api/Favoris/5
        [ResponseType(typeof(Favori))]
        public IHttpActionResult DeleteFavori(int id)
        {
            Favori favori = db.Favoris.Find(id);
            if (favori == null)
            {
                return NotFound();
            }

            db.Favoris.Remove(favori);
            db.SaveChanges();

            return Ok(favori);
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