using MiBanco.Application.Dtos;
using MiBanco.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiBanco.Application.Servicios
{
    public class CuentaServicio
    {
        private readonly ICuentaRepositorio _cuentaRepositorio;
        
        

        public CuentaServicio(ICuentaRepositorio cuentaRepositorio)
        {
            _cuentaRepositorio = cuentaRepositorio;
            
         
        }


        public async Task<CuentaDto> ObtenerCuentaPorClienteAsync(int IdCliente)
        {
            var cuenta = await _cuentaRepositorio.ObtenerCuentaAsync(IdCliente);

            if (cuenta == null)
            {
                throw new KeyNotFoundException($"No se encontró la cuenta con Id {IdCliente}");
            }

            return new CuentaDto
            {
                IdCuenta = cuenta.IdCuenta,
                Saldo = cuenta.Saldo
            };
        }

    }
}
