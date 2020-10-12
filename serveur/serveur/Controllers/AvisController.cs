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
    public class AvisController : ApiController
    {
        private IBuyContext db = new IBuyContext();

        // GET: api/Avis
        public IQueryable<Avis> GetAvis()
        {
            return db.Avis;
        }

        // GET: api/Avis/5
        [ResponseType(typeof(Avis))]
        public IHttpActionResult GetAvis(int id)
        {
            Avis avis = db.Avis.Find(id);
            if (avis == null)
            {
                return NotFound();
            }

            return Ok(avis);
        }

        // PUT: api/Avis/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAvis(int id, Avis avis)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != avis.IdAvis)
            {
                return BadRequest();
            }

            db.Entry(avis).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AvisExists(id))
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

        // POST: api/Avis
        [ResponseType(typeof(Avis))]
        public IHttpActionResult PostAvis(Avis avis)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Avis.Add(avis);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = avis.IdAvis }, avis);
        }

        // DELETE: api/Avis/5
        [ResponseType(typeof(Avis))]
        public IHttpActionResult DeleteAvis(int id)
        {
            Avis avis = db.Avis.Find(id);
            if (avis == null)
            {
                return NotFound();
            }

            db.Avis.Remove(avis);
            db.SaveChanges();

            return Ok(avis);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AvisExists(int id)
        {
            return db.Avis.Count(e => e.IdAvis == id) > 0;
        }
    }
}