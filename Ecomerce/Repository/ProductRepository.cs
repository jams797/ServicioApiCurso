using Ecomerce.DBModels;

namespace Ecomerce.Repository
{
    public class ProductRepository
    {
        public List<Product> GetProductsById(DbproductContext Context, List<int> Ids)
        {
            return Context.Products.Where(x => Ids.Contains(x.ProductId)).ToList();
        }
    }
}
