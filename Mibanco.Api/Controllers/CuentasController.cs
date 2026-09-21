using MiBanco.Application.Servicios;
using MiBanco.Domain.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MiBanco.Api.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]

    public class CuentasController : ControllerBase
    {
        private readonly CuentaServicio _cuentaServicio;

        public CuentasController(CuentaServicio cuentaServicio)
        {
            _cuentaServicio = cuentaServicio;
        }

        [HttpGet("{IdCliente}")]
        public async Task<IActionResult> ObtenerCuentaAsync(int IdCliente)
        {
            try
            {
                var cuenta = await _cuentaServicio.ObtenerCuentaPorClienteAsync(IdCliente);
                return Ok(cuenta);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }
    }
}
