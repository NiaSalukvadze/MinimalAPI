﻿using System;
using System.Collections.Generic;

namespace MinimalAPIwithCQRS_Dapper_EF.Models;

public partial class CategorySalesFor1997
{
    public string CategoryName { get; set; } = null!;

    public decimal? CategorySales { get; set; }
}
