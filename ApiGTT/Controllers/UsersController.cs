using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiGTT.Models;
using ApiGTT.Helpers;
namespace ApiGTT.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDBContext _context; //guion bajo delante si es de solo lectura
        public UsersController(AppDBContext context) {
            this._context = context;
            if (this._context.Users.Count() == 0) {
                Console.WriteLine("No existen usuarios");

                Users usuario = new Users();
                usuario.id = 1;
                usuario.username = "nombreprueba";
                //usuario.password = Encrypt.Hash("1234");
                usuario.password = "1234";
                usuario.email = "nombreprueba@gmail.com";
                usuario.role = 0;
           
                this._context.Users.Add(usuario);
                this._context.SaveChanges(); //guardamos el usuario 
            }
        }
        // GET api/users
        [HttpGet]
        public ActionResult<List<Users>> GetAll()
        {
            return this._context.Users.ToList();
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public ActionResult<Users> Get(long id)
        {
            Users user = this._context.Users.Find(id);
            if (user == null) {
                return NotFound("El user no se ha encontrado o no existe.");
            }
            return user;
        }

        // POST api/users
        [HttpPost]
        public ActionResult<Users> Post([FromBody] Users value)
        {
           value.password = Encrypt.Hash(value.password);
           this._context.Users.Add(value);
            
            this._context.SaveChanges();
            return value;
        }

        // PUT api/users/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Users value)
        {
            Users user = this._context.Users.Find(id);
            user.username = value.username;
            user.password = value.password;
            user.email = value.email;
            user.role = value.role;
            this._context.SaveChanges();
        }

        // DELETE api/users
        [HttpDelete("{id}")]
        public string Delete(long id)
        {
            // Users user = this._context.Users.Find(id); tambien se puede asi
            Users userDelete = this._context.Users.Where(
                user => user.username == "nombreprueba" && user.password=="1234").First();
            if (userDelete == null) {
                return "no existe usuario";
            }
            this._context.Remove(userDelete);
            this._context.SaveChanges();
            return "se ha eliminado "+userDelete.id;
        }
    }
}
