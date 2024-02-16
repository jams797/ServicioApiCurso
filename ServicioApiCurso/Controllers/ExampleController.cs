﻿using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServicioApiCurso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        // GET: api/<ExampleController>
        [HttpGet]
        public dynamic Get()
        {
            var tmp = (new ConfigurationBuilder()).AddJsonFile("appsettings.json").Build().GetSection("BD");
            return tmp.GetValue("name", "");
        }

        // GET api/<ExampleController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ExampleController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ExampleController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ExampleController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}