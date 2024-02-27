using Ecomerce.DBModels;
using Ecomerce.Helpers;
using Ecomerce.Models.LoginProcess;
using Ecomerce.Repository;
using MigracionModelDB.DBModels;
using ServicioApiCurso.Helpers;
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

        public GenericResponse<LoginResponseModel> GetLoginUSer(LoginRequestModel ReqModel)
        {
            try
            {


                var UserRep = new UserRepository();

                TUser TU = UserRep.FindUserByUserName(ContextDB, ReqModel);

                if(TU != null)
                {
                    string passFindDecrypt = (new MethodsEncryptHelper()).DencryptPassword(TU.Password);
                    if (passFindDecrypt == ReqModel.password)
                    {
                        if (TU.Status == "A")
                        {
                            return new GenericResponse<LoginResponseModel>
                            {
                                statusCode = 200,
                                data = new LoginResponseModel {
                                    UserName = TU.UserName,
                                    Token = (new MethodsHelper()).CreateTokenSesion(TU.UserId),
                                },
                            };
                        } else
                        {
                            return new GenericResponse<LoginResponseModel>
                            {
                                statusCode = 500,
                                message = MessageHelper.LoginErrorNotActived,
                            };
                        }
                    }
                    else
                    {
                        return new GenericResponse<LoginResponseModel>
                        {
                            statusCode = 500,
                            message = MessageHelper.LoginErrorPassword,
                        };
                    }
                } else
                {
                    return new GenericResponse<LoginResponseModel>
                    {
                        statusCode = 500,
                        message = MessageHelper.LoginErrorUserName,
                    };
                }

            } catch (Exception ex) {
                return new GenericResponse<LoginResponseModel>
                {
                    statusCode = 500,
                    message = "Error.",
                };
            }
        }
    }
}
