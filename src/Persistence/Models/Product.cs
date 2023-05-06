using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class Product
{
    public string Idproduct { get; set; } = null!;

    public int ProductCatergoryId { get; set; }

    public string Name { get; set; } = null!;

    public double UnitPrice { get; set; }

    public int LeftInStock { get; set; }

    public string? Description { get; set; }

    public double Height { get; set; }

    public double Width { get; set; }

    public double Depth { get; set; }

    public string SupplierId { get; set; } = null!;

    public virtual ICollection<Orderitem> Orderitems { get; } = new List<Orderitem>();

    public virtual Supplier Supplier { get; set; } = null!;
}
