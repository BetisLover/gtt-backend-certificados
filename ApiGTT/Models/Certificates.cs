using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGTT.Models
{
    public class Certificates
    {
        public long id { get; set; }

        public string alias { get; set; }

        public string entidad_emisora { get; set; }

        public string numero_de_serie { get; set; }

        public string subject { get; set; }

        public DateTime caducidad { get; set; }

        public string password { get; set; }

        public string id_orga { get; set; }

        public string nombre_cliente { get; set; }

        public string contacto_renovacion { get; set; }

        public string repositorio { get; set; }

        public string observaciones { get; set; }

        public string integration_list { get; set; }

        public string ficheroBase64 { get; set; }

        public string nombre_archivo { get; set; } 

        public bool marcar_borrado { get; set; } 
    }
}
