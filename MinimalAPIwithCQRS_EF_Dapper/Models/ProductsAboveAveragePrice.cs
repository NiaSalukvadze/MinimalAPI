using System;
using System.Collections.Generic;

namespace MinimalAPIwithCQRS_EF_Dapper.Models;

public partial class ProductsAboveAveragePrice
{
    public string ProductName { get; set; } = null!;

    public decimal? UnitPrice { get; set; }
}
