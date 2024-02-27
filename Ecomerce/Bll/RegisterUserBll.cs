using Ecomerce.DBModels;
using Ecomerce.Helpers;
using Ecomerce.Models.LoginProcess;
using Ecomerce.Repository;
using MigracionModelDB.DBModels;
using ServicioApiCurso.Helpers;
using ServicioApiCurso.Models.General;

namespace Ecomerce.Bll
{
    public class RegisterUserBll
    {
        DbproductContext ContextDB;

        public RegisterUserBll(DbproductContext _Context)
        {
            ContextDB = _Context;
        }

        public GenericResponse<bool> RegisterUser(LoginRequestModel LoginModel)
        {
            ContextDB.Database.BeginTransaction();
            try
            {
                UserRepository UserRep = new UserRepository();
                TUser? ValidateExisteUser = UserRep.FindUserByUserName(ContextDB, LoginModel);
                if(ValidateExisteUser != null)
                {
                    return new GenericResponse<bool>
                    {
                        statusCode = 500,
                        data = false,
                        message= MessageHelper.RegisterUserErrorExisteUser,
                    };
                }

                TUser UserReg = new TUser();
                UserReg.UserName = LoginModel.userName;
                UserReg.Password = (new MethodsEncryptHelper()).EncryptPassword(LoginModel.password);
                UserReg.Status = "A";

                UserRep.RegisterUser(ContextDB, UserReg);

                ContextDB.Database.CommitTransaction();

                return new GenericResponse<bool>
                {
                    statusCode = 200,
                    data = true,
                    message = "",
                };
            } catch (Exception ex) {
                ContextDB.Database.RollbackTransaction();
                return new GenericResponse<bool>
                {
                    statusCode = 500,
                    data = false,
                    message = MessageHelper.RegisterUserErrorEx,
                };
            }
        }
    }
}
