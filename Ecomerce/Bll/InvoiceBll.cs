﻿using Ecomerce.DBModels;
using Ecomerce.Models.InvoiceProcess;
using Ecomerce.Repository;
using ServicioApiCurso.Helpers;

namespace Ecomerce.Bll
{
    public class InvoiceBll
    {

        ProductRepository ProductRep = new ProductRepository();

        public CreateInvoiceResponse CreateInvoiceModel(DbproductContext Context, List<CreateInvoiceRequest> ListReq)
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

                CreateInvoiceResponse? ProcForeachProduct = ValidateInvoiceStockProduct(Context, ListReq, ref ListProdUpdateCount, ref ListProdSend, ref TotalInvoice);
                if (ProcForeachProduct != null)
                {
                    return ProcForeachProduct;
                }

                InvoiceHeadRepository InvoiceHR = new InvoiceHeadRepository();
                int InvoiceHeadId = InvoiceHR.CreateHeadInvoice(Context, TotalInvoice);

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

        public CreateInvoiceResponse? ValidateInvoiceStockProduct(DbproductContext Context, List<CreateInvoiceRequest> ListReq, ref List<Product> ListProd, ref List<CreateInvoiceModel> ListProdSend, ref double TotalInvoice)
        {
            List<Product> ListProdModif = new List<Product>();
            foreach (CreateInvoiceRequest ProductReq in ListReq)
            {
                Product ProductFindDB = ListProd.Single(x => x.ProductId == ProductReq.ProductId);

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

                TotalInvoice += ModelT.Price * ModelT.Count;

                ListProdSend.Add(ModelT);

                ProductFindDB.Count = ProductFindDB.Count - ModelT.Count;
                ListProdModif.Add(ProductFindDB);
            }
            ListProd = ListProdModif;
            return null;
        }
    }
}
