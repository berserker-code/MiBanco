using MiBanco.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiBanco.Application.Dtos
{
    public class TransaccionDto
    {
        public int IdTransaccion { get; set; }
        public TipoTransaccion FormaTransaccion { get; set; }

        public decimal Movimiento { get; set; }

        public decimal SaldoTotal { get; set; }

        public DateTime Fecha { get; set; }

    }
}
