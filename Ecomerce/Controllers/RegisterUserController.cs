using Ecomerce.Bll;
using Ecomerce.DBModels;
using Ecomerce.Models.LoginProcess;
using Microsoft.AspNetCore.Mvc;
using ServicioApiCurso.Helpers;
using ServicioApiCurso.Models.General;
using System.Text.RegularExpressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ecomerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterUserController : ControllerBase
    {

        private RegisterUserBll RegisterB;

        private readonly DbproductContext _db;

        public RegisterUserController(DbproductContext dbProductContext)
        {
            _db = dbProductContext;
            RegisterB = new RegisterUserBll(dbProductContext);
        }

        // GET: api/<RegisterUserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RegisterUserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RegisterUserController>
        [HttpPost]
        public GenericResponse<bool> Post([FromBody] LoginRequestModel value)
        {
            if(value.userName.Length < 4 || value.userName.Length > 16)
            {
                return new GenericResponse<bool>
                {
                    statusCode = 400,
                    data = false,
                    message = MessageHelper.RegisterUserErrorParamUserName,
                };
            }
            if (value.password.Length < 8 || value.password.Length > 16 || !Regex.IsMatch(value.password, "^[a-zA-Z_0-9]+$"))
            {
                return new GenericResponse<bool>
                {
                    statusCode = 400,
                    data = false,
                    message = MessageHelper.RegisterUserErrorParamPassword,
                };
            }
            return RegisterB.RegisterUser(value);
        }

        // PUT api/<RegisterUserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RegisterUserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
