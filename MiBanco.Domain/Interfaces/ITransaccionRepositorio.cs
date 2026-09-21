using MiBanco.Domain.Entidades;

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MiBanco.Domain.Interfaces
{
    public interface ITransaccionRepositorio
    {

        Task<Transaccion> AgregarTransaccionAsync(Transaccion transaccion, IDbConnection connection, IDbTransaction dbtransaction);
        Task<decimal> ObtenerSaldoTotalAsync(int IdCuenta);

        Task<IEnumerable<Transaccion>> VerHistorialAsync(int IdCuenta);
    }
}
