using System;
using System.Collections.Generic;

namespace Health_Care_System.Entities;

public partial class Appointment
{
    public int PatientId { get; set; }

    public int DoctorId { get; set; }

    public DateTime AppointmentDate { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;

    public bool IsDeleted { get; set; }

}
