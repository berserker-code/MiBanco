using MiBanco.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiBanco.Application.Dtos
{
    public class CrearTransaccionDto
    {
        public int IdCuenta { get; set; }
        public TipoTransaccion FormaTransaccion { get; set; }

        public decimal Movimiento { get; set; }

        
    }
}
