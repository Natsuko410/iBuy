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
{
    public class CategoriesController : ApiController
    {
        private IBuyContext db = new IBuyContext();

        // GET: api/Categories
        public IQueryable<Categorie> GetCategories()
        {
            return db.Categories;
        }

        // GET: api/Categories/5
        [ResponseType(typeof(Categorie))]
        public IHttpActionResult GetCategorie(int id)
        {
            Categorie categorie = db.Categories.Find(id);
            if (categorie == null)
            {
                return NotFound();
            }

            return Ok(categorie);
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategorie(int id, Categorie categorie)
        {
            int IdUser = TokenService.GetIdUserByToken(db.TokenWallets);
            User user = db.Users.Find(IdUser);

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

            if (user.IsAdmin == false)
            {
                return Unauthorized();
            }

            if (id != categorie.IdCat)
            {
                return BadRequest("L'identifiant ne correspond pas avec la catégorie spécifiée.");
            }

            db.Entry(categorie).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategorieExists(id))
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

        // POST: api/Categories
        [ResponseType(typeof(Categorie))]
        public IHttpActionResult PostCategorie(Categorie categorie)
        {
            int IdUser = TokenService.GetIdUserByToken(db.TokenWallets);
            User user = db.Users.Find(IdUser);

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

            if (user.IsAdmin == false)
            {
                return Unauthorized();
            }

            db.Categories.Add(categorie);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = categorie.IdCat }, categorie);
        }

        // DELETE: api/Categories/5
        [ResponseType(typeof(Categorie))]
        public IHttpActionResult DeleteCategorie(int id)
        {
            int IdUser = TokenService.GetIdUserByToken(db.TokenWallets);
            User user = db.Users.Find(IdUser);

            if (IdUser.Equals(-1))
            {
                return Unauthorized();
            }

            Categorie categorie = db.Categories.Find(id);
            if (categorie == null)
            {
                return NotFound();
            }

            if (user.IsAdmin == false)
            {
                return Unauthorized();
            }

            db.Categories.Remove(categorie);
            db.SaveChanges();

            return Ok(categorie);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategorieExists(int id)
        {
            return db.Categories.Count(e => e.IdCat == id) > 0;
        }
    }
}