using System;
using System.Collections.Generic;

namespace DocEase.Persistence.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public string? UserType { get; set; }

    public int? UserId { get; set; }

    public string? Message { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? IsRead { get; set; }
}
