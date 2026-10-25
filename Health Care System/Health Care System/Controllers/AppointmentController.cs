using AutoMapper;
using Health_Care_System.AppointmentDTOs;

using Health_Care_System.Services;
using Microsoft.AspNetCore.Mvc;

namespace Health_Care_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<AppointmentController> _logger;

        public AppointmentController(
            IAppointmentService service,
            IMapper mapper,
            ILogger<AppointmentController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAppointment()
        {
            var appointments = await _service.GetAllAsync();

            var response = _mapper.Map<List<GetAppointmentResponse>>(appointments);

            return Ok(response);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetAppointmentById(
            int patientId,
            int doctorId)
        {
            var appointment = await _service.GetByIdAsync(
                patientId,
                doctorId);

            if (appointment is null)
            {
                _logger.LogWarning(
                    "Appointment for Patient {PatientId} and Doctor {DoctorId} was not found",
                    patientId,
                    doctorId);

                return NotFound("Appointment is not found");
            }

            var response = _mapper.Map<GetAppointmentResponse>(appointment);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment(
            CreateAppointmentRequest request)
        {
            var appointment = await _service.CreateAsync(request);

            return Ok(appointment);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAppointment(
            UpdateAppointmentRequest request)
        {
            var updated = await _service.UpdateAsync(request);

            if (!updated)
            {
                _logger.LogWarning(
                    "Appointment for Patient {PatientId} and Doctor {DoctorId} was not found",
                    request.PatientId,
                    request.DoctorId);

                return NotFound("Appointment is not found");
            }

            var appointment = await _service.GetByIdAsync(
                request.PatientId,
                request.DoctorId);

            return Ok(appointment);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(
            int patientId,
            int doctorId)
        {
            var deleted = await _service.DeleteAsync(
                patientId,
                doctorId);

            if (!deleted)
            {
                _logger.LogWarning(
                    "Appointment for Patient {PatientId} and Doctor {DoctorId} was not found",
                    patientId,
                    doctorId);

                return NotFound("Appointment is not found");
            }

            return Ok("Appointment Deleted Successfully!");
        }
    }
}
