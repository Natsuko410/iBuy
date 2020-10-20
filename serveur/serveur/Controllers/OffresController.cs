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
    public class OffresController : ApiController
    {
        private IBuyContext db = new IBuyContext();

        // GET: api/Offres
        public IQueryable<Offre> GetOffres()
        {
            return db.Offres;
        }

        // GET: api/Offres/5
        [ResponseType(typeof(Offre))]
        public IHttpActionResult GetOffre(int id)
        {
            Offre offre = db.Offres.Find(id);
            if (offre == null)
            {
                return NotFound();
            }

            return Ok(offre);
        }

        // PUT: api/Offres/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOffre(int id, Offre offre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != offre.IdOffr)
            {
                return BadRequest();
            }

            db.Entry(offre).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OffreExists(id))
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

        // POST: api/Offres
        [ResponseType(typeof(Offre))]
        public IHttpActionResult PostOffre(Offre offre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Offres.Add(offre);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = offre.IdOffr }, offre);
        }

        // DELETE: api/Offres/5
        [ResponseType(typeof(Offre))]
        public IHttpActionResult DeleteOffre(int id)
        {
            Offre offre = db.Offres.Find(id);
            if (offre == null)
            {
                return NotFound();
            }

            db.Offres.Remove(offre);
            db.SaveChanges();

            return Ok(offre);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OffreExists(int id)
        {
            return db.Offres.Count(e => e.IdOffr == id) > 0;
        }
    }
}