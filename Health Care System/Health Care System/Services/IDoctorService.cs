using Health_Care_System.DoctorDTOs;
using Health_Care_System.Entities;

namespace Health_Care_System.Services
{
    public interface IDoctorService
    {
        Task<IEnumerable<Doctor>> GetAllAsync();
        Task<Doctor?> GetByIdAsync(int id);
        Task<Doctor> CreateAsync(CreateDoctorRequest request);
        Task<bool> UpdateAsync(UpdateDoctorRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
