using Bank.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        IClienteService clienteService;
        public ClienteController(IClienteService service)
        {
            clienteService = service;
        }
        [HttpGet("informe")]
        public IActionResult get()
        {
            return Ok(clienteService.ObtenerClientesConRetirosFueraCiudad());
        }
    }
}
