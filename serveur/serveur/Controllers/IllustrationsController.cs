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
    public class IllustrationsController : ApiController
    {
        private IBuyContext db = new IBuyContext();

        // GET: api/Illustrations
        public IQueryable<Illustration> GetIllustrations()
        {
            return db.Illustrations;
        }

        // GET: api/Illustrations/5
        [ResponseType(typeof(Illustration))]
        public IHttpActionResult GetIllustration(int id)
        {
            Illustration illustration = db.Illustrations.Find(id);
            if (illustration == null)
            {
                return NotFound();
            }

            return Ok(illustration);
        }

        // PUT: api/Illustrations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIllustration(int id, Illustration illustration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != illustration.IdIllu)
            {
                return BadRequest();
            }

            db.Entry(illustration).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IllustrationExists(id))
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

        // POST: api/Illustrations
        [ResponseType(typeof(Illustration))]
        public IHttpActionResult PostIllustration(Illustration illustration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Illustrations.Add(illustration);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = illustration.IdIllu }, illustration);
        }

        // DELETE: api/Illustrations/5
        [ResponseType(typeof(Illustration))]
        public IHttpActionResult DeleteIllustration(int id)
        {
            Illustration illustration = db.Illustrations.Find(id);
            if (illustration == null)
            {
                return NotFound();
            }

            db.Illustrations.Remove(illustration);
            db.SaveChanges();

            return Ok(illustration);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IllustrationExists(int id)
        {
            return db.Illustrations.Count(e => e.IdIllu == id) > 0;
        }
    }
}