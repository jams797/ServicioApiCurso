using System;
using System.Collections.Generic;

namespace MigracionModelDB.DBModels;

public partial class InvoiceDetail
{
    public int InvoiceDetailId { get; set; }

    public int ProductId { get; set; }

    public double Count { get; set; }

    public double Price { get; set; }

    public int InvoiceHeadId { get; set; }

    public virtual InvoiceHead InvoiceHead { get; set; } = null!;
}
