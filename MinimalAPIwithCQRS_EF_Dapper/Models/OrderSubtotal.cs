﻿using System;
using System.Collections.Generic;

namespace MinimalAPIwithCQRS_EF_Dapper.Models;

public partial class OrderSubtotal
{
    public int OrderId { get; set; }

    public decimal? Subtotal { get; set; }
}
