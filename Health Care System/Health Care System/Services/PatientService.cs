using AutoMapper;
using Health_Care_System.Entities;
using Health_Care_System.PatientDTOs;
using Health_Care_System.Repositories;
using Health_Care_System.result;

namespace Health_Care_System.Services
{
    public class PatientService:IPatientService
    {
        private readonly IGenericRepository<Patient> _repository;
        private readonly IMapper _mapper;

        public PatientService(  IGenericRepository<Patient> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Patient> CreateAsync(CreatePatientRequest request)
        {
           var patient = _mapper.Map<Patient>(request);

            return await _repository.CreateAsync(patient);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
           return await _repository.GetAllAsync();
        }

        public async Task<GenericResult<GetPatientResponse>> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity is null)
                return GenericResult<GetPatientResponse>.Failure($"Patient with Id = {id} is not found.");
            
            var response = _mapper.Map<GetPatientResponse>(entity);

            return GenericResult<GetPatientResponse>.Success(response);
        
        }

        public async Task<bool> UpdateAsync(UpdatePatientRequest request)
        {
            var patient = await _repository.GetByIdAsync(request.Id);

            if (patient is null)
                return false;

            _mapper.Map(request,patient);
            return await _repository.UpdateAsync(patient);
        }
    }
}
