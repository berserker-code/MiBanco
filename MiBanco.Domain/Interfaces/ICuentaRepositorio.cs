using MiBanco.Domain.Entidades;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MiBanco.Domain.Interfaces
{
    public interface ICuentaRepositorio
    {
        Task<Cuenta> AgregarCuentaAsync(Cuenta cuenta, IDbConnection connection, IDbTransaction dbtransaction);
        Task<Cuenta> ObtenerCuentaAsync(int IdCliente);
        Task<decimal> ObtenerSaldoAsync(int IdCliente);

        Task<bool> ActualizarSaldoAsync(int IdCuenta, decimal NuevoSaldo, IDbConnection connection, IDbTransaction dbtransaction);
    }
}
