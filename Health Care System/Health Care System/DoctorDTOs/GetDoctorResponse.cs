using Health_Care_System.AppointmentDTOs;
using Health_Care_System.Entities;

namespace Health_Care_System.DoctorDTOs
{
    public class GetDoctorResponse
    {

        public string Name { get; set; } = null!;

        public string Specialization { get; set; } = null!;
        public ICollection<GetAppointmentResponse> Appointments { get; set; }= new List<GetAppointmentResponse>();

    }
}
