using Bank.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bank.Services
{
    public class CuentaService: ICuentaService
    {
        BluesoftBankDbContext context;
        public CuentaService(BluesoftBankDbContext dbContext)
        {

            context = dbContext;

        }
        public ActionResult<decimal> ObtenerSaldo(int id)
        {
            var cuenta = context.Cuentas.FirstOrDefault(c => c.CuentaId == id);

            if (cuenta == null)
            {
                // La cuenta no existe, puedes personalizar el mensaje según tus necesidades.
                return new NotFoundObjectResult("No se encontró la cuenta.");
            }

            return new OkObjectResult(cuenta.Saldo);
        }
    }

    public interface ICuentaService
    {
        public ActionResult<decimal> ObtenerSaldo(int id);

    }
}
