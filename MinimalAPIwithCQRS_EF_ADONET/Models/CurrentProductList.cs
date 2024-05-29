using System;
using System.Collections.Generic;

namespace MinimalAPIwithCQRS_EF_ADONET.Models;

public partial class CurrentProductList
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;
}
