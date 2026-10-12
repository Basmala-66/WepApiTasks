using System;
using System.Collections.Generic;

namespace E_commerce_System.Entities;

public partial class Order
{
    public int Id { get; set; }

    public DateTime OrderDate { get; set; }

    public int CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
