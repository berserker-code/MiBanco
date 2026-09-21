using Dapper;
using MiBanco.Domain.Entidades;
using MiBanco.Domain.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
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

        public async Task<Cuenta> AgregarCuentaAsync(Cuenta cuenta, IDbConnection connection, IDbTransaction dbtransaction)
        {

            string sql = @"INSERT INTO Accounts (Saldo,ID_de_Cliente)
            VALUES (@Saldo, @IdCliente);
            SELECT LAST_INSERT_ID();";
            int nuevoId = await connection.QuerySingleAsync<int>(sql, cuenta, dbtransaction);
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

        public async Task<bool> ActualizarSaldoAsync(int IdCuenta, decimal NuevoSaldo, IDbConnection connection, IDbTransaction dbtransaction)
        {

            string sql = "UPDATE Accounts SET Saldo = @NuevoSaldo WHERE ID_cuenta = @IdCuenta";
            var resul = await connection.ExecuteAsync(sql, new { IdCuenta = IdCuenta, NuevoSaldo= NuevoSaldo }, dbtransaction);
            return resul > 0;
        }
    }
}
