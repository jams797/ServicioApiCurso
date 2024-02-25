using System;
using System.Collections.Generic;

namespace MigracionModelDB.DBModels;

public partial class TUser
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual ICollection<InvoiceHead> InvoiceHeads { get; set; } = new List<InvoiceHead>();
}
