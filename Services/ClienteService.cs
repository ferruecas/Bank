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
        public List<ClienteConTransacciones> ExtractoCliente(int mes)
        {
            var clientesConTransacciones = context.Clientes
                .Select(cliente => new ClienteConTransacciones
                {
                    ClienteId = cliente.ClienteId,
                    Nombre = cliente.Nombre,
                    NumeroTransacciones = cliente.Cuenta.SelectMany(cuenta => cuenta.Transacciones)
                        .Count(transaccion =>
                            transaccion.FechaTransaccion != null &&
                            transaccion.FechaTransaccion.Value.Month == mes)
                }).ToList();

            return clientesConTransacciones;
        }
    }
    public class ClienteConTransacciones
    {
        public int ClienteId { get; set; }
        public string Nombre { get; set; }
        public int NumeroTransacciones { get; set; }
    }

    public interface IClienteService
    {
        public List<ClienteConTransacciones> ExtractoCliente(int mes);


    }
}
