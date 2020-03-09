using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Estaba terminandno mi lab" +
" Solo me falta subir el archivo desde postman\n" +
" Pero me di cuenta que el VS 17 no soporta .Net Core 3.1, solo las que menores a 2.1\n" +
" La biblioteca IWebHostEnviorment no la puedo usar\n" +
" Y eso es para poder hostear el archivo\n" +
" Lo que hare será enviar la ruta desde el body, por el momento\n" +
" Mañana nada mas pueda actualizarlo\n" +
" Bajaré visual 19 para trabajar a partir de alli los demas labs\n" +
" Disculpe la molestia\n\n" +
"El formato de entrada sera un JSON\n" +

"{\n"+

    "FilePath : direccion del archivo a comprimir o descompimir\n"+
"}";
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
