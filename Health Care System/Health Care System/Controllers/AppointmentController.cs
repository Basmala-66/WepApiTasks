using Health_Care_System.AppointmentDTOs;
using Health_Care_System.Data;
using Health_Care_System.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Health_Care_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AppointmentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAppointment()
        {
            var appointments = await _context.Appointments.ToListAsync();

            return Ok(appointments);
        }


        [HttpGet("id")]
        public async Task<IActionResult> getAppointmentById(int patientId,int doctorId)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(x => x.PatientId == patientId && x.DoctorId == doctorId);

            if (appointment is null)
                return NotFound("not found");


            var response = new GetAppointmentResponse
            {
              PatientId = appointment.PatientId,
              DoctorId = appointment.DoctorId,
              DoctortName = appointment.Doctor?.Name??string.Empty,
              AppointmentDate = appointment.AppointmentDate,
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> createAppointment(CreateAppointmentRequest request)
        {
            var appointment = new Appointment()
            {
              PatientId= request.PatientId,
              DoctorId= request.DoctorId,
              AppointmentDate= request.AppointmentDate,
            };

            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            return Ok(appointment);

        }


        [HttpPut]
        public async Task<IActionResult> updateAppointment(UpdateAppointmentRequest request)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(x => x.PatientId == request.PatientId && x.DoctorId == request.DoctorId);

            if (appointment is null)
                return NotFound("not found");


            appointment.DoctorId = request.DoctorId;
            appointment.AppointmentDate = request.AppointmentDate;

            _context.Appointments.Update(appointment);
            _context.SaveChanges();

            return Ok(appointment);
        }


        [HttpDelete]
        public async Task<IActionResult> delete(int patientId, int doctorId)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(x => x.PatientId == patientId && x.DoctorId == doctorId);
            if (appointment is null)
                return NotFound("not found");

            _context.Appointments.Remove(appointment);
            _context.SaveChanges();

            return Ok(appointment);
        }
    }
}
