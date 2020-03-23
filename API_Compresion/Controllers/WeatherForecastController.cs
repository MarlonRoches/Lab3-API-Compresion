using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using API_Compresion.Data;
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
            var reader = new StreamReader(file.OpenReadStream());
            var longitud = Convert.ToInt32(reader.BaseStream.Length);
            var buffer = reader.ReadToEnd();
            var listabytes = new List<string>();
            foreach (var item in buffer)
            {
                listabytes.Add(item.ToString());
            }
            
            Data.LWZ_API.Instance.CompresionLZW(listabytes, file.FileName);
            return Ok();
        }
        [HttpPost("Desompresion/{tipo}")]
        public async Task<IActionResult> DesCompresionHuffman(IFormFile file, string tipo)
        {
            var lol = file.OpenReadStream();
            var reader = new StreamReader(file.OpenReadStream());
            var longitud = Convert.ToInt32(reader.BaseStream.Length);
            var buffer = reader.ReadToEnd();
            var listabytes = new List<string>();
            foreach (var item in buffer)
            {
                listabytes.Add(item.ToString());
            }
            Data.LWZ_API.Instance.DescompresionLZW(listabytes, file.FileName);
            return Ok();
        }
    }
}
