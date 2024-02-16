using System;
using System.Collections.Generic;

namespace ServicioApiCurso.DBModels;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public double Price { get; set; }

    public int Count { get; set; }

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;
}
