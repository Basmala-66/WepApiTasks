using Health_Care_System.Entities;
using Health_Care_System.PatientDTOs;
using Health_Care_System.result;

namespace Health_Care_System.Services
{
    public interface IPatientService
    {
        Task<IEnumerable<Patient>> GetAllAsync();
        Task<GenericResult<GetPatientResponse>> GetByIdAsync(int id);
        Task<Patient> CreateAsync(CreatePatientRequest request);
        Task<bool> UpdateAsync(UpdatePatientRequest request);
        Task<bool> DeleteAsync(int id);
    }

}
