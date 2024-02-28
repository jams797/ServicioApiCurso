using Ecomerce.DBModels;
using Ecomerce.Helpers;
using Ecomerce.Models.ChangePasswordProcess;
using Ecomerce.Repository;
using MigracionModelDB.DBModels;
using ServicioApiCurso.Helpers;
using ServicioApiCurso.Models.General;

namespace Ecomerce.Bll
{
    public class ChangePasswordBll
    {

        DbproductContext ContextDB;

        public ChangePasswordBll(DbproductContext _Context)
        {
            ContextDB = _Context;
        }

        public GenericResponse<bool> ChangePasswordUser(ChangePasswordRequesModel model, int UserId)
        {
            ContextDB.Database.BeginTransaction();
            try
            {
                UserRepository UserRep = new UserRepository();
                TUser UserFind = UserRep.GetUserById(ContextDB, UserId);
                if(UserFind == null)
                {
                    return new GenericResponse<bool>
                    {
                        statusCode = 500,
                        data = false,
                        message = MessageHelper.ChangePasswordErrorId,
                    };
                }

                MethodsEncryptHelper MethodsEncryptHelp = new MethodsEncryptHelper();

                string passwordUserOld = MethodsEncryptHelp.DencryptPassword(UserFind.Password);
                if (passwordUserOld != model.passwordOld)
                {
                    return new GenericResponse<bool>
                    {
                        statusCode = 500,
                        data = false,
                        message = MessageHelper.ChangePasswordErrorPassword,
                    };
                }

                string encryptNewPassword = MethodsEncryptHelp.EncryptPassword(model.passwordNew);

                UserRep.ChangePassword(ContextDB, UserId, encryptNewPassword);

                ContextDB.Database.CommitTransaction();

                return new GenericResponse<bool>
                {
                    statusCode = 200,
                    data = true,
                    message = "",
                };
            } catch (Exception ex)
            {
                return new GenericResponse<bool>
                {
                    statusCode = 500,
                    data = false,
                    message = MessageHelper.ChangePasswordErrorEx,
                };
            }
        }
    }
}
