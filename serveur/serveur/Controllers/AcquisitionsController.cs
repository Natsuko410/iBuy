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
    public class AcquisitionsController : ApiController
    {
        private IBuyContext db = new IBuyContext();

        // GET: api/Acquisitions
        public IQueryable<Acquisition> GetAcquisitions()
        {
            return db.Acquisitions;
        }

        // GET: api/Acquisitions/5
        [ResponseType(typeof(Acquisition))]
        public IHttpActionResult GetAcquisition(int id)
        {
            Acquisition acquisition = db.Acquisitions.Find(id);
            if (acquisition == null)
            {
                return NotFound();
            }

            return Ok(acquisition);
        }

        // PUT: api/Acquisitions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAcquisition(int id, Acquisition acquisition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != acquisition.IdAcqu)
            {
                return BadRequest();
            }

            db.Entry(acquisition).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AcquisitionExists(id))
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

        // POST: api/Acquisitions
        [ResponseType(typeof(Acquisition))]
        public IHttpActionResult PostAcquisition(Acquisition acquisition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Acquisitions.Add(acquisition);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = acquisition.IdAcqu }, acquisition);
        }

        // DELETE: api/Acquisitions/5
        [ResponseType(typeof(Acquisition))]
        public IHttpActionResult DeleteAcquisition(int id)
        {
            Acquisition acquisition = db.Acquisitions.Find(id);
            if (acquisition == null)
            {
                return NotFound();
            }

            db.Acquisitions.Remove(acquisition);
            db.SaveChanges();

            return Ok(acquisition);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AcquisitionExists(int id)
        {
            return db.Acquisitions.Count(e => e.IdAcqu == id) > 0;
        }
    }
}