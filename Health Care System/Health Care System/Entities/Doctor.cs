using System;
using System.Collections.Generic;

namespace Health_Care_System.Entities;

public partial class Doctor
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Specialization { get; set; } = null!;

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
