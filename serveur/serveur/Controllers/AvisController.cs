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
using SecureAPIExemple.Services;
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
        public IHttpActionResult PutAvis([FromUri] int id, [FromBody] Avis avis)
        {

            // Checks token validity
            int IdUser = TokenService.GetIdUserByToken(db.TokenWallets);
            if (IdUser.Equals(-1) || IdUser != avis.IdUser)
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest($"{String.Join(" ", ModelState.Keys.First().Split('.')).ToLower()} est vide ou mal défini."
                            + $" {ModelState.Values.Select(x => x.Errors).First().First().ErrorMessage}"
                        );
            }

            if (id != avis.IdAvis)
            {
                return BadRequest("L'identifiant de l'avis ne correspond pas avec l'identifiant donné.");
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

            UpdateUserMoyenneNote(avis.IdConcerne);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Avis
        [ResponseType(typeof(Avis))]
        public IHttpActionResult PostAvis(Avis avis)
        {
            try
            {
                if (!TokenService.IsTokenValid(db.TokenWallets))
                {
                    return Unauthorized();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest($"{String.Join(" ", ModelState.Keys.First().Split('.')).ToLower()} est vide ou mal défini."
                            + $" {ModelState.Values.Select(x => x.Errors).First().First().ErrorMessage}"
                        );
                }

                db.Avis.Add(avis);
                db.SaveChanges();

                UpdateUserMoyenneNote(avis.IdConcerne);

                return CreatedAtRoute("DefaultApi", new { id = avis.IdAvis }, avis);
            } 
            catch
            {
                return InternalServerError();
            }
        }

        // DELETE: api/Avis/5
        [ResponseType(typeof(Avis))]
        public IHttpActionResult DeleteAvis(int id)
        {
            try
            {
                Avis avis = db.Avis.Find(id);
                if (avis == null)
                {
                    return NotFound();
                }

                // Checks token validity
                int IdUser = TokenService.GetIdUserByToken(db.TokenWallets);
                if (IdUser.Equals(-1) || IdUser != avis.IdUser)
                {
                    return Unauthorized();
                }

                db.Avis.Remove(avis);
                db.SaveChanges();

                UpdateUserMoyenneNote(avis.IdConcerne);

                return Ok(avis);
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

        private bool AvisExists(int id)
        {
            return db.Avis.Count(e => e.IdAvis == id) > 0;
        }

        private void UpdateUserMoyenneNote(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return;
            }

            double TotalNote = db.Avis.Sum(a => a.IdConcerne == id ? a.Note : 0);
            int TotalAvis = db.Avis.Count(a => a.IdConcerne == id);
            user.MoyenneNote = TotalNote / TotalAvis;

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return;
            }

        }
    }
}