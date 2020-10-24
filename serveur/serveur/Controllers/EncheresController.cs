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
using serveur.Services;

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

            if (IdUser != enchere.Annonce.IdUser)
            {
                return BadRequest("L'identifiant ne correspond pas avec l'id de propriétaire de cette annonce.");
            }

            if (id != enchere.IdEnch)
            {
                return BadRequest("L'identifiant ne correspond pas avec l'enchère spécifiée.");
            }

            if (enchere.DateDebut < DateTime.Now)
            {
                return BadRequest("La date de début ne peut pas être inférieure à la date actuelle.");
            }

            if (enchere.DateFin < DateTime.Now)
            {
                return BadRequest("La date de fin ne peut pas être inférieure à la date actuelle.");
            }

            if (enchere.DateFin <= enchere.DateDebut)
            {
                return BadRequest("La date de début ne peut pas être inférieure ou égale à la date de fin.");
            }

            Enchere EnchereDb = db.Encheres.Find(id);
            if (EnchereDb == null)
            {
                return BadRequest("Cette enchère n'existe pas.");
            }

            if (DateTime.Now > EnchereDb.DateDebut || DateTime.Now > EnchereDb.DateFin)
            {
                return BadRequest("Cette enchère a déjà débuté ou est déjà terminée.");
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

            if (IdUser != enchere.Annonce.IdUser)
            {
                return Unauthorized();
            }

            if (enchere.DateDebut < DateTime.Now)
            {
                return BadRequest("La date de début ne peut pas être inférieure à la date actuelle.");
            }

            if (enchere.DateFin < DateTime.Now)
            {
                return BadRequest("La date de fin ne peut pas être inférieure à la date actuelle.");
            }

            if (enchere.DateFin < enchere.DateDebut)
            {
                return BadRequest("La date de début ne peut pas être inférieure à la date de fin.");
            }

            db.Encheres.Add(enchere);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = enchere.IdEnch }, enchere);
        }

        // DELETE: api/Encheres/5
        [ResponseType(typeof(Enchere))]
        public IHttpActionResult DeleteEnchere(int id)
        {

            int IdUser = TokenService.GetIdUserByToken(db.TokenWallets);
            if (IdUser.Equals(-1))
            {
                return Unauthorized();
            }

            Enchere enchere = db.Encheres.Find(id);
            if (enchere == null)
            {
                return NotFound();
            }

            if (IdUser != enchere.Annonce.IdUser)
            {
                return Unauthorized();
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