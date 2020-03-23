using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API_Compresion.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Estaba terminandno mi lab" +
"Solo me falta subir el archivo desde postman                                       " +
"Pero me di cuenta que el VS 17 no soporta .Net Core 3.1, solo las que menores a 2.1" +
"La biblioteca IWebHostEnviorment no la puedo usar                                  " +
"Y eso es para poder hostear el archivo                                             " +
"Lo que hare será enviar la ruta desde el body, por el momento                      " +
"Mañana nada mas pueda actualizarlo                                                 " +
"Bajaré visual 19 para trabajar a partir de alli los demas labs                     " +
"Disculpe la molestia";
        }
        [HttpPost("Compresion/{tipo}")]
        public async Task<IActionResult> CompresionHuffman(IFormFile file, string tipo)
        {
            var lol = file.OpenReadStream();
            var reader = new BinaryReader(file.OpenReadStream());
            return Ok();
        }
        [HttpPost("DesCompresionHuffman")]
        public async Task<IActionResult> DesCompresionHuffman(IFormFile file)
        {
            var filePath = Path.GetTempFileName();
            if (file.Length > 0)
                using (var stream = new FileStream(filePath, FileMode.Create))
                    await file.CopyToAsync(stream);

            Data.LWZ.Instance.CompresionLZW("");
            return Ok();
        }
    }
}
