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
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly string _connectionString;

        public ClienteRepositorio(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Cliente> AgregarClienteAsync(Cliente cliente, IDbConnection connection, IDbTransaction transaction)
        {
            string sql = @"INSERT INTO Clients (Nombre, Apellido, Edad, Email, Direccion)
            VALUES (@Nombre, @Apellido, @Edad, @Email, @Direccion);
            SELECT LAST_INSERT_ID();";
            int nuevoId = await connection.QuerySingleAsync<int>(sql, cliente, transaction);
            cliente.Id = nuevoId;
            return cliente;
        }

        public async Task<Cliente> ObtenerPorIdAsync(int Id)
        {
            using var connection = new MySqlConnection(_connectionString);
            string sql = "SELECT ID_Clientes AS Id, Nombre, Apellido, Edad, Email, Direccion FROM Clients  WHERE ID_Clientes = @Id";
            return await connection.QueryFirstOrDefaultAsync<Cliente>(sql, new { Id = Id });
       
        }

        public async Task<IEnumerable<Cliente>> ObtenerTodosAsync()
        {
            using var connection = new MySqlConnection(_connectionString);
            string sql = "SELECT ID_Clientes AS Id, Nombre, Apellido, Edad, Email, Direccion FROM Clients";
            return await connection.QueryAsync<Cliente>(sql);
         
        }
    }
}
