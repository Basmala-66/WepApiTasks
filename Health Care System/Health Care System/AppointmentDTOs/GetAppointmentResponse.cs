namespace Health_Care_System.AppointmentDTOs
{
    public class GetAppointmentResponse
    {
        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        public string DoctortName { get; set; }
        public DateTime AppointmentDate { get; set; }
    }


    public class UpdateAppointmentRequest
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        
        public DateTime AppointmentDate { get; set; }
    }
}
