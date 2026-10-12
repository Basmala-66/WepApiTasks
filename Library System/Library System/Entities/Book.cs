using System;
using System.Collections.Generic;

namespace Library_System.Entities;

public partial class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Isbn { get; set; } = null!;

    public int AuthorId { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
