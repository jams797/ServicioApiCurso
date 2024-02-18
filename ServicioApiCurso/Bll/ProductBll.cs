using ServicioApiCurso.DBModels;
using ServicioApiCurso.Models;
using ServicioApiCurso.Repository;

namespace ServicioApiCurso.Bll
{
    public class ProductBll
    {
        public bool UpdateProduct(DbproductContext Context, InsertProductModelReq Model, int IdProduct)
        {
            Context.Database.BeginTransaction();
            try
            {
                ProductRepository ProductRepos = new ProductRepository();
                bool update = ProductRepos.UpdateProduct(Context, Model, IdProduct);
                Context.Database.CommitTransaction();
                return update;
            } catch {
                Context.Database.RollbackTransaction();
                return false;
            }
        }
    }
}
