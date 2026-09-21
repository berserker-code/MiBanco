using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiBanco.Application.Dtos
{
    public class ClienteConCuentaDto
    {
        public ClienteDto Cliente { get; set; }
        public CuentaDto Cuenta { get; set; }

    }
}
