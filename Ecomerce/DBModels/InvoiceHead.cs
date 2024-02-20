using System;
using System.Collections.Generic;

namespace Ecomerce.DBModels;

public partial class InvoiceHead
{
    public int InvoiceHeadId { get; set; }

    public double Total { get; set; }

    public DateTime DateTime { get; set; }

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
}
