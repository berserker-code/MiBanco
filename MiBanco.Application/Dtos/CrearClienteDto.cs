using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiBanco.Application.Dtos
{
    public class CrearClienteDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }

        public string Email { get; set; }

        public string Direccion { get; set; }


    }
}
