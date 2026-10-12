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

    public class CreateDoctorRequest
    {
        public string Name { get; set; }
        public string Specialization { get; set; }
    }


    public class UpdateDoctorRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
    }
}
