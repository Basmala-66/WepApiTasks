using Health_Care_System.Entities;

namespace Health_Care_System.Repositories
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAllAsync();
        Task<Appointment> GetByIdAsync(
           int patientId, int doctorId);

        Task<Appointment> CreateAsync(Appointment appointment);

        Task<bool> UpdateAsync(Appointment appointment);

        Task<bool> DeleteAsync(int patientId, int doctorId);

    }
}
