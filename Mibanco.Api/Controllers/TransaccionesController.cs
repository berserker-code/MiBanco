using MiBanco.Application.Dtos;
using MiBanco.Application.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace MiBanco.Api.Controllers
{

    [ApiController]
    [Route("Api/[Controller]")]
    public class TransaccionesController : ControllerBase
    {
        private readonly TransaccionServicio _transaccionServicio;

        public TransaccionesController (TransaccionServicio transaccionServicio)
        {
            _transaccionServicio = transaccionServicio;
        }


        [HttpPost]
        public async Task<IActionResult> RealizarTransaccionAsync([FromBody] CrearTransaccionDto dto)
        {
            try
            {
                var transaccion = await _transaccionServicio.RealizarTransaccionAsync(dto);
                return Ok(transaccion);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{IdCuenta}")]
        public async Task<IActionResult> ObtenerHistorialAsync(int IdCuenta)
        {
            var historial = await _transaccionServicio.ObtenerHistorialAsync(IdCuenta);
            return Ok(historial);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerSaldoAsync(int IdCuenta)
        {
            var saldo = await _transaccionServicio.ObtenerSaldoTotalAsync(IdCuenta);
            return Ok(saldo);
        }
    }
}
