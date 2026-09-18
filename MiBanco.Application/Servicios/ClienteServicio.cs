using MiBanco.Application.Dtos;
using MiBanco.Domain.Entidades;
using MiBanco.Domain.Interfaces;
using MySqlConnector;
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


        public async Task<ClienteConCuentaDto> RegistrarClienteAsync(CrearClienteDto dto)
        {
            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            using var transaction = await connection.BeginTransactionAsync();

            try
            {
                var cliente = new Cliente
                {
                    Nombre = dto.Nombre,
                    Apellido = dto.Apellido,
                    Edad = dto.Edad,
                    Email = dto.Email,
                    Direccion = dto.Direccion,
                };

                cliente = await _clienteRepositorio.AgregarClienteAsync(cliente, connection, transaction);

                var cuenta = new Cuenta
                {
                    Saldo = 0,
                    IdCliente = cliente.Id

                };

                cuenta = await _cuentaRepositorio.AgregarCuentaAsync(cuenta, connection, transaction);

                await transaction.CommitAsync();

                return new ClienteConCuentaDto
                {
                    Cliente = new ClienteDto
                    {
                        Id = cliente.Id,
                        Nombre = cliente.Nombre,
                        Apellido = cliente.Apellido,
                        Edad = cliente.Edad,
                        Email = cliente.Email,
                        Direccion = cliente.Direccion
                    },
                    Cuenta = new CuentaDto
                    {
                        IdCuenta = cuenta.IdCuenta,
                        Saldo = cuenta.Saldo
                    }
                };
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }


        }

    }
}
