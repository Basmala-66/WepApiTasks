using AutoMapper;
using Health_Care_System.AppointmentDTOs;
using Health_Care_System.Entities;
using Health_Care_System.Repositories;

namespace Health_Care_System.Services
{
    public class AppointmentService: IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public AppointmentService(IAppointmentRepository appointmentRepository,IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task<Appointment> CreateAsync(CreateAppointmentRequest request)
        {
           var appointment = _mapper.Map<Appointment>(request);
            return await _appointmentRepository.CreateAsync(appointment);
        }

        public async Task<bool> DeleteAsync(int patientId, int dovtorId)
        {
            return await _appointmentRepository.DeleteAsync(patientId, dovtorId);
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
           return await _appointmentRepository.GetAllAsync();
        }

        public async Task<Appointment> GetByIdAsync(int patientId, int doctorId)
        {
            return await _appointmentRepository.GetByIdAsync(patientId, doctorId);
        }

        public async Task<bool> UpdateAsync(UpdateAppointmentRequest request)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(request.PatientId, request.DoctorId);

            if (appointment is null) 
                return false;

            _mapper.Map(request,appointment);

            return await _appointmentRepository.UpdateAsync(appointment);
        }
    }

}
