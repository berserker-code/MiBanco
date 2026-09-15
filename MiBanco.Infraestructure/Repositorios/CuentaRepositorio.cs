using Dapper;
using MiBanco.Domain.Entidades;
using MiBanco.Domain.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiBanco.Infraestructure.Repositorios
{
    public class CuentaRepositorio : ICuentaRepositorio
    {

        private readonly string _connectionString;

        public CuentaRepositorio(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Cuenta> AgregarCuentaAsync(Cuenta cuenta)
        {

            using var connection = new MySqlConnection(_connectionString);
            string sql = @"INSERT INTO Accounts (Saldo,ID_de_Cliente)
            VALUES (@Saldo, @IdCliente);
            SELECT LAST_INSERT_ID();";
            int nuevoId = await connection.QuerySingleAsync<int>(sql, cuenta);
            cuenta.IdCuenta = nuevoId;
            return cuenta;
            
        }

        public async Task<Cuenta> ObtenerCuentaAsync(int IdCliente)
        {
            using var connection = new MySqlConnection(_connectionString);
            string sql = "SELECT ID_de_Cliente AS IdCliente, Saldo, ID_cuenta AS IdCuenta FROM Accounts WHERE ID_de_Cliente = @IdCliente";
            return await connection.QueryFirstOrDefaultAsync<Cuenta>(sql, new { IdCliente = IdCliente });
            
        }

        public async Task<decimal> ObtenerSaldoAsync(int IdCliente)
        {
            using var connection = new MySqlConnection(_connectionString);
            string sql = "SELECT Saldo FROM Accounts WHERE ID_de_Cliente = @IdCliente";
            return await connection.QueryFirstAsync<decimal>(sql, new { IdCliente = IdCliente });
            
        }
    }
}
