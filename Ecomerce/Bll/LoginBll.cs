using Ecomerce.DBModels;
using Ecomerce.Models;
using Ecomerce.Repository;
using MigracionModelDB.DBModels;
using ServicioApiCurso.Models.General;

namespace Ecomerce.Bll
{
    public class LoginBll
    {

        DbproductContext ContextDB;

        public LoginBll(DbproductContext _Context)
        {
            ContextDB = _Context;
        }

        public GenericResponse<TUser> GetLoginUSer(LoginRequestModel ReqModel)
        {
            try
            {
                TUser TU = new TUser();


                var UserRep = new UserRepository();

                string MessageError = UserRep.LoginUser(ContextDB, ReqModel, ref TU);

                if(MessageError == null)
                {
                    return new GenericResponse<TUser>
                    {
                        statusCode = 200,
                        data = TU,
                    };
                } else
                {
                    return new GenericResponse<TUser>
                    {
                        statusCode = 500,
                        message = MessageError,
                    };
                }

            } catch (Exception ex) {
                return new GenericResponse<TUser>
                {
                    statusCode = 500,
                    message = "Error.",
                };
            }
        }
    }
}
