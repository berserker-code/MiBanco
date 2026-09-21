using MiBanco.Application.Dtos;
using MiBanco.Application.Servicios;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace MiBanco.Api.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteServicio _clienteServicio;


        public ClientesController (ClienteServicio clienteServicio)
        {
            _clienteServicio = clienteServicio;
        }


        [HttpPost]
        public async Task<IActionResult> RegistrarClienteAsync([FromBody ] CrearClienteDto dto)
        {
       
                var result = await _clienteServicio.RegistrarClienteAsync(dto);
                return Ok(result);
           
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> ObtenerPorIdAsync(int Id)
        {
            try
            {
                var cliente = await _clienteServicio.ObtenerPorIdAsync(Id);
                return Ok(cliente);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodosAsync()
        {
            var clientes = await _clienteServicio.ObtenerTodosAsync();
            return Ok(clientes);
        }

    }
}
