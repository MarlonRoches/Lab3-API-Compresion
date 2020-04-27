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
            return "";
        }
        [HttpPost("Compresion/{tipo}")]
        public async Task<IActionResult> Compresiones(IFormFile file, string tipo)
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
            if (tipo =="" || tipo == null)
            {
                return BadRequest();
            }
            else
            {

                if (tipo.ToLower() == "lzw")
                {
                return Ok();

                Data.LWZ_API.Instance.CompresionLZW(listabytes, file.FileName);
                }
                else if (tipo.ToLower() == "huff")
                {
                    //huffman
                return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
        }
        [HttpPost("Desompresion/{tipo}")]
        public async Task<IActionResult> Descompresiones(IFormFile file, string tipo)
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
            if (tipo == "" || tipo == null)
            {
                return BadRequest();
            }
            else
            {
                if (tipo.ToLower() == "lzw")
                {
                    Data.LWZ_API.Instance.DescompresionLZW(listabytes, file.FileName);
                    return Ok();
                }
                else if (tipo.ToLower() == "huff")
                {
                    //huffman
                    return Ok();

                }
                else
                {
                    return BadRequest();

                }
            }
        }
    }
}
