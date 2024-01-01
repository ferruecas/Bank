using Bank.Models;
using Microsoft.EntityFrameworkCore;

namespace Bank.Services
{
    public class CiudadService : ICiudadService
    {
        BluesoftBankDbContext context;
        public CiudadService(BluesoftBankDbContext dbContext)
        {

            context = dbContext;

        }

        public List<Ciudad> ObtenerCiudades()
        {
            return context.Ciudads.ToList();
        }

    }

    public interface ICiudadService
    {
        public List<Ciudad> ObtenerCiudades();


    }
}
