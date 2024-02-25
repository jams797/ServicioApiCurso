using Ecomerce.Bll;
using Ecomerce.DBModels;
using Ecomerce.Filters;
using Ecomerce.Helpers;
using Ecomerce.Models.InvoiceProcess;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ServicioApiCurso.Models.General;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ecomerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(SessionUsuarioFilter))]
    public class InvoiceController : ControllerBase
    {
        InvoiceBll InvoiceB = new InvoiceBll();

        private readonly DbproductContext _db;

        public InvoiceController(DbproductContext dbProductContext)
        {
            _db = dbProductContext;
        }

        // GET: api/<InvoiceController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            Console.WriteLine("Ruta");
            return new string[] { "value1", "value2" };
        }

        // GET api/<InvoiceController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            Console.WriteLine("Ruta 2");
            return "value";
        }

        // POST api/<InvoiceController>
        [HttpPost]
        public GenericResponse<CreateInvoiceResponse> Post([FromBody] List<CreateInvoiceRequest> ReqModel)
        {
            var ModelSesion = (new MethodsHelper()).GetModelSesionByToken(HttpContext.Request.Headers["Authorization"].ToString());
            CreateInvoiceResponse Resp = InvoiceB.CreateInvoiceModel(_db, ReqModel, ModelSesion.UserId);
            if(Resp.InvoiceId != null)
            {
                return new GenericResponse<CreateInvoiceResponse>
                {
                    statusCode = 200,
                    data = Resp,
                    message = "",
                };
            } else
            {
                return new GenericResponse<CreateInvoiceResponse>
                {
                    statusCode = 500,
                    data = null,
                    message = Resp.Message,
                };
            }
        }

        // PUT api/<InvoiceController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<InvoiceController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
