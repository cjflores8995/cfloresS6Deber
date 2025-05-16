using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cfloresS6Jueves.Models
{
    public class Metrica
    {
        public Guid Id { get; set; }
        public string Especie { get; set; }
        public string NombreRaza { get; set; }
        public string Origen { get; set; }
        public Guid Usuario { get; set; } = Guid.Empty;
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool Estado { get; set; } = true;
    }
}
