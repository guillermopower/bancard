using System;
using System.Collections.Generic;

namespace Bancard.DAL.Models;

public partial class FpgBancard
{
    public long Id { get; set; }

    public DateTime Date { get; set; }

    public string? Token { get; set; }

    public long ShopProcessId { get; set; }

    public string? Response { get; set; }

    public string? ResponseDetails { get; set; }

    public string? Amount { get; set; }

    public string? Currency { get; set; }

    public string? AuthorizationNumber { get; set; }

    public string? TicketNumber { get; set; }

    public string? ResponseCode { get; set; }

    public string? ResponseDescription { get; set; }

    public string? ExtendedResponseDescription { get; set; }

    public string? SecurityInformation { get; set; }
}
