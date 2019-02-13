using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGTT.Helpers;
using ApiGTT.Models;
using Jose;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGTT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDBContext _context;

        public AuthController(AppDBContext context)
        {
            this._context = context;
        }
        // GET: api/Auth
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

   

        // POST: api/Auth
        [HttpPost]
        public ActionResult<Control> Post([FromBody] Users value)
        {

            try

            {

                Users UserResult = this._context.Users.Where(
                user => user.username == value.username).FirstOrDefault();
                Control control;
                if (UserResult.password == value.password)
                {
                    string token = JWT.Encode(value.role, "top secret", JweAlgorithm.PBES2_HS256_A128KW, JweEncryption.A256CBC_HS512);
                    control = new Control(200, "todo bien", token, UserResult.id, UserResult.role);
                    return control;

                     //return new JsonResult(“Estado: true”);
                }

                else
                {
                    control = new Control(400, "estoy en else", "",value.id, value.role);
                    return control;
                }

            }

            catch (Exception e)
            {

                Control control = new Control(400, "estoy en catch", "",value.id, value.role);
                return control;
            }
        }

        // PUT: api/Auth/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
