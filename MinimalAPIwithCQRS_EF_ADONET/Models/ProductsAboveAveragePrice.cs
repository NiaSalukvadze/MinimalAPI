using System;
using System.Collections.Generic;

namespace MinimalAPIwithCQRS_EF_ADONET.Models;

public partial class ProductsAboveAveragePrice
{
    public string ProductName { get; set; } = null!;

    public decimal? UnitPrice { get; set; }
}
