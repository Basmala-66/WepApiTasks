using System;
using System.Collections.Generic;

namespace Library_System.Entities;

public partial class Loan
{
    public int BookId { get; set; }

    public int BorrowerId { get; set; }

    public DateTime LoanDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Borrower Borrower { get; set; } = null!;
}
