using System;
using System.Collections.Generic;

namespace MinimalAPIwithCQRS_ADONET_EF.Models;

public partial class Region
{
    public int RegionId { get; set; }

    public string RegionDescription { get; set; } = null!;

    public virtual ICollection<Territory> Territories { get; set; } = new List<Territory>();
}
