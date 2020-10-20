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

namespace serveur.Controllers
{
    public class AnnoncesController : ApiController
    {
        private IBuyContext db = new IBuyContext();

        // GET: api/Annonces
        // ?nom=str                             nom d'un article 
        // ?idCat=num                           id de catégorie de produit 
        // ?limit=num                           nb d'article par page
        // ?offset=num                          indice de la page 
        // ?typeDeVente=enchere                 type de vente
        public IQueryable<Annonce> GetAnnonces([FromUri] int idCat = 0, [FromUri] int limit = 20, [FromUri] int offset = 0, [FromUri] String typeDeVente = "", [FromUri] String nomRecherche = "")
        {
            Uri Uri = Request.RequestUri;
            IQueryable<Annonce> Annonces = null;

            switch (typeDeVente)
            {
                case "enchere":
                    if (idCat != 0)
                        Annonces = db.Annonces.Where(anno => anno.Etat == "vente" && anno.IsEnchere == true && anno.Produit.IdCat == idCat && anno.Produit.Nom.Contains(nomRecherche)).OrderBy(anno => anno.IdAnno).Skip(limit * offset).Take(limit);
                    else
                        Annonces = db.Annonces.Where(anno => anno.Etat == "vente" && anno.IsEnchere == true && anno.Produit.Nom.Contains(nomRecherche)).OrderBy(anno => anno.IdAnno).Skip(limit * offset).Take(limit);

                    break;

                default:
                    if (idCat != 0)
                        Annonces = db.Annonces.Where(anno => anno.Etat == "vente" && anno.Produit.IdCat == idCat && anno.Produit.Nom.Contains(nomRecherche)).OrderBy(anno => anno.IdAnno).Skip(limit * offset).Take(limit);
                    else
                        Annonces = db.Annonces.Where(anno => anno.Etat == "vente" && anno.Produit.Nom.Contains(nomRecherche)).OrderBy(anno => anno.IdAnno).Skip(limit * offset).Take(limit);

                    break;
            }

            return Annonces;
        }

        // GET: api/Annonces/5
        [ResponseType(typeof(Annonce))]
        public IHttpActionResult GetAnnonce(int id)
        {
            Annonce annonce = db.Annonces.Find(id);
            if (annonce == null)
            {
                return NotFound();
            }

            return Ok(annonce);
        }

        // PUT: api/Annonces/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAnnonce(int id, Annonce annonce)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != annonce.IdAnno)
            {
                return BadRequest();
            }

            db.Entry(annonce).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnnonceExists(id))
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

        // POST: api/Annonces
        [ResponseType(typeof(Annonce))]
        public IHttpActionResult PostAnnonce(Annonce annonce)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Annonces.Add(annonce);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = annonce.IdAnno }, annonce);
        }

        // DELETE: api/Annonces/5
        [ResponseType(typeof(Annonce))]
        public IHttpActionResult DeleteAnnonce(int id)
        {
            Annonce annonce = db.Annonces.Find(id);
            if (annonce == null)
            {
                return NotFound();
            }

            db.Annonces.Remove(annonce);
            db.SaveChanges();

            return Ok(annonce);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AnnonceExists(int id)
        {
            return db.Annonces.Count(e => e.IdAnno == id) > 0;
        }
    }
}