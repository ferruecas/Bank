using Bank.Models;
using Bank.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CiudadController : ControllerBase
    {
        ICiudadService ciudadService;

        public CiudadController(ICiudadService service)
        {
            ciudadService = service;
        }

        [HttpGet]
        public IActionResult get()
        {
         
          return Ok(ciudadService.ObtenerCiudades());
         
          
        }
    }
}
