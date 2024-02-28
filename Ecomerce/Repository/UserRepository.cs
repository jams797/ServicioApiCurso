using Ecomerce.DBModels;
using Ecomerce.Helpers;
using Ecomerce.Models.LoginProcess;
using MigracionModelDB.DBModels;
using ServicioApiCurso.Helpers;

namespace Ecomerce.Repository
{
    public class UserRepository
    {
        public TUser? FindUserByUserName(DbproductContext contextDB, LoginRequestModel ModelReq)
        {
            var UserFind = contextDB.TUsers.SingleOrDefault(x => x.UserName == ModelReq.userName);

            return UserFind;
        }

        public TUser? RegisterUser(DbproductContext contextDB, TUser TU) {
            contextDB.TUsers.Add(TU);

            contextDB.SaveChanges();

            return TU;
        }

        public TUser GetUserById(DbproductContext contextDB, int userId)
        {
            TUser TU = contextDB.TUsers.SingleOrDefault(x => x.UserId == userId);

            return TU;
        }

        public void ChangePassword(DbproductContext contextDB, int userId, string password)
        {
            TUser TU = contextDB.TUsers.Single(x => x.UserId == userId);

            TU.Password = password;

            contextDB.SaveChanges();
        }
    }
}
