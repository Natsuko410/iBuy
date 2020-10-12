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
    public class EncheresController : ApiController
    {
        private IBuyContext db = new IBuyContext();

        // GET: api/Encheres
        public IQueryable<Enchere> GetEncheres()
        {
            return db.Encheres;
        }

        // GET: api/Encheres/5
        [ResponseType(typeof(Enchere))]
        public IHttpActionResult GetEnchere(int id)
        {
            Enchere enchere = db.Encheres.Find(id);
            if (enchere == null)
            {
                return NotFound();
            }

            return Ok(enchere);
        }

        // PUT: api/Encheres/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEnchere(int id, Enchere enchere)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != enchere.IdEnch)
            {
                return BadRequest();
            }

            db.Entry(enchere).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnchereExists(id))
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

        // POST: api/Encheres
        [ResponseType(typeof(Enchere))]
        public IHttpActionResult PostEnchere(Enchere enchere)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Encheres.Add(enchere);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = enchere.IdEnch }, enchere);
        }

        // DELETE: api/Encheres/5
        [ResponseType(typeof(Enchere))]
        public IHttpActionResult DeleteEnchere(int id)
        {
            Enchere enchere = db.Encheres.Find(id);
            if (enchere == null)
            {
                return NotFound();
            }

            db.Encheres.Remove(enchere);
            db.SaveChanges();

            return Ok(enchere);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EnchereExists(int id)
        {
            return db.Encheres.Count(e => e.IdEnch == id) > 0;
        }
    }
}