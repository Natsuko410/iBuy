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
            try
            {
                Enchere enchere = db.Encheres.Find(id);
                if (enchere == null)
                {
                    return NotFound();
                }

                return Ok(enchere);
            }
            catch
            {
                return InternalServerError();
            }
        }

        // PUT: api/Encheres/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEnchere(int id, [FromBody] Enchere enchere)
        {
            try
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

                Annonce Annonce = db.Annonces.Find(enchere.IdAnno);
                if (IdUser != Annonce.IdUser)
                {
                    return BadRequest("L'identifiant ne correspond pas avec l'id du propriétaire de cette annonce.");
                }

                if (id != enchere.IdEnch)
                {
                    return BadRequest("L'identifiant ne correspond pas avec l'enchère spécifiée.");
                }

                if (DateTime.Now > enchere.DateDebut)
                {
                    return BadRequest("Impossible de modifier une enchère qui a déjà commencé.");
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

                Enchere EnchereDb = db.Encheres.AsNoTracking().Where(e => e.IdEnch == id).FirstOrDefault();
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
                        return InternalServerError();
                    }
                }

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch
            {
                return InternalServerError();
            }
        }

        // POST: api/Encheres
        [ResponseType(typeof(Enchere))]
        public IHttpActionResult PostEnchere(Enchere enchere)
        {
            try
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

                Annonce Annonce = db.Annonces.Find(enchere.IdAnno);
                if (IdUser != Annonce.IdUser)
                {
                    return Unauthorized();
                }

                if (db.Encheres.Count(e => e.IdAnno == enchere.IdAnno) > 0)
                {
                    return BadRequest("Impossible d'ajouter plusieurs enchères sur la même annonce.");
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
            catch
            {
                return InternalServerError();
            }
        }

        // DELETE: api/Encheres/5
        [ResponseType(typeof(Enchere))]
        public IHttpActionResult DeleteEnchere(int id)
        {
            try
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

                if (DateTime.Now > enchere.DateDebut)
                {
                    return BadRequest("Impossible de supprimer une enchère qui a déjà commencé.");
                }

                db.Encheres.Remove(enchere);
                db.SaveChanges();

                return Ok(enchere);
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

        private bool EnchereExists(int id)
        {
            return db.Encheres.Count(e => e.IdEnch == id) > 0;
        }
    }
}