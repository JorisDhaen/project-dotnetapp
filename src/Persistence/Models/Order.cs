using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class Order
{
    public Order(string idorder, double netPrice, double totalAmount, DateTime orderDate, DateTime statusDate, string? deliveryAdress, string? invoiceAdress, string orderStatus,string userId)
    {
        Idorder = idorder;
        NetPrice = netPrice;
        OrderDate = orderDate;
        OrderStatus = orderStatus;
        StatusDate = statusDate;
        DeliveryAdress = deliveryAdress;
        InvoiceAdress = invoiceAdress;
        TotalAmount = totalAmount;
        UserId = userId;   
        TaxAmount = 1;
      
    }

    public string Idorder { get; set; } = null!;

    public double NetPrice { get; set; }

    public DateTime OrderDate { get; set; }

    public string OrderStatus { get; set; } = null!;

    public DateTime StatusDate { get; set; }

    public string DeliveryAdress { get; set; } = null!;

    public string InvoiceAdress { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string PackageId { get; set; } = null!;

    public int TaxAmount { get; set; }

    public double TotalAmount { get; set; }

    public virtual ICollection<Notification> Notifications { get; } = new List<Notification>();

    public virtual ICollection<Orderitem> Orderitems { get; } = new List<Orderitem>();

    public virtual Package Package { get; set; } = null!;
}
