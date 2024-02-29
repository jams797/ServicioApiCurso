using Ecomerce.DBModels;
using Ecomerce.Models.InvoiceProcess;
using Ecomerce.Repository;
using ServicioApiCurso.Helpers;
using ServicioApiCurso.Models.General;

namespace Ecomerce.Bll
{
    public class InvoiceBll
    {

        ProductRepository ProductRep = new ProductRepository();

        DbproductContext ContextDB;

        public InvoiceBll(DbproductContext _Context)
        {
            ContextDB = _Context;
        }

        public CreateInvoiceResponse CreateInvoiceModel(List<CreateInvoiceRequest> ListReq, int UserId)
        {
            ContextDB.Database.BeginTransaction();
            try
            {
                List<Product> ListProd = new List<Product>();
                CreateInvoiceResponse? ValidateProd = ValidateIdProducstDB(ContextDB, ListReq, ref ListProd);
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
                CreateInvoiceResponse? ProcForeachProduct = ValidateInvoiceStockProduct(ContextDB, ref ModelValStock);
                if (ProcForeachProduct != null)
                {
                    return ProcForeachProduct;
                }
                ListProdUpdate = ModelValStock.ListProdUpdate;
                ListProdSend = ModelValStock.ListProdSend;
                TotalInvoice = ModelValStock.TotalInvoice;

                InvoiceHeadRepository InvoiceHR = new InvoiceHeadRepository();
                int InvoiceHeadId = InvoiceHR.CreateHeadInvoice(ContextDB, TotalInvoice, UserId);

                InvoiceDetailRepository InvoiceDR = new InvoiceDetailRepository();
                InvoiceDR.CreateInvoiceDetail(ContextDB, ListProdSend, InvoiceHeadId);

                ProductRep.UpdateProductsCount(ContextDB, ListProd, ListProdUpdate);

                ContextDB.Database.CommitTransaction();

                return new CreateInvoiceResponse
                {
                    InvoiceId = InvoiceHeadId,
                    Message = "",
                };

            } catch (Exception ex) {
                ContextDB.Database.RollbackTransaction();
                return new CreateInvoiceResponse
                {
                    InvoiceId = null,
                    Message = MessageHelper.ErrorCreateInvoice,
                };
            }
        }

        public GenericResponse<List<InvoiceHead>> GetAllInvoiceByUserId(int UserId)
        {
            try
            {
                InvoiceHeadRepository InvoiceHRep = new InvoiceHeadRepository();
                List<InvoiceHead> ListInvoices = InvoiceHRep.GetAllInvoiceByUserId(ContextDB, UserId);
                return new GenericResponse<List<InvoiceHead>>
                {
                    statusCode = 200,
                    data = ListInvoices,
                    message = "",
                };
            } catch (Exception ex)
            {
                return new GenericResponse<List<InvoiceHead>>
                {
                    statusCode = 500,
                    data = null,
                    message = MessageHelper.GetInvoiceErrorHead,
                };
            }
        }

        public GenericResponse<List<InvoiceDetailResponse>> GetInvoiceDetailByHeadId(int InvoiceHeadId, int UserId)
        {
            try
            {
                InvoiceHeadRepository InvoiceHRep = new InvoiceHeadRepository();
                InvoiceHead InvoiceFind = InvoiceHRep.GetInvoiceByUserIdAndHeadId(ContextDB, InvoiceHeadId, UserId);
                if (InvoiceFind == null)
                {
                    return new GenericResponse<List<InvoiceDetailResponse>>
                    {
                        statusCode = 500,
                        data = null,
                        message = MessageHelper.GetInvoiceErrorDetailNotUser,
                    };
                }
                InvoiceDetailRepository InvoiceDRep = new InvoiceDetailRepository();
                List<InvoiceDetail> ListDetails = InvoiceDRep.GetInvoiceDetailByHeadId(ContextDB, InvoiceHeadId);

                List<InvoiceDetailResponse> ListReturn = [];
                foreach(var Item in ListDetails)
                {
                    ListReturn.Add(new InvoiceDetailResponse
                    {
                        DetailId = Item.InvoiceHeadId,
                        Price = Item.Price,
                        ProductId = Item.ProductId,
                    });
                }
                return new GenericResponse<List<InvoiceDetailResponse>>
                {
                    statusCode = 200,
                    data = ListReturn,
                    message = "",
                };
            }
            catch (Exception ex)
            {
                return new GenericResponse<List<InvoiceDetailResponse>>
                {
                    statusCode = 500,
                    data = null,
                    message = MessageHelper.GetInvoiceErrorDetail,
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
