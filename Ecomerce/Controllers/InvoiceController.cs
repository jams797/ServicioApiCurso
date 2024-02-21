using Ecomerce.Bll;
using Ecomerce.DBModels;
using Ecomerce.Models.InvoiceProcess;
using Microsoft.AspNetCore.Mvc;
using ServicioApiCurso.Models.General;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ecomerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            return new string[] { "value1", "value2" };
        }

        // GET api/<InvoiceController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<InvoiceController>
        [HttpPost]
        public GenericResponse<CreateInvoiceResponse> Post([FromBody] List<CreateInvoiceRequest> ReqModel)
        {
            CreateInvoiceResponse Resp = InvoiceB.CreateInvoiceModel(_db, ReqModel);
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
