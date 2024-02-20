using Ecomerce.DBModels;
using Ecomerce.Models.InvoiceProcess;

namespace Ecomerce.Repository
{
    public class InvoiceDetailRepository
    {
        public void CreateInvoiceDetail(DbproductContext Context, List<CreateInvoiceModel> ListDetail, int InvoiceHeadId)
        {
            List<InvoiceDetail> ListID = new List<InvoiceDetail>();

            foreach (CreateInvoiceModel Item in ListDetail) {
                InvoiceDetail InvoiceD = new InvoiceDetail();
                InvoiceD.InvoiceHeadId = InvoiceHeadId;
                InvoiceD.Price = Item.Price;
                InvoiceD.Count = Item.Count;
                InvoiceD.ProductId = Item.ProductId;

                ListID.Add(InvoiceD);
            }

            Context.InvoiceDetails.AddRange(ListID);

            Context.SaveChanges();
        }
    }
}
