using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ApiGTT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGTT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificatesController : ControllerBase

    {
        private readonly AppDBContext _context;

        public CertificatesController(AppDBContext context)
        {
            this._context = context;
          
        }
        // GET: api/Certificates
        [HttpGet]
        public List<Certificates> Get()
        {
            return this._context.Certificates.ToList();
        }

        // GET: api/Certificates/5
        [HttpGet("{id}", Name = "GetDetails")]
        public ActionResult<Certificates> GetDetails(long id)
        {
            try
            {
                Certificates certResult = this._context.Certificates.Where(
                    certificado => certificado.id == id).First();
                return certResult;
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]

        public ActionResult<Control> Post([FromBody] Certificates value)
        {
            try
            {
                // Obtenemos el string en base64 y se convierte a byte []

                byte[] arrayBytes = System.Convert.FromBase64String(value.ficheroBase64);
                //byte[] arrayBytes = System.Convert.FromBase64String("asfssdafsdafsdfds");

                // Lo cargamos en certificate

                X509Certificate2 certificate = new X509Certificate2(arrayBytes, "111111");

                string token = certificate.ToString(true);
                //elementos del certificado para el front

                value.numero_de_serie = certificate.SerialNumber.ToString();
                value.subject = certificate.Subject.ToString();
                value.entidad_emisora = certificate.Issuer.ToString();
                value.caducidad = certificate.NotAfter;


                // Por ahora solo devuelve todos los datos
                this._context.Certificates.Add(value);
                this._context.SaveChanges();
                Control control = new Control(200, "certificado añadido a bbdd", "", -1,Role.user);
                return control;
            }
            catch (Exception ex){
                return Unauthorized();
            }

        }

        // PUT: api/Certificates/5
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
