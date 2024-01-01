using Bank.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{
    [Route("api/[controller]")]
    public class CuentaController:ControllerBase
    {
        ICuentaService cuentaService;
        public CuentaController(ICuentaService service)
        {
            cuentaService = service;
        }
        [HttpGet("saldo/{id}")]
        public ActionResult ConsultarSaldoCuenta(int id)
        {
            ActionResult<decimal> saldo = cuentaService.ObtenerSaldo(id);

            return Ok(new {Code = ((ObjectResult)saldo.Result).StatusCode, Data= ((ObjectResult)saldo.Result).Value });


        }
    }
}
