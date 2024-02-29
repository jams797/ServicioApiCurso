using Ecomerce.DBModels;

namespace Ecomerce.Repository
{
    public class InvoiceHeadRepository
    {
        public int CreateHeadInvoice(DbproductContext Context, double Total, int UserId)
        {
            InvoiceHead InvoiceH = new InvoiceHead();
            InvoiceH.Total = Total;
            InvoiceH.DateTime = DateTime.Now;
            InvoiceH.UserId = UserId;

            Context.InvoiceHeads.Add(InvoiceH);

            Context.SaveChanges();

            return InvoiceH.InvoiceHeadId;
        }

        public List<InvoiceHead> GetAllInvoiceByUserId(DbproductContext Context, int userId)
        {
            return Context.InvoiceHeads.Where(x => x.UserId == userId).ToList();
        }

        public InvoiceHead? GetInvoiceByUserIdAndHeadId(DbproductContext Context, int HeadId, int userId)
        {
            return Context.InvoiceHeads.SingleOrDefault(x => x.UserId == userId && x.InvoiceHeadId == HeadId);
        }
    }
}
