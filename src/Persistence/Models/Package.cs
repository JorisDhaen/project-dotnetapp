using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Server.Models;

public partial class Package
{
    public Package(string idpackage, double height, double width, double debth, double price)
    {
        Idpackage = idpackage;
        Height = height;
        Width = width;
        Debth = debth;
        Price = price;
    }

    public string Idpackage { get; set; } = null!;

    public double Height { get; set; }

    public double Width { get; set; }

    public double Debth { get; set; }

    public double Price { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
