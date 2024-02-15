﻿using LibraryMethod.Helpers;
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
        public void Post([FromBody] string value)
        {
            //return StatusCode(418, null);
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
