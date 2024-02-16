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
        public IActionResult Get(int id)
        {
            GenericResponse<UsersServiceModel?> GenResp = new Models.GenericResponse<UsersServiceModel?>();
            ValidateRequest ValidateReq = new ValidateRequest();

            if (!ValidateReq.ValidateIdUser(id))
            {
                GenResp.statusCode = 500;
                GenResp.message = Message.IdNotValid;
            }
            else
            {

                UsersServiceModel UserModel = UserBll.GetUser(id);

                if (UserModel == null)
                {
                    GenResp.statusCode = 404;
                    GenResp.message = Message.IdNotFound;
                } else
                {
                    GenResp.statusCode = 200;
                    GenResp.data = UserModel;
                    GenResp.message = "";
                }
            }

            return StatusCode(GenResp.statusCode, GenResp);

            //return UserModel;
            //return StatusCode(200, UserModel);
            //return Ok(UserModel);
        }

        // POST api/<UsersServiceController>
        [HttpPost]
        public IActionResult Post([FromBody] UsersServiceModel model)
        {
            UsersBll UserB = new UsersBll();
            UserB.CreateUser(model);
            return StatusCode(200, model);
        }

        // PUT api/<UsersServiceController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserServiceModelReq model)
        {
            GenericResponse<UsersServiceModel?> GenResp = new Models.GenericResponse<UsersServiceModel?>();
            bool Updated = (new UsersBll()).UpdateUser(id, model);
            if (Updated)
            {
                GenResp.statusCode = 200;
                GenResp.message = "";
            }
            else
            {
                GenResp.statusCode = 200;
                GenResp.message = "Error al actualizar";
            }
            return Ok(GenResp);
        }

        // DELETE api/<UsersServiceController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
