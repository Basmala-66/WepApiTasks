using Health_Care_System.AppointmentDTOs;
using Health_Care_System.Data;
using Health_Care_System.DoctorDTOs;
using Health_Care_System.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Health_Care_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DoctorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDoctor()
        {
            var doctors = await _context.Doctors
                .Include(x=>x.Appointments)
                .Select(doctor => new GetDoctorResponse
                {
                    Name = doctor.Name,
                    Specialization = doctor.Specialization,

                    Appointments = doctor.Appointments.Select(a =>
                        new GetAppointmentResponse
                        {
                            PatientId = a.PatientId,
                            DoctorId = a.DoctorId,
                            AppointmentDate = a.AppointmentDate
                        }).ToList()
                })
                .ToListAsync();


            return Ok(doctors);
        }


        [HttpGet("id")]
        public async Task<IActionResult> getDoctorById(int id)
        {
            var doctor = await _context.Doctors
                .Include(x => x.Appointments)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (doctor is null)
                return NotFound("not found");


            var response = new GetDoctorResponse
            {
                Name = doctor.Name,
                Specialization = doctor.Specialization,

                Appointments = doctor.Appointments.Select(a =>
                    new GetAppointmentResponse
                    {
                        PatientId = a.PatientId,
                        DoctorId= a.DoctorId,
                        AppointmentDate = a.AppointmentDate
                    }).ToList()
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> createDoctor(CreateDoctorRequest request)
        {
            var doctor = new Doctor()
            {
                Name = request.Name,
                Specialization= request.Specialization,

            };

            _context.Doctors.Add(doctor);
            _context.SaveChanges();

            return Ok(doctor);

        }


        [HttpPut]
        public async Task<IActionResult> updateDoctor(UpdateDoctorRequest request)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (doctor is null)
                return NotFound("not found");


           doctor.Specialization = request.Specialization;

            _context.Doctors.Update(doctor);
            _context.SaveChanges();

            return Ok(doctor);
        }


        [HttpDelete]
        public async Task<IActionResult> delete(int id)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(x => x.Id == id);
            if (doctor is null)
                return NotFound("not found");

            _context.Doctors.Remove(doctor);
            _context.SaveChanges();

            return Ok(doctor);
        }
    }
}
