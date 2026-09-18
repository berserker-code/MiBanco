using MiBanco.Application.Dtos;
using MiBanco.Domain.Entidades;
using MiBanco.Domain.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiBanco.Application.Servicios
{
    public class TransaccionServicio
    {
        private readonly ICuentaRepositorio  _cuentaRepositorio;
        private readonly ITransaccionRepositorio _transaccionRepositorio;
        private readonly string _connectionString;

        public TransaccionServicio(ICuentaRepositorio cuentaRepositorio, ITransaccionRepositorio transaccionRepositorio, string connectionString)
        {
            _cuentaRepositorio = cuentaRepositorio;
            _transaccionRepositorio = transaccionRepositorio;
            _connectionString = connectionString;
        }

        public async Task<TransaccionDto> RealizarTransaccionAsync(CrearTransaccionDto dto)
        {

            decimal validarSaldo = await _cuentaRepositorio.ObtenerSaldoAsync(dto.IdCuenta);

            if (dto.Movimiento <= 0) 
            {
                throw new InvalidOperationException("no tiene saldo");
            }
            else if (dto.FormaTransaccion == TipoTransaccion.Retiro && dto.Movimiento > validarSaldo)
            {
                throw new InvalidOperationException("el retiro supera su saldo");
            }

            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            using var transaction =  await connection.BeginTransactionAsync();

            try
            {
                decimal nuevoSaldo;
                if (dto.FormaTransaccion == TipoTransaccion.Consignacion)
                {
                    nuevoSaldo = validarSaldo + dto.Movimiento;
                }
                else if (dto.FormaTransaccion == TipoTransaccion.Retiro)
                {
                    nuevoSaldo = validarSaldo - dto.Movimiento;
                }
                else
                {
                    throw new InvalidOperationException("Tipo de transacción no válido");
                }

                var result = await _cuentaRepositorio.ActualizarSaldoAsync(dto.IdCuenta, nuevoSaldo, connection, transaction); 

            }catch (Exception ) {

    }
}
