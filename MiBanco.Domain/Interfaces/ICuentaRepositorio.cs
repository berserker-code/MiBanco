using MiBanco.Domain.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiBanco.Domain.Interfaces
{
    public interface ICuentaRepositorio
    {
        Task<Cuenta> AgregarCuentaAsync(Cuenta cuenta);
        Task<Cuenta> ObtenerCuentaAsync(int IdCliente);
        Task<decimal> ObtenerSaldoAsync(int IdCliente);
    }
}
