

namespace MiBanco.Domain.Entidades
{
    public class Cuenta
    {
        public int IdCuenta { get; set; }
        public decimal Saldo { get; set; }

        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }
    }
}
