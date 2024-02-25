using Ecomerce.DBModels;
using Ecomerce.Helpers;
using Ecomerce.Models;
using MigracionModelDB.DBModels;
using ServicioApiCurso.Helpers;

namespace Ecomerce.Repository
{
    public class UserRepository
    {
        public string? LoginUser(DbproductContext contextDB, LoginRequestModel ModelReq, ref TUser? UserRep)
        {
            var UserFind = contextDB.TUsers.SingleOrDefault(x => x.UserName == ModelReq.userName);

            if(UserFind != null)
            {
                string passFindDecrypt = (new MethodsEncryptHelper()).DencryptPassword(UserFind.Password);

                if(passFindDecrypt == ModelReq.password)
                {
                    UserRep = UserFind;
                    return null;
                } else
                {
                    return MessageHelper.LoginErrorPassword;
                }
            } else
            {
                return MessageHelper.LoginErrorUserName;
            }
        }
    }
}
