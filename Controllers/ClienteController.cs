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
        [HttpGet("informe/{mes}")]
        public IActionResult get(int mes)
        {
            //return Ok(clienteService.ExtractoCliente(mes));
            var transaccion = new List<ClienteConTransacciones>();
            transaccion = clienteService.ExtractoCliente(mes);
            return Ok(new { Code = 200, Data = transaccion });
        }
    }
}
