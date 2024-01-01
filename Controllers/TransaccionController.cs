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
            var transaccion = new List<Transaccione>();
            transaccion = transaccionService.ObtenerMovimientosRecientes(cuentaId, cantidad);
            return Ok(new { Code = 200, Data = transaccion });

            //return Ok();
        }

        [HttpGet("{cuentaId}/{mes}/{año}")]
        public IActionResult Get( int cuentaId, int mes, int año)
        {
            List<Transaccione> transaccion = new List<Transaccione>();
            transaccion=transaccionService.GenerarExtractoMensual(cuentaId,mes,año);
            return Ok(new { Code = 200, Data = transaccion });
        }

        [HttpPost("consignacion")]
        public async Task<IActionResult> PostConsignacion([FromBody] Transaccione transaccione)
        {
            try
            {
                await transaccionService.RealizarConsignacion(transaccione);
                return Ok(new { Message = "Consignación realizada con éxito" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = "Argumento inválido", Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Error = "Operación no válida", Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Error interno del servidor", Message = ex.Message });
            }
        }
        [HttpPost("retiro")]
        public async Task<IActionResult> PostRetiro([FromBody] Transaccione transaccione)
        {
            try
            {
                await transaccionService.RealizarRetiro(transaccione);
                return Ok(new { Message = "Retiro realizado con éxito" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = "Argumento inválido", Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Error = "Operación no válida", Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Error interno del servidor", Message = ex.Message });
            }
        }

    }
}
