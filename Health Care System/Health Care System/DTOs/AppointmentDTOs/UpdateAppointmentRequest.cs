namespace Health_Care_System.DTOs.AppointmentDTOs
{
    public class UpdateAppointmentRequest
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        
        public DateTime AppointmentDate { get; set; }
    }
}
