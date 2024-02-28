using Ecomerce.Bll;
using Ecomerce.DBModels;
using Ecomerce.Filters;
using Ecomerce.Helpers;
using Ecomerce.Models.ChangePasswordProcess;
using Microsoft.AspNetCore.Mvc;
using ServicioApiCurso.Models.General;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ecomerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChangePasswordController : ControllerBase
    {

        private ChangePasswordBll ChangePassB;

        private readonly DbproductContext _db;

        public ChangePasswordController(DbproductContext dbProductContext)
        {
            _db = dbProductContext;
            ChangePassB = new ChangePasswordBll(dbProductContext);
        }

        // GET: api/<ChangePasswordController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ChangePasswordController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ChangePasswordController>
        [HttpPost]
        [ServiceFilter(typeof(SessionUsuarioFilter))]
        public GenericResponse<bool> Post([FromBody] ChangePasswordRequesModel value)
        {
            var ModelSesion = (new MethodsHelper()).GetModelSesionByToken(HttpContext.Request.Headers["Authorization"].ToString());
            return ChangePassB.ChangePasswordUser(value, ModelSesion.UserId);
        }

        // PUT api/<ChangePasswordController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ChangePasswordController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
