using LibraryMethod.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServicioApiCurso.Bll;
using ServicioApiCurso.DBModels;
using ServicioApiCurso.Helpers;
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

            // _db.Products.ToList().Take(10);  // NO se debe hacer
            // _db.Products.Take(10).ToList(); // Recomendable

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
        public GenericResponse<bool?> Put(int id, [FromBody] InsertProductModelReq Model)
        {
            GenericResponse<bool?> GenResp = new Models.GenericResponse<bool?>();
            ValidateRequest ValidateReq = new ValidateRequest();

            if (!ValidateReq.ValidateIdUser(id))
            {
                GenResp.statusCode = 500;
                GenResp.message = Message.IdNotValid;
            } else
            {
                ProductBll ProductB = new ProductBll();
                bool update = ProductB.UpdateProduct(_db, Model, id);
                if (update)
                {
                    GenResp.statusCode = 200;
                    GenResp.data = true;
                    GenResp.message = "";
                }
                else
                {
                    GenResp.statusCode = 500;
                    GenResp.data = false;
                    GenResp.message = Message.ErrorUpdateProduct;
                }
            }
            return GenResp;
        }

        // DELETE api/<ExampleController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
