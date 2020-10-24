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
using serveur.Data;
using serveur.Models;
using serveur.Services;

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
        [ResponseType(typeof(Illustration[]))]
        public IHttpActionResult PostIllustration([FromUri] int idAnno)
        {
            try
            {
                if (!TokenService.IsTokenValid(db.TokenWallets))
                {
                    return Unauthorized();
                }

                if (db.Annonces.Count(a => a.IdAnno.Equals(idAnno)) < 1)
                {
                    return BadRequest("Le produit spécifié n'existe pas.");
                }

                List<Illustration> Illustrations = new List<Illustration>();
                var HttpRequest = HttpContext.Current.Request;
                
                foreach(string fileName in HttpRequest.Files)
                {
                    string Name = $"{idAnno}_{fileName}";
                    string physicalPath = $"~/Illustrations/{Name}";

                    Illustration Illustration = new Illustration
                    {
                        IdAnno = idAnno,
                        Path = physicalPath
                    };

                    HttpRequest.Files[fileName].SaveAs(
                        HttpContext.Current.Server.MapPath(physicalPath)
                    );

                    Illustrations.Add(Illustration);
                }

                db.Illustrations.AddRange(Illustrations);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = idAnno }, Illustrations);
            }
            catch
            {
                return InternalServerError();
            }
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