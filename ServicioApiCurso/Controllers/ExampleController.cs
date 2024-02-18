using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServicioApiCurso.DBModels;
using ServicioApiCurso.Models;
using ServicioApiCurso.Repository;

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

            //return await _db.Categories.ToListAsync();

            var tmp = (from catg in _db.Categories
             join prod in _db.Products on catg.CategoryId equals prod.CategoryId
             select new
             {
                 catg,
                 prod,
             }).ToList();

            List<ProductCategoryResp> ListProdCatg = [];

            foreach(var item in tmp) {
                ListProdCatg.Add(new ProductCategoryResp
                {
                    IdProduct = item.prod.ProductId,
                    NameProduct = item.prod.Name,
                    NameCategory = item.catg.Name,
                });
            }

            return ListProdCatg;
        }

        // GET api/<ExampleController>/5
        [HttpGet("{id}")]
        public async Task<GenericResponse<List<Product>>> Get(int id)
        {
            // List<Product> ListProduct = await _db.Products.Where(k => k.CategoryId == id).ToListAsync();

            List<Product> ListProduct =  (from prod in _db.Products
                                          where prod.CategoryId == id
                                          select prod).ToList();

            GenericResponse<List<Product>> Resp = new GenericResponse<List<Product>>();
            Resp.statusCode = 200;
            Resp.message = "";
            Resp.data = ListProduct;

            return Resp;
        }

        // POST api/<ExampleController>
        [HttpPost]
        public int Post([FromBody] InsertProductModelReq Model)
        {
            ProductRepository ProductRepos = new ProductRepository();
            return ProductRepos.InsertProduct(_db, Model);
        }

        // PUT api/<ExampleController>/5
        [HttpPut("{id}")]
        public bool Put(int id, [FromBody] InsertProductModelReq Model)
        {
            ProductRepository ProductRepos = new ProductRepository();
            return ProductRepos.UpdateProduct(_db, Model, id);
        }

        // DELETE api/<ExampleController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
