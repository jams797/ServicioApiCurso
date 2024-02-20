namespace Ecomerce.Models.InvoiceProcess
{
    public class CreateInvoiceModel
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
    }
}
