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
                List<CreateInvoiceModel> ListProdUpdate = new List<CreateInvoiceModel>();

                List<Product> ListProdModel = ListProd;

                CreateInvoiceValidateStockFunctionModel ModelValStock = new CreateInvoiceValidateStockFunctionModel
                {
                    ListReq = ListReq,
                    ListProd = ListProdModel,
                    ListProdSend = ListProdSend,
                    TotalInvoice = TotalInvoice,
                    ListProdUpdate = ListProdUpdate,
                };
                CreateInvoiceResponse? ProcForeachProduct = ValidateInvoiceStockProduct(Context, ref ModelValStock);
                if (ProcForeachProduct != null)
                {
                    return ProcForeachProduct;
                }
                ListProdUpdate = ModelValStock.ListProdUpdate;
                ListProdSend = ModelValStock.ListProdSend;
                TotalInvoice = ModelValStock.TotalInvoice;

                InvoiceHeadRepository InvoiceHR = new InvoiceHeadRepository();
                int InvoiceHeadId = InvoiceHR.CreateHeadInvoice(Context, TotalInvoice, UserId);

                InvoiceDetailRepository InvoiceDR = new InvoiceDetailRepository();
                InvoiceDR.CreateInvoiceDetail(Context, ListProdSend, InvoiceHeadId);

                ProductRep.UpdateProductsCount(Context, ListProd, ListProdUpdate);

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
            List<CreateInvoiceModel> ListProductUpdate = new List<CreateInvoiceModel>();
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

                //ProductFindDB.Count = ProductFindDB.Count - ModelT.Count;
                CreateInvoiceModel ProdModel = new CreateInvoiceModel();
                ProdModel.ProductId = ProductFindDB.ProductId;
                ProdModel.Count = ProductFindDB.Count - ModelT.Count;

                ListProdModif.Add(ProductFindDB);
                ListProductUpdate.Add(ProdModel);
            }
            ModelValStock.ListProd = ListProdModif;
            ModelValStock.ListProdUpdate = ListProductUpdate;
            return null;
        }
    }
}
