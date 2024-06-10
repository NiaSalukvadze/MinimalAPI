using System;
using System.Collections.Generic;

namespace MinimalAPIwithCQRS_EF_ADONET.Models;

public partial class SummaryOfSalesByQuarter
{
    public DateTime? ShippedDate { get; set; }

    public int OrderId { get; set; }

    public decimal? Subtotal { get; set; }
}
