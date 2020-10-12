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
    public class AchatsController : ApiController
    {
        private IBuyContext db = new IBuyContext();

        // GET: api/Achats
        public IQueryable<Achat> GetAchats()
        {
            return db.Achats;
        }

        // GET: api/Achats/5
        [ResponseType(typeof(Achat))]
        public IHttpActionResult GetAchat(int id)
        {
            Achat achat = db.Achats.Find(id);
            if (achat == null)
            {
                return NotFound();
            }

            return Ok(achat);
        }

        // PUT: api/Achats/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAchat(int id, Achat achat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != achat.IdAchat)
            {
                return BadRequest();
            }

            db.Entry(achat).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AchatExists(id))
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

        // POST: api/Achats
        [ResponseType(typeof(Achat))]
        public IHttpActionResult PostAchat(Achat achat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Achats.Add(achat);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = achat.IdAchat }, achat);
        }

        // DELETE: api/Achats/5
        [ResponseType(typeof(Achat))]
        public IHttpActionResult DeleteAchat(int id)
        {
            Achat achat = db.Achats.Find(id);
            if (achat == null)
            {
                return NotFound();
            }

            db.Achats.Remove(achat);
            db.SaveChanges();

            return Ok(achat);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AchatExists(int id)
        {
            return db.Achats.Count(e => e.IdAchat == id) > 0;
        }
    }
}