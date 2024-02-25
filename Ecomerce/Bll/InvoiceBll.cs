using Ecomerce.DBModels;
using Ecomerce.Models.InvoiceProcess;
using Ecomerce.Repository;
using ServicioApiCurso.Helpers;

namespace Ecomerce.Bll
{
    public class InvoiceBll
    {

        ProductRepository ProductRep = new ProductRepository();

        public CreateInvoiceResponse CreateInvoiceModel(DbproductContext Context, List<CreateInvoiceRequest> ListReq, int UserId)
        {
            Context.Database.BeginTransaction();
            try
            {
                List<Product> ListProd = new List<Product>();
                CreateInvoiceResponse? ValidateProd = ValidateIdProducstDB(Context, ListReq, ref ListProd);
                if (ValidateProd != null)
                {
                    return ValidateProd;
                }

                double TotalInvoice = 0;

                List<CreateInvoiceModel> ListProdSend = new List<CreateInvoiceModel>();

                List<Product> ListProdUpdateCount = ListProd;

                CreateInvoiceValidateStockFunctionModel ModelValStock = new CreateInvoiceValidateStockFunctionModel
                {
                    ListReq = ListReq,
                    ListProd = ListProdUpdateCount,
                    ListProdSend = ListProdSend,
                    TotalInvoice = TotalInvoice,
                };
                CreateInvoiceResponse? ProcForeachProduct = ValidateInvoiceStockProduct(Context, ref ModelValStock);
                if (ProcForeachProduct != null)
                {
                    return ProcForeachProduct;
                }
                ListProdUpdateCount = ModelValStock.ListProd;
                ListProdSend = ModelValStock.ListProdSend;
                TotalInvoice = ModelValStock.TotalInvoice;

                InvoiceHeadRepository InvoiceHR = new InvoiceHeadRepository();
                int InvoiceHeadId = InvoiceHR.CreateHeadInvoice(Context, TotalInvoice, UserId);

                InvoiceDetailRepository InvoiceDR = new InvoiceDetailRepository();
                InvoiceDR.CreateInvoiceDetail(Context, ListProdSend, InvoiceHeadId);

                ProductRep.UpdateProductsCount(Context, ListProd, ListProdUpdateCount);

                Context.Database.CommitTransaction();

                return new CreateInvoiceResponse
                {
                    InvoiceId = InvoiceHeadId,
                    Message = "",
                };

            } catch (Exception ex) {
                Context.Database.RollbackTransaction();
                return new CreateInvoiceResponse
                {
                    InvoiceId = null,
                    Message = MessageHelper.ErrorCreateInvoice,
                };
            }
        }

        public CreateInvoiceResponse? ValidateIdProducstDB(DbproductContext Context, List<CreateInvoiceRequest> ListReq, ref List<Product> ListProd)
        {
            ListProd = ProductRep.GetProductsById(Context, ListReq.Select(x => x.ProductId).ToList());
            if (ListProd.Count != ListReq.Count)
            {
                return new CreateInvoiceResponse
                {
                    InvoiceId = null,
                    Message = MessageHelper.ErrorCreateInvoiceProductNotFound,
                };
            }
            return null;
        }

        public CreateInvoiceResponse? ValidateInvoiceStockProduct(DbproductContext Context, ref CreateInvoiceValidateStockFunctionModel ModelValStock)
        {
            List<Product> ListProdModif = new List<Product>();
            foreach (CreateInvoiceRequest ProductReq in ModelValStock.ListReq)
            {
                Product ProductFindDB = ModelValStock.ListProd.Single(x => x.ProductId == ProductReq.ProductId);

                if(ProductFindDB.Count < ProductReq.Count)
                {
                    return new CreateInvoiceResponse
                    {
                        InvoiceId = null,
                        Message = MessageHelper.ErrorCreateInvoiceProductExecendStock,
                    };
                }

                CreateInvoiceModel ModelT = new CreateInvoiceModel();
                ModelT.ProductId = ProductReq.ProductId;
                ModelT.Count = ProductReq.Count;
                ModelT.Price = ProductFindDB.Price;

                ModelValStock.TotalInvoice += ModelT.Price * ModelT.Count;

                ModelValStock.ListProdSend.Add(ModelT);

                ProductFindDB.Count = ProductFindDB.Count - ModelT.Count;
                ListProdModif.Add(ProductFindDB);
            }
            ModelValStock.ListProd = ListProdModif;
            return null;
        }
    }
}
