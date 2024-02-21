using Ecomerce.DBModels;
using Ecomerce.Models.InvoiceProcess;
using Ecomerce.Repository;
using ServicioApiCurso.Helpers;

namespace Ecomerce.Bll
{
    public class InvoiceBll
    {
        public CreateInvoiceResponse CreateInvoiceModel(DbproductContext Context, List<CreateInvoiceRequest> ListReq)
        {
            Context.Database.BeginTransaction();
            try
            {
                ProductRepository ProductRep = new ProductRepository();
                List<Product> ListProd = ProductRep.GetProductsById(Context, ListReq.Select(x => x.ProductId).ToList());
                if (ListProd.Count != ListReq.Count)
                {
                    return new CreateInvoiceResponse
                    {
                        InvoiceId = null,
                        Message = MessageHelper.ErrorCreateInvoiceProductNotFound,
                    };
                }

                double TotalInvoice = 0;

                List<CreateInvoiceModel> ListProdSend = new List<CreateInvoiceModel>();
                foreach (CreateInvoiceRequest ProductReq in ListReq) {
                    CreateInvoiceModel ModelT = new CreateInvoiceModel();
                    ModelT.ProductId = ProductReq.ProductId;
                    ModelT.Count = ProductReq.Count;
                    ModelT.Price = ListProd.Single(x => x.ProductId == ProductReq.ProductId).Price;

                    TotalInvoice += ModelT.Price * ModelT.Count;

                    ListProdSend.Add(ModelT);
                }

                InvoiceHeadRepository InvoiceHR = new InvoiceHeadRepository();
                int InvoiceHeadId = InvoiceHR.CreateHeadInvoice(Context, TotalInvoice);

                InvoiceDetailRepository InvoiceDR = new InvoiceDetailRepository();
                InvoiceDR.CreateInvoiceDetail(Context, ListProdSend, InvoiceHeadId);

                Context.Database.CommitTransaction();

                return new CreateInvoiceResponse
                {
                    InvoiceId = InvoiceHeadId,
                    Message = "",
                };

            } catch {
                Context.Database.RollbackTransaction();
                return new CreateInvoiceResponse
                {
                    InvoiceId = null,
                    Message = MessageHelper.ErrorCreateInvoice,
                };
            }
        }
    }
}
