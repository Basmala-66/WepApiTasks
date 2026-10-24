using AutoMapper;
using Health_Care_System.AppointmentDTOs;
using Health_Care_System.Data;
using Health_Care_System.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Health_Care_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<AppointmentController> _logger;

        public AppointmentController(AppDbContext context,IMapper mapper ,ILogger<AppointmentController> logger)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAppointment()
        {
            var appointments = await _context.Appointments
                .Include(a => a.Doctor)
                .ToListAsync();


            var response = _mapper.Map<List<GetAppointmentResponse>>(appointments);

            return Ok(response);
        }


        [HttpGet("id")]
        public async Task<IActionResult> getAppointmentById(int patientId,int doctorId)
        {
            var appointment = await _context.Appointments
             .Include(a => a.Doctor)
             .FirstOrDefaultAsync(x =>x.PatientId == patientId && x.DoctorId == doctorId);


            if (appointment is null)
            {
                _logger.LogWarning("Appointment for Patient {PatientId} and Doctor {DoctorId} was not found",patientId,doctorId);

                return NotFound($" Appointment is not found");
            }


            var response = _mapper.Map<GetAppointmentResponse>(appointment);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> createAppointment(CreateAppointmentRequest request)
        {
            var appointment = _mapper.Map<Appointment>(request);

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return Ok(appointment);

        }


        [HttpPut]
        public async Task<IActionResult> updateAppointment(UpdateAppointmentRequest request)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(x => x.PatientId == request.PatientId && x.DoctorId == request.DoctorId);

            if (appointment is null)
            {
                _logger.LogWarning("Appointment for Patient {PatientId} and Doctor {DoctorId} was not found", request.PatientId, request.DoctorId);

                return NotFound($" Appointment is not found");
            }

                
            _mapper.Map(request, appointment);

            await _context.SaveChangesAsync();

            return Ok(appointment);
        }


        [HttpDelete]
        public async Task<IActionResult> delete(int patientId, int doctorId)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(x => x.PatientId == patientId && x.DoctorId == doctorId);


            if (appointment is null)
            {
                _logger.LogWarning("Appointment for Patient {PatientId} and Doctor {DoctorId} was not found",patientId,doctorId);
                return NotFound($" Appointment is not found");
            }

            appointment.IsDeleted = true;
            await _context.SaveChangesAsync();

            return Ok("Appointment Deleted Successfully!");
        }
    }
}
