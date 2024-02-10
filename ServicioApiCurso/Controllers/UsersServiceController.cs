using LibraryMethod.Helpers;
using Microsoft.AspNetCore.Mvc;
using ServicioApiCurso.Bll;
using ServicioApiCurso.Helpers;
using ServicioApiCurso.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServicioApiCurso.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersServiceController : ControllerBase
    {
        UsersBll UserBll = new UsersBll();

        // GET: <UsersServiceController>
        [HttpGet]
        public List<UsersServiceModel> Get()
        {
            List<UsersServiceModel> ListUser = UserBll.GetUsers();

            return ListUser;
        }

        // GET <UsersServiceController>/5
        [HttpGet("{id}")]
        public dynamic Get(int id)
        {
            ValidateRequest ValidateReq = new ValidateRequest();

            if (!ValidateReq.ValidateIdUser(id))
            {
                return Message.IdNotValid;
            }

            UsersServiceModel UserModel = UserBll.GetUser(id);

            if (UserModel == null)
            {
                return Message.IdNotFound;
            }

            return UserModel;
        }

        // POST api/<UsersServiceController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UsersServiceController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersServiceController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
