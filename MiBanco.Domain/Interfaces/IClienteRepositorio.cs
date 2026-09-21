

using MiBanco.Domain.Entidades;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data;

namespace MiBanco.Domain.Interfaces
{
    public interface IClienteRepositorio
    {

        Task<Cliente> AgregarClienteAsync(Cliente cliente, IDbConnection connection, IDbTransaction dbtransaction);
        Task<Cliente> ObtenerPorIdAsync(int Id);
        Task<IEnumerable<Cliente>> ObtenerTodosAsync();




    }
}
