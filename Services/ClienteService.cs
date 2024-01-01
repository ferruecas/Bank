using Bank.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using Bank;

namespace Bank.Services
{
    public class ClienteService : IClienteService
    {
        BluesoftBankDbContext context;
        public ClienteService(BluesoftBankDbContext dbContext)
        {

            context = dbContext;

        }

        public List<Cliente> ObtenerClientesConRetirosFueraCiudad()
        {
            var clientesConRetiros = context.Clientes
            .Where(cliente => cliente.Cuenta
            .Any(cuenta => cuenta.Transacciones
              .Any(transaccion => transaccion.Tipo == "Retiro" && transaccion.Monto > 1000000)))
                .ToList();

            return clientesConRetiros;

        }

    }

    public interface IClienteService
    {
        public List<Cliente> ObtenerClientesConRetirosFueraCiudad();


    }
}
