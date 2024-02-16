using ServicioApiCurso.Models;

namespace ServicioApiCurso.Bll
{
    public class UsersBll
    {
        static List<UsersServiceModel> ListUser = new List<UsersServiceModel>
        {
            new UsersServiceModel
            {
                Id = 1,
                Name = "José",
                LastName = "Morán",
                Age = 26,
            },
            new UsersServiceModel
            {
                Id = 2,
                Name = "José3",
                LastName = "Morán2",
                Age = 40,
            },
            new UsersServiceModel
            {
                Id = 3,
                Name = "Marco",
                LastName = "Polo",
                Age = 20,
            },
        };
        public List<UsersServiceModel> GetUsers()
        {

            return ListUser.Skip(1).ToList();
        }

        public UsersServiceModel? GetUser(int Id)
        {
            try
            {
                return ListUser.Where(x => x.Id == Id).First();
            } catch (Exception ex)
            {
                return null;
            }
        }

        public void CreateUser(UsersServiceModel model)
        {
            ListUser.Add(model);
        }
    }
}
