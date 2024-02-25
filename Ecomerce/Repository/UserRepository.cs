using Ecomerce.DBModels;
using Ecomerce.Helpers;
using Ecomerce.Models.LoginProcess;
using MigracionModelDB.DBModels;
using ServicioApiCurso.Helpers;

namespace Ecomerce.Repository
{
    public class UserRepository
    {
        public TUser? LoginUser(DbproductContext contextDB, LoginRequestModel ModelReq)
        {
            var UserFind = contextDB.TUsers.SingleOrDefault(x => x.UserName == ModelReq.userName);

            return UserFind;
        }
    }
}
