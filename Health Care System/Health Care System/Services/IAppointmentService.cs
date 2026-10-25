using Health_Care_System.AppointmentDTOs;
using Health_Care_System.Entities;

namespace Health_Care_System.Services
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAllAsync();

        Task<Appointment> GetByIdAsync(int patientId, int doctorId);

        Task<Appointment> CreateAsync(CreateAppointmentRequest request);

        Task<bool> UpdateAsync(UpdateAppointmentRequest request);

        Task<bool> DeleteAsync(int patientId, int doctorId);
    }

}
