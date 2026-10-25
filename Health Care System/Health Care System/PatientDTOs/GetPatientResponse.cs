using Health_Care_System.AppointmentDTOs;
using Health_Care_System.Entities;

namespace Health_Care_System.PatientDTOs
{
    public class GetPatientResponse
    {

        public string Name { get; set; } = null!;

        public DateTime BirthDate { get; set; }
        public ICollection<GetAppointmentResponse> Appointments { get; set; }= new List<GetAppointmentResponse>();
    }

}
