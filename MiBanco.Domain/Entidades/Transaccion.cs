using System;

namespace MiBanco.Domain.Entidades
{
    public class Transaccion
    {
        public int IdTransaccion { get; set; }
        public TipoTransaccion FormaTransaccion { get; set; }

        public decimal Movimiento { get; set; }

        public decimal SaldoTotal { get; set; }

        public DateTime Fecha { get; set; }

        public int IdCuenta { get; set; }

        public Cuenta Cuenta { get; set; }
    }

    public enum TipoTransaccion
    {
        Consignacion,
        Retiro
    }
}

