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
    public class TransaccionRepositorio: ITransaccionRepositorio
    {

        private readonly string _connectionString;

        public TransaccionRepositorio(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Transaccion> AgregarTransaccionAsync(Transaccion transaccion, IDbConnection connection, IDbTransaction dbtransaction)
        {
            
            string sql = @"INSERT INTO Transactions (Tipo_transacción, Movimiento, Saldo_Actual, Fecha, ID_Cuenta)
            VALUES (@FormaTransaccion, @Movimiento, @SaldoTotal, @Fecha, @IdCuenta);
            SELECT LAST_INSERT_ID();";
            int nuevoId = await connection.QuerySingleAsync<int>(sql, transaccion, dbtransaction);
            transaccion.IdTransaccion = nuevoId;
            return transaccion;
            
        }

        public async Task<decimal> ObtenerSaldoTotalAsync(int IdCuenta)
        {
            using var connection = new MySqlConnection(_connectionString);
            string sql = "SELECT Saldo_Actual FROM Transactions WHERE ID_Cuenta = @IdCuenta ORDER BY Fecha DESC LIMIT 1";
            return await connection.QueryFirstAsync<decimal>(sql, new { IdCuenta = IdCuenta });
            
        }

        public async Task<IEnumerable<Transaccion>> VerHistorialAsync(int IdCuenta)
        {
            using var connection = new MySqlConnection(_connectionString);
            string sql = "SELECT ID_Transaction AS IdTransaccion , Tipo_transacción AS FormaTransaccion,Movimiento, Saldo_Actual AS SaldoTotal, Fecha, ID_Cuenta AS IdCuenta FROM Transactions WHERE ID_Cuenta = @IdCuenta ORDER BY Fecha DESC ";
            return await connection.QueryAsync<Transaccion>(sql);
        }
    }
}
