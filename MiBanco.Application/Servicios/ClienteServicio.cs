using MiBanco.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiBanco.Application.Servicios
{
    public class ClienteServicio
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly ICuentaRepositorio _cuentaRepositorio;
        private readonly string _connectionString;



        public ClienteServicio(IClienteRepositorio clienteRepositorio, ICuentaRepositorio cuentaRepositorio, string connectionString)
        {
            _clienteRepositorio = clienteRepositorio;
            _cuentaRepositorio = cuentaRepositorio;
            _connectionString = connectionString;
        }

    }
}
