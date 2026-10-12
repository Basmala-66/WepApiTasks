using System;
using System.Collections.Generic;

namespace Library_System.Entities;

public partial class Borrower
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime MembershipDate { get; set; }

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
