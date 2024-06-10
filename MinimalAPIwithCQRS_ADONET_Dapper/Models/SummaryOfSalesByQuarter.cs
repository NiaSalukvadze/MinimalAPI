using System;
using System.Collections.Generic;

namespace MinimalAPIwithCQRS_ADONET_Dapper.Models;

public partial class SummaryOfSalesByQuarter
{
    public DateTime? ShippedDate { get; set; }

    public int OrderId { get; set; }

    public decimal? Subtotal { get; set; }
}
