using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class Notification
{
    public Notification(string message)
    {
        Idnotification = Guid.NewGuid().ToString().Substring(0, 16);
        Message = message;
        IsSeen = 0;
        Duration = 1;
        NotificationDate = DateTime.Now;
    }

    public string Idnotification { get; set; } = null!;

    public string Message { get; set; } = null!;

    public short IsSeen { get; set; }

    public int? Duration { get; set; }
    public DateTime NotificationDate { get; set; }

    public string? OrderId { get; set; }

    public virtual Order? Order { get; set; }
}
