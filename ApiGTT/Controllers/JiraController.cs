using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGTT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGTT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JiraController : ControllerBase
    {
        private readonly AppDBContext _context; //guion bajo delante si es de solo lectura
        public JiraController(AppDBContext context)
        {
            this._context = context;
            if (this._context.Jira.Count() == 0)
            {
                Console.WriteLine("No existen usuarios");
                Jira jira = new Jira();
                //jira.id = 2;
                jira.username = "nombrejira";
                jira.password = "1234";
                jira.component = "componente";
                jira.project = "proyecto";
                jira.url = "holi soy una url";
                jira.user_id = 1;
                jira.issue = "explotacion";

                this._context.Jira.Add(jira);

                this._context.SaveChanges(); //guardamos el usuario 
            }
        }


        // GET: api/Jira
        [HttpGet]
        public List<Jira> GetAll()
        {
            return this._context.Jira.ToList();
        }

        // GET: api/Jira/5
        [HttpGet("{id}", Name = "GetJira")]
        public ActionResult<Jira> Get(long id)
        {
            try
            {
                Jira userDelete = this._context.Jira.Where(
                    jira => jira.user_id == id).First();
                return userDelete;
            }
            catch(Exception)
            {
                return NotFound();
            }
        }

        // POST: api/Jira
        [HttpPost]
        public ActionResult<Control> Post(Jira userJira)
        {
            Control control;
            Jira userAdd;
            try {
                userAdd = this._context.Jira.Where(queryUser => queryUser.user_id == userJira.user_id).First();
            }
            catch (Exception)
            {
                userAdd = null;
            }
            if (userAdd == null)
            {
                userJira.id = userJira.user_id;
               

                this._context.Jira.Add(userJira);
                this._context.SaveChanges();
                control = new Control(200, "user de jira guardado!", "", -1,Role.user);
                return control;
            }
            else {
                control = new Control(409, "El usuario ya existe", "",userAdd.id,Role.user);
                return control;
            }
            
        }

        // PUT: api/Jira/5
        [HttpPut("{id}")]
        public ActionResult<Jira> Put(Jira userJira)
        {
            try {

                Jira user = this._context.Jira.Where(
                    jira => jira.user_id.Equals(userJira.id)).First();

                user.user_id = userJira.user_id;
                user.username = userJira.username;
                user.password = userJira.password;
                user.component = userJira.component;
                user.url = userJira.url;
                user.project = userJira.project;
                user.issue = userJira.issue;

                this._context.SaveChanges();

                return user;
            }
            catch (Exception)
            {
                return NotFound();
            }

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
