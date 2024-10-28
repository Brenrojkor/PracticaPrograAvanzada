using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace PracticaProgramada2_Grupo2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConexionController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ConexionController(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult Conectar() 
        {
            string connectionString = _configuration.GetConnectionString("Minombredeconexion");

            try 
            {
                using (var conexion = new MySqlConnection(connectionString))
                {
                    conexion.Open();
                    return Ok("Conexión Exitosa. ");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
