using Health_Care_System.AppointmentDTOs;
using Health_Care_System.Data;
using Health_Care_System.DoctorDTOs;
using Health_Care_System.Entities;
using Health_Care_System.PatientDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Health_Care_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PatientController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPatient()
        {
            var patients = await _context.Doctors
                .Include(x => x.Appointments)
                .Select(p => new GetDoctorResponse
                {
                    Name = p.Name,
                    Specialization = p.Specialization,

                    Appointments = p.Appointments.Select(a =>
                        new GetAppointmentResponse
                        {
                            PatientId = a.PatientId,
                            DoctorId = a.DoctorId,
                            AppointmentDate = a.AppointmentDate
                        }).ToList()
                })
                .ToListAsync();




            return Ok(patients);
        }


        [HttpGet("id")]
        public async Task<IActionResult> getPatientById(int id)
        {
            var patient = await _context.Patients
                .Include(x=>x.Appointments)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (patient is null)
                return NotFound("not found");


            var response = new GetPatientResponse
            {
                Name = patient.Name,
                BirthDate = patient.BirthDate,

                Appointments = patient.Appointments.Select(a =>
                   new GetAppointmentResponse
                   {
                       PatientId = a.PatientId,
                       DoctorId = a.DoctorId,
                       AppointmentDate = a.AppointmentDate
                   }).ToList()
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> createPatient(CreatePatientRequest request)
        {
            var patient = new Patient()
            {
                Name = request.Name,
               BirthDate= request.BirthDate,

            };

            _context.Patients.Add(patient);
            _context.SaveChanges();

            return Ok(patient);

        }


        [HttpPut]
        public async Task<IActionResult> updatePatient(UpdatePatientRequest request)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (patient is null)
                return NotFound("not found");


            patient.Name = request.Name;

            _context.Patients.Update(patient);
            _context.SaveChanges();

            return Ok(patient);
        }


        [HttpDelete]
        public async Task<IActionResult> delete(int id)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(x => x.Id == id);
            if (patient is null)
                return NotFound("not found");

            _context.Patients.Remove(patient);
            _context.SaveChanges();

            return Ok(patient);
        }
    }
}
