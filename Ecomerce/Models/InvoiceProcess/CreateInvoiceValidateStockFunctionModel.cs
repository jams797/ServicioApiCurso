using Ecomerce.DBModels;

namespace Ecomerce.Models.InvoiceProcess
{
    public class CreateInvoiceValidateStockFunctionModel
    {
        // Listado de productos que llega en la petición
        public List<CreateInvoiceRequest> ListReq { get; set; }
        public List<Product> ListProd { get; set; }
        public List<CreateInvoiceModel> ListProdSend { get; set; }
        public double TotalInvoice { get; set; }
        public List<CreateInvoiceModel> ListProdUpdate { get; set; }
    }
}
