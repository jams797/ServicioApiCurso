using ServicioApiCurso.DBModels;
using ServicioApiCurso.Models;

namespace ServicioApiCurso.Repository
{
    public class ProductRepository
    {
        public int InsertProduct(DbproductContext Context, InsertProductModelReq Model)
        {
            Product Product = new Product
            {
                Name = Model.NameProduct,
                Price = Model.Price,
                Count = Model.Cant,
                CategoryId = Model.IdCategory,
            };

            Context.Products.Add(Product);

            Context.SaveChanges();

            return Product.ProductId;
        }


        public bool UpdateProduct(DbproductContext Context, InsertProductModelReq Model, int IdProduct)
        {
            Context.Database.BeginTransaction();
            Product ProductFind = Context.Products.Where(x => x.ProductId == IdProduct).FirstOrDefault();

            if (ProductFind != null)
            {
                ProductFind.Name = Model.NameProduct;
                ProductFind.Price = Model.Price;
                ProductFind.Count = Model.Cant;

                Context.SaveChanges();

                Context.Database.CommitTransaction();

                return true;
            } else
            {
                return false;
            }
        }
    }
}
