using System;
using System.Collections.Generic;

namespace Health_Care_System.Entities;

public partial class Patient
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
