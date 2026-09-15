

using MiBanco.Domain.Entidades;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MiBanco.Domain.Interfaces
{
    public interface IClienteRepositorio
    {

        Task<Cliente> AgregarClienteAsync(Cliente cliente);
        Task<Cliente> ObtenerPorIdAsync(int Id);
        Task<IEnumerable<Cliente>> ObtenerTodosAsync();




    }
}
