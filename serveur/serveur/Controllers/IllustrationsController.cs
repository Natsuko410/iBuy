using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
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
        public HttpResponseMessage GetIllustration(int id)
        {
            Illustration Illustration = db.Illustrations.Find(id);
            if (Illustration == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var response = Request.CreateResponse(HttpStatusCode.OK);
            var fileStream = new FileStream(HttpContext.Current.Server.MapPath(Illustration.Path), FileMode.Open, FileAccess.Read);
            response.Content = new StreamContent(fileStream);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");

            return response;
        }

        // PUT: api/Illustrations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIllustration()
        {
            return Unauthorized();
        }

        // POST: api/Illustrations
        [ResponseType(typeof(Illustration[]))]
        public IHttpActionResult PostIllustration(int id)
        {
            try
            {
                if (!TokenService.IsTokenValid(db.TokenWallets))
                {
                    return Unauthorized();
                }

                if (db.Annonces.Count(a => a.IdAnno.Equals(id)) < 1)
                {
                    return BadRequest("L'annonce spécifiée n'existe pas.");
                }

                List<Illustration> Illustrations = new List<Illustration>();
                var HttpRequest = HttpContext.Current.Request;
                
                foreach(string fileName in HttpRequest.Files)
                {
                    string Name = $"{id}_{fileName}";
                    string physicalPath = $"~/Illustrations/{Name}";

                    Illustration Illustration = new Illustration
                    {
                        IdAnno = id,
                        Path = physicalPath
                    };

                    HttpRequest.Files[fileName].SaveAs(
                        HttpContext.Current.Server.MapPath(physicalPath)
                    );

                    Illustrations.Add(Illustration);
                }

                db.Illustrations.AddRange(Illustrations);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { Id = id }, Illustrations);
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
            try
            {
                if (!TokenService.IsTokenValid(db.TokenWallets))
                {
                    return Unauthorized();
                }

                Illustration illustration = db.Illustrations.Find(id);
                if (illustration == null)
                {
                    return NotFound();
                }

                File.Delete(HttpContext.Current.Server.MapPath(illustration.Path));

                db.Illustrations.Remove(illustration);
                db.SaveChanges();

                return Ok(illustration);
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

        private bool IllustrationExists(int id)
        {
            return db.Illustrations.Count(e => e.IdIllu == id) > 0;
        }
    }
}