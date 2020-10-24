using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
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
    public class AnnoncesController : ApiController
    {
        private IBuyContext db = new IBuyContext();

        // GET: api/Annonces
        // ?nom=str                             nom d'un article 
        // ?idCat=num                           id de catégorie de produit 
        // ?limit=num                           nb d'article par page
        // ?offset=num                          indice de la page 
        // ?typeDeVente=enchere                 type de vente
        // ?idUser=num                          id d'un user
        // ?etat:str                            l'état des ventes (vente, vendu, fini, ...)
        [ResponseType(typeof(DataAnnonce[]))]
        public IHttpActionResult GetAnnonces([FromUri] int idCat = 0, [FromUri] int limit = 20, [FromUri] int offset = 0, [FromUri] String typeDeVente = "", [FromUri] String nomRecherche = "", [FromUri] int idUser = 0, [FromUri] String etat = "vente")
        {
            IQueryable<Annonce> Annonces = null;

            if (typeDeVente == "enchere")
            {
                if (idCat != 0)
                {
                    if (idUser != 0)
                        Annonces =
                            db.Annonces.Where(anno =>
                                anno.IsEnchere == true
                                && anno.IdUser == idUser
                                && anno.IdCat == idCat
                                && anno.EtatAnno.Contains(etat)
                                && anno.NomProd.Contains(nomRecherche)
                            )
                            .OrderBy(anno => anno.IdAnno)
                            .Skip(limit * offset).Take(limit);
                    else
                        Annonces =
                            db.Annonces.Where(anno =>
                                anno.IsEnchere == true
                                && anno.IdCat == idCat
                                && anno.EtatAnno.Contains(etat)
                                && anno.NomProd.Contains(nomRecherche)
                            )
                            .OrderBy(anno => anno.IdAnno)
                            .Skip(limit * offset).Take(limit);
                }
                else
                {
                    if (idUser != 0)
                        Annonces =
                            db.Annonces.Where(anno =>
                                anno.IsEnchere == true
                                && anno.IdUser == idUser
                                && anno.EtatAnno.Contains(etat)
                                && anno.NomProd.Contains(nomRecherche)
                            )
                            .OrderBy(anno => anno.IdAnno)
                            .Skip(limit * offset).Take(limit);
                    else
                        Annonces =
                            db.Annonces.Where(anno =>
                                anno.IsEnchere == true
                                && anno.EtatAnno.Contains(etat)
                                && anno.NomProd.Contains(nomRecherche)
                            )
                            .OrderBy(anno => anno.IdAnno)
                            .Skip(limit * offset).Take(limit);
                }
            }
            else
            {
                if (idCat != 0)
                {
                    if (idUser != 0)
                        Annonces =
                            db.Annonces.Where(anno =>
                                anno.IdUser == idUser
                                && anno.IdCat == idCat
                                && anno.EtatAnno.Contains(etat)
                                && anno.NomProd.Contains(nomRecherche)
                            )
                            .OrderBy(anno => anno.IdAnno)
                            .Skip(limit * offset).Take(limit);
                    else
                        Annonces =
                            db.Annonces.Where(anno =>
                                anno.IdCat == idCat
                                && anno.EtatAnno.Contains(etat)
                                && anno.NomProd.Contains(nomRecherche)
                            )
                            .OrderBy(anno => anno.IdAnno)
                            .Skip(limit * offset).Take(limit);
                }
                else
                {
                    if (idUser != 0)
                        Annonces =
                            db.Annonces.Where(anno =>
                                anno.IdUser == idUser
                                && anno.EtatAnno.Contains(etat)
                                && anno.NomProd.Contains(nomRecherche)
                            )
                            .OrderBy(anno => anno.IdAnno)
                            .Skip(limit * offset).Take(limit);
                    else
                        Annonces =
                            db.Annonces.Where(anno =>
                                anno.EtatAnno.Contains(etat)
                                && anno.NomProd.Contains(nomRecherche)
                            )
                            .OrderBy(anno => anno.IdAnno)
                            .Skip(limit * offset).Take(limit);
                }
            }

            string BaseUrl = HttpContext.Current.Request.Url.AbsoluteUri;
            List<DataAnnonce> DataAnnonces = new List<DataAnnonce>();
            
            foreach(Annonce annonce in Annonces)
            {
                List<string> IllustrationsUrl = new List<string>();
                IQueryable<Illustration> Illustrations = db.Illustrations.Where(i => i.IdAnno == annonce.IdAnno);

                foreach (Illustration illustration in Illustrations)
                {
                    IllustrationsUrl.Add($"{BaseUrl}/api/Illustrations?idIllu={illustration.IdIllu}");
                }

                DataAnnonces.Add(new DataAnnonce
                {
                    Annonce = annonce,
                    UrlIllustrations = IllustrationsUrl.ToArray()
                });

            }

            return Ok(DataAnnonces);
        }

        // GET: api/Annonces/5
        [ResponseType(typeof(DataAnnonce))]
        public IHttpActionResult GetAnnonce(int id)
        {
            Annonce Annonce = db.Annonces.Find(id);
            if (Annonce == null)
            {
                return NotFound();
            }

            string BaseUrl = HttpContext.Current.Request.Url.AbsoluteUri;
            List<string> IllustrationsUrl = new List<string>();
            IQueryable<Illustration> Illustrations = db.Illustrations.Where(i => i.IdAnno == id);

            foreach (Illustration illustration in Illustrations)
            {
                IllustrationsUrl.Add($"{BaseUrl}/api/Illustrations?idIllu={illustration.IdIllu}");
            }

            return Ok(new DataAnnonce 
            {
                Annonce = Annonce,
                UrlIllustrations = IllustrationsUrl.ToArray()
            });
        }

        // PUT: api/Annonces/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAnnonce([FromUri] int id, [FromBody] Annonce annonce)
        {
            // Checks token validity
            int IdUser = TokenService.GetIdUserByToken(db.TokenWallets);
            if (IdUser.Equals(-1))
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest($"{String.Join(" ", ModelState.Keys.First().Split('.')).ToLower()} est vide ou mal défini."
                            + $" {ModelState.Values.Select(x => x.Errors).First().First().ErrorMessage}"
                        );
            }

            if (IdUser != annonce.IdUser)
            {
                return Unauthorized();
            }

            if (id != annonce.IdAnno)
            {
                return BadRequest("L'identifiant de l'annonce ne correspond pas avec l'identifiant donné.");
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
        public IHttpActionResult PostAnnonce([FromBody] Annonce annonce)
        {
            try
            {
                // Checks token validity
                int IdUser = TokenService.GetIdUserByToken(db.TokenWallets);
                if (IdUser.Equals(-1))
                {
                    return Unauthorized();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest($"{String.Join(" ", ModelState.Keys.First().Split('.')).ToLower()} est vide ou mal défini."
                                + $" {ModelState.Values.Select(x => x.Errors).First().First().ErrorMessage}"
                            );
                }

                if (IdUser != annonce.IdUser)
                {
                    return Unauthorized();
                }

                db.Annonces.Add(annonce);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = annonce.IdAnno }, annonce);
            }
            catch
            {
                return InternalServerError();
            }
        }

        // DELETE: api/Annonces/5
        [ResponseType(typeof(Annonce))]
        public IHttpActionResult DeleteAnnonce(int id)
        {
            try
            {
                Annonce annonce = db.Annonces.Find(id);

                // Checks token validity
                int IdUser = TokenService.GetIdUserByToken(db.TokenWallets);
                if (IdUser.Equals(-1) || IdUser != annonce.IdUser)
                {
                    return Unauthorized();
                }

                if (annonce == null)
                {
                    return NotFound();
                }

                IQueryable<Illustration> Illustrations = db.Illustrations.Where(i => i.IdAnno == annonce.IdAnno);

                foreach(Illustration illustration in Illustrations)
                {
                    File.Delete(HttpContext.Current.Server.MapPath(illustration.Path));
                }

                db.Annonces.Remove(annonce);
                db.SaveChanges();

                return Ok(annonce);
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

        private bool AnnonceExists(int id)
        {
            return db.Annonces.Count(e => e.IdAnno == id) > 0;
        }
    }
}