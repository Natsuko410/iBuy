﻿using System;
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


    // GET: api/Annonces
    // ?montant=num                             montant de l'offre
    // ?idUser=num                              id du user correspondant à l'offre 
    // ?idEnchere=num                           id de l'enchère correspodant à l'offre
 
{
    public class OffresController : ApiController
    {
        private IBuyContext db = new IBuyContext();

        // GET: api/Offres
        public IQueryable<Offre> GetOffres([FromUri] int idEnch)
        {
            return db.Offres.Where(offre => offre.IdEnch == idEnch);
        }

        // GET: api/Offres/5
        [ResponseType(typeof(Offre))]
        public IHttpActionResult GetOffre(int id)
        {
            Offre offre = db.Offres.Find(id);
            if (offre == null)
            {
                return NotFound();
            }

            return Ok(offre);
        }

        // PUT: api/Offres/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOffre(int id, Offre offre)
        {
            return Unauthorized();
        }

        // POST: api/Offres
        [ResponseType(typeof(Offre))]
        public IHttpActionResult PostOffre([FromBody] Offre offre)
        {
            // Checks token validity
            int IdUser = TokenService.GetIdUserByToken(db.TokenWallets);
            if (IdUser.Equals(-1) || IdUser != offre.IdUser)
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if ((DateTime.Compare(DateTime.Now, offre.Enchere.DateDebut) < 0) || (DateTime.Compare(DateTime.Now, offre.Enchere.DateFin) > 0))
            {
                return BadRequest();
            }

            db.Offres.Add(offre);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = offre.IdOffr }, offre);
        }

        // DELETE: api/Offres/5
        [ResponseType(typeof(Offre))]
        public IHttpActionResult DeleteOffre(int id)
        {
            Offre offre = db.Offres.Find(id);
            if (offre == null)
            {
                return NotFound();
            }
            
            // Checks token validity
            int IdUser = TokenService.GetIdUserByToken(db.TokenWallets);
            if (IdUser.Equals(-1) || IdUser != offre.IdUser)
            {
                return Unauthorized();
            }

            db.Offres.Remove(offre);
            db.SaveChanges();

            return Ok(offre);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OffreExists(int id)
        {
            return db.Offres.Count(e => e.IdOffr == id) > 0;
        }
    }
}