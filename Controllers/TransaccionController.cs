using Microsoft.AspNetCore.Mvc;
using Bank.Models;
using Bank.Services;

namespace Bank.Controllers
{
    [Route("api/[controller]")]
    public class TransaccionController:ControllerBase
    {
        ITransaccionService transaccionService;
        public TransaccionController(ITransaccionService service)
        {
           transaccionService = service;
        }
        [HttpGet("{cuentaId}/{cantidad}")]
        public IActionResult Get(int cuentaId,int cantidad)
        {
            return Ok(transaccionService.ObtenerMovimientosRecientes(cuentaId,cantidad));
        }

        [HttpGet("{cuentaId}/{mes}/{año}")]
        public IActionResult Get( int cuentaId, int mes, int año)
        {
            transaccionService.GenerarExtractoMensual(cuentaId,mes,año);
            return Ok();
        }

        [HttpPost("consignacion")]
        public IActionResult PostConsignacion([FromBody] Transaccione transaccione)
        {
            transaccionService.RealizarConsignacion(transaccione);
            return Ok();
        }
        [HttpPost("retiro")]
        public IActionResult PostRetiro([FromBody] Transaccione transaccione)
        {
            var message=transaccionService.RealizarRetiro(transaccione);
            return Ok();
        }

    }
}
