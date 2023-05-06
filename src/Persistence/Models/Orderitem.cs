using Domain;
using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class Orderitem
{
    public Orderitem(string idorderItem, int quantity, double totalPrice, string productId, string? orderId)
    {
        IdorderItem = idorderItem;
        Quantity = quantity;
        TotalPrice = totalPrice;
        ProductId = productId;
        OrderId = orderId;
    }

    public string IdorderItem { get; set; } = null!;

    public int Quantity { get; set; }

    public double TotalPrice { get; set; }

    public string ProductId { get; set; } = null!;

    public string? OrderId { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product Product { get; set; } = null!;
}
