using MiBanco.Domain.Entidades;

using System.Collections.Generic;

using System.Threading.Tasks;

namespace MiBanco.Domain.Interfaces
{
    public interface ITransaccionRepositorio
    {

        Task<Transaccion> AgregarTransaccionAsync(Transaccion transaccion);
        Task<decimal> ObtenerSaldoTotalAsync(int IdCuenta);

        Task<IEnumerable<Transaccion>> VerHistorialAsync(int IdCuenta);
    }
}
