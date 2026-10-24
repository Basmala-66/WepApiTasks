using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly ILogger<DoctorController> _logger;
        public DoctorController(AppDbContext context, IMapper mapper ,ILogger<DoctorController> logger)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDoctor()
        {
            var doctors = await _context.Doctors
                .Include(x=>x.Appointments)
                .ToListAsync();


            var response = _mapper.Map<List<GetDoctorResponse>>(doctors);



            return Ok(response);
        }


        [HttpGet]
        [Route("GetDoctorById/{id:int}")]
        public async Task<IActionResult> getDoctorById(int id)
        {
            var doctor = await _context.Doctors
                .Include(x => x.Appointments)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (doctor is null)
            {
                _logger.LogWarning($"Doctor with ID {id} is not found");
                return NotFound($"Doctor with ID {id} is not found");
            }


            var response = _mapper.Map<GetDoctorResponse>(doctor);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> createDoctor(CreateDoctorRequest request)
        {
            var doctor = _mapper.Map<Doctor>(request);

            _context.Doctors.Add(doctor);
           await _context.SaveChangesAsync();

            return Ok(doctor);

        }


        [HttpPut]
        public async Task<IActionResult> updateDoctor(UpdateDoctorRequest request)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (doctor is null)
            {
                _logger.LogWarning($"Doctor with ID {request.Id} is not found");
                return NotFound($"Doctor with ID {request.Id} is not found");
            }


            _mapper.Map(request, doctor);

            _context.Doctors.Update(doctor);
          await  _context.SaveChangesAsync();

            return Ok(doctor);
        }


        [HttpDelete]
        public async Task<IActionResult> delete(int id)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(x => x.Id == id);
            if (doctor is null)
            {
                _logger.LogWarning($"Doctor with ID {id} is not found");
                return NotFound($"Doctor with ID {id} is not found");
            }

            doctor.IsDeleted = true;
            await _context.SaveChangesAsync();

            return Ok("Doctor Deleted Successfully!");

        }
    }
}
