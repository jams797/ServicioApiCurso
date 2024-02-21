using Ecomerce.DBModels;
using System.Linq;

namespace Ecomerce.Repository
{
    public class ProductRepository
    {
        public List<Product> GetProductsById(DbproductContext Context, List<int> Ids)
        {
            return Context.Products.Where(x => Ids.Contains(x.ProductId)).ToList();
        }

        public void UpdateProductsCount(DbproductContext Context, List<Product> ListProd, List<Product> ListProdUpdate)
        {
            foreach (Product Prd in ListProd)
            {
                Prd.Count = ListProdUpdate.Single(x => x.ProductId == Prd.ProductId).Count;
            }
            Context.SaveChanges();
        }
    }
}
