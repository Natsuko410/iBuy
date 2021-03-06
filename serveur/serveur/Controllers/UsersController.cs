﻿using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using serveur.Data;
using serveur.Models;
using serveur.Services;

namespace serveur.Controllers
{
    public class UsersController : ApiController
    {
        private IBuyContext db = new IBuyContext();

        //OPTIONS
        public IHttpActionResult OptionsUsers()
        {
            return Ok();
        }

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            try
            {
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch
            {
                return InternalServerError();
            }
        }

        // PUT: api/Users
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser([FromBody] User user)
        {
            try
            {
                // Checks token validity
                int IdUser = TokenService.GetIdUserByToken(db.TokenWallets);
                if (IdUser.Equals(-1))
                {
                    return BadRequest("L'identifiant fourni n'est pas correct.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest($"{String.Join(" ", ModelState.Keys.First().Split('.')).ToLower()} est vide ou mal défini."
                            + $" {ModelState.Values.Select(x => x.Errors).First().First().ErrorMessage}"
                        );
                }
                user.IsAdmin = false;

                if (IdUser != user.IdUser)
                {
                    return Unauthorized();
                }

                // Finds User in Database + Checks password Hash
                User UserDB = db.Users.AsNoTracking().Where(u => u.IdUser == IdUser).FirstOrDefault();
                if (UserDB != null && !UserDB.Mdp.Equals(user.Mdp))
                {
                    // Hash password before saving
                    user.Mdp = BCrypt.HashPassword(user.Mdp, BCrypt.GenerateSalt());
                }

                db.Entry(user).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(IdUser))
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
            catch
            {
                return InternalServerError();
            }
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest($"{String.Join(" ", ModelState.Keys.First().Split('.')).ToLower()} est vide ou mal défini."
                        + $" {ModelState.Values.Select(x => x.Errors).First().First().ErrorMessage}"
                    );
                }

                // Checks if this user pseudo isn't already taken
                if (db.Users.Count(u => u.Pseudo.Equals(user.Pseudo)) > 0)
                {
                    return BadRequest("Ce pseudo est déjà utilisé.");
                }
                user.IsAdmin = false;

                // Hash password before saving
                user.Mdp = BCrypt.HashPassword(user.Mdp, BCrypt.GenerateSalt());

                db.Users.Add(user);
                db.SaveChanges();

            } catch
            {
                return InternalServerError();
            }

            return CreatedAtRoute("DefaultApi", new { id = user.IdUser }, user);
        }

        // DELETE: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser()
        {
            try
            {
                // Checks token validity
                int IdUser = TokenService.GetIdUserByToken(db.TokenWallets);
                if (IdUser.Equals(-1))
                {
                    return BadRequest("L'identifiant fourni n'est pas correct.");
                }

                // Find user
                User user = db.Users.Find(IdUser);
                if (user == null)
                {
                    return NotFound();
                }

                if (IdUser != user.IdUser)
                {
                    return Unauthorized();
                }

                db.Users.Remove(user);
                db.SaveChanges();

                return Ok(user);
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

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.IdUser == id) > 0;
        }
    }
}