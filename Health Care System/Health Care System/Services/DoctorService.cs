using AutoMapper;
using Health_Care_System.DoctorDTOs;
using Health_Care_System.Entities;
using Health_Care_System.Repositories;

namespace Health_Care_System.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IGenericRepository<Doctor> _repository;
        private readonly IMapper _mapper;

        public DoctorService(
            IGenericRepository<Doctor> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Doctor?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Doctor> CreateAsync(CreateDoctorRequest request)
        {
            var doctor = _mapper.Map<Doctor>(request);
            return await _repository.CreateAsync(doctor);
        }

        public async Task<bool> UpdateAsync(UpdateDoctorRequest request)
        {
            var doctor = await _repository.GetByIdAsync(request.Id);

            if (doctor is null)
                return false;

            _mapper.Map(request, doctor);

            return await _repository.UpdateAsync(doctor);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
