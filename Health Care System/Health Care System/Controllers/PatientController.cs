using AutoMapper;
using Health_Care_System.Data;
using Health_Care_System.Entities;
using Health_Care_System.PatientDTOs;
using Health_Care_System.result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Health_Care_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<PatientController> _logger;
        public PatientController(AppDbContext context, IMapper mapper, ILogger<PatientController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllPatient(int pageNumber, int PageSize, string? searchTerm)
        {

            //var patients = await _context.Patients
            //    .Include(x => x.Appointments)
            //.Select(p => new GetDoctorResponse
            //{
            //    Name = p.Name,
            //    Specialization = p.Specialization,

            //    Appointments = p.Appointments.Select(a =>
            //        new GetAppointmentResponse
            //        {
            //            PatientId = a.PatientId,
            //            DoctorId = a.DoctorId,
            //            AppointmentDate = a.AppointmentDate
            //        }).ToList()
            //})
            // .ToListAsync();

            List<Patient> patients;
            var totalCount = 0;

            if (searchTerm is not null)
            {
                patients = await _context.Patients
                .Include(x => x.Appointments)
                .Where(p => p.Name.Contains(searchTerm))
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

                totalCount = await _context.Patients
                    .CountAsync(p => p.Name.Contains(searchTerm));
            }

            else
            {
                patients = await _context.Patients
                    .Include(x => x.Appointments)
                    .Skip((pageNumber - 1) * PageSize)
                    .Take(PageSize)
                    .ToListAsync();

                totalCount = await _context.Patients.CountAsync();
            }


                var response = _mapper.Map<List<GetPatientResponse>>(patients);

                var paginatedList = new PaginatedList<GetPatientResponse>
                {
                    PageSize = PageSize,
                    PageNumber = pageNumber,
                    TotalRecords = totalCount,
                    TotalPage = (int)Math.Ceiling(
                    totalCount / ((double)PageSize)),
                    Data = response
                };


            var result = GenericResult<PaginatedList<GetPatientResponse>>
             .Success(paginatedList);

            return Ok(result);
            }


            [HttpGet]
            [Route("GetPatientById/{id:int}")]
            public async Task<IActionResult> getPatientById(int id)
            {
                var patient = await _context.Patients
                    .Include(x => x.Appointments)
                    .ThenInclude(d=>d.Doctor)
                    .FirstOrDefaultAsync(x => x.Id == id);

            if (patient is null)
            {
                _logger.LogWarning($"Patient with ID: {id} is not found");
                return NotFound($"Patient with ID: {id} is not found");
            }


                //var response = new GetPatientResponse
                //{
                //    Name = patient.Name,
                //    BirthDate = patient.BirthDate,

                //    Appointments = patient.Appointments.Select(a =>
                //       new GetAppointmentResponse
                //       {
                //           PatientId = a.PatientId,
                //           DoctorId = a.DoctorId,
                //           AppointmentDate = a.AppointmentDate
                //       }).ToList()
                //};


                var response = _mapper.Map<GetPatientResponse>(patient);

                return Ok(response);
            }

            [HttpPost]
            public async Task<IActionResult> createPatient(CreatePatientRequest request)
            {
                //var patient = new Patient()
                //{
                //    Name = request.Name,
                //   BirthDate= request.BirthDate,

                //};



                var patient = _mapper.Map<Patient>(request);

                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();

                return Ok(patient);

            }


            [HttpPut]
            public async Task<IActionResult> updatePatient(UpdatePatientRequest request)
            {
                var patient = await _context.Patients.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (patient is null)
                    return NotFound("not found");


                //patient.Name = request.Name;

                _mapper.Map(request, patient);


                _context.Patients.Update(patient);
                await _context.SaveChangesAsync();

                return Ok(patient);
            }


            [HttpDelete]
            public async Task<IActionResult> delete(int id)
            {
                var patient = await _context.Patients.FirstOrDefaultAsync(x => x.Id == id);
                if (patient is null)
                    return NotFound("not found");

                //_context.Patients.Remove(patient);
                patient.IsDeleted = true;
                await _context.SaveChangesAsync();

                return Ok("Patient Deleted Successfully!");
            }
        }
    }

