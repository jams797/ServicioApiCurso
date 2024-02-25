using Ecomerce.Models.General;
using LibraryMethod.Helpers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ecomerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestMethodsController : ControllerBase
    {
        // GET: api/<TestMethodsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TestMethodsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TestMethodsController>
        [HttpPost]
        public string Post([FromBody] TextEncryptRequestModel value)
        {
            var SectionKey = (new ConfigurationBuilder()).AddJsonFile("appsettings.json").Build().GetSection("passwordEncrypt");
            var EncryptH = new EncryptHelper
            {
                EncKey = SectionKey.GetValue<string>("key"),
                EncMacKey = SectionKey.GetValue<string>("macKey"),
            };

            if (value.encrypt)
            {
                return EncryptH.EncryptValue(value.value);
            } else
            {
                return EncryptH.DencryptValue(value.value);
            }
        }

        // PUT api/<TestMethodsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TestMethodsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
