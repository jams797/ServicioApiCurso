using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServicioApiCurso.DBModels;
using ServicioApiCurso.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServicioApiCurso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        private readonly DbproductContext _db;

        public ExampleController(DbproductContext dbProductContext)
        {
            _db = dbProductContext;
        }



        // GET: api/<ExampleController>
        [HttpGet]
        public async Task<dynamic> Get()
        {
            //var tmp = (new ConfigurationBuilder()).AddJsonFile("appsettings.json").Build().GetSection("rutas");
            // return tmp.GetValue("deleteUser", "");
            // return tmp["deleteUser"];
            //return tmp.GetValue<int>("tmp");

            return await _db.Categories.ToListAsync();
        }

        // GET api/<ExampleController>/5
        [HttpGet("{id}")]
        public async Task<GenericResponse<List<Product>>> Get(int id)
        {
            List<Product> ListProduct = await _db.Products.Where(k => k.CategoryId == id).ToListAsync();

            GenericResponse<List<Product>> Resp = new GenericResponse<List<Product>>();
            Resp.statusCode = 200;
            Resp.message = "";
            Resp.data = ListProduct;

            return Resp;
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
