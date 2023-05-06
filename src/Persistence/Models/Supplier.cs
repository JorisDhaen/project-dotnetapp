using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class Supplier
{
    public string Idsupplier { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Adress { get; set; } = null!;

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
