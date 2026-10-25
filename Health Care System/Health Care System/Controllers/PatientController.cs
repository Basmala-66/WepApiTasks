using AutoMapper;

using Health_Care_System.PatientDTOs;
using Health_Care_System.result;
using Health_Care_System.Services;
using Microsoft.AspNetCore.Mvc;


namespace Health_Care_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        //private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<PatientController> _logger;
        private readonly IPatientService _service;
        public PatientController( IMapper mapper, ILogger<PatientController> logger,IPatientService service)
        {
            _mapper = mapper;
            _logger = logger;
            _service = service;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllPatient()
        {

            var patients =await _service.GetAllAsync();
            var response = _mapper.Map<List<GetPatientResponse>>(patients);


            var result = GenericResult<List<GetPatientResponse>>.Success(response);

            return Ok(result);

            #region old
            //List<Patient> patients;
            //var totalCount = 0;

            //if (searchTerm is not null)
            //{
            //    patients = await _context.Patients
            //    .Include(x => x.Appointments)
            //    .Where(p => p.Name.Contains(searchTerm))
            //    .Skip((pageNumber - 1) * PageSize)
            //    .Take(PageSize)
            //    .ToListAsync();

            //    totalCount = await _context.Patients
            //        .CountAsync(p => p.Name.Contains(searchTerm));
            //}

            //else
            //{
            //    patients = await _context.Patients
            //        .Include(x => x.Appointments)
            //        .Skip((pageNumber - 1) * PageSize)
            //        .Take(PageSize)
            //        .ToListAsync();

            //    totalCount = await _context.Patients.CountAsync();
            //}


            //    var response = _mapper.Map<List<GetPatientResponse>>(patients);

            //    var paginatedList = new PaginatedList<GetPatientResponse>
            //    {
            //        PageSize = PageSize,
            //        PageNumber = pageNumber,
            //        TotalRecords = totalCount,
            //        TotalPage = (int)Math.Ceiling(
            //        totalCount / ((double)PageSize)),
            //        Data = response
            //    };


            //var result = GenericResult<PaginatedList<GetPatientResponse>>
            // .Success(paginatedList);

            //return Ok(result);

            #endregion old
        }


        [HttpGet]
            [Route("GetPatientById/{id:int}")]
            public async Task<IActionResult> getPatientById(int id)
            {

                var result = await _service.GetByIdAsync(id);

                return result.IsSuccess? Ok(result) : NotFound(result);
            #region old
            //var patient = await _context.Patients
            //        .Include(x => x.Appointments)
            //        .ThenInclude(d=>d.Doctor)
            //        .FirstOrDefaultAsync(x => x.Id == id);

            //if (patient is null)
            //{
            //    _logger.LogWarning($"Patient with ID: {id} is not found");
            //    return NotFound($"Patient with ID: {id} is not found");
            //}


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


                //var response = _mapper.Map<GetPatientResponse>(patient);

                //return Ok(response);
            #endregion
        }

        [HttpPost]
            public async Task<IActionResult> createPatient(CreatePatientRequest request)
            {


            var patient = await _service.CreateAsync(request);
            return Ok(patient);


            #region old
            //var patient = new Patient()
            //{
            //    Name = request.Name,
            //   BirthDate= request.BirthDate,

            //};



            //var patient = _mapper.Map<Patient>(request);

            //    _context.Patients.Add(patient);
            //    await _context.SaveChangesAsync();

               // return Ok(patient);
            #endregion

        }


        [HttpPut]
            public async Task<IActionResult> updatePatient(UpdatePatientRequest request)
            {
                var isUpdated = await _service.UpdateAsync(request);

            if (!isUpdated)
                return NotFound("patient not found");

            return Ok("Patient Updated Successfully!");

            #region old
            //var patient = await _context.Patients.FirstOrDefaultAsync(x => x.Id == request.Id);

            //    if (patient is null)
            //        return NotFound("not found");


            //    //patient.Name = request.Name;

            //    _mapper.Map(request, patient);


            //    _context.Patients.Update(patient);
            //    await _context.SaveChangesAsync();

            //    return Ok(patient);
            #endregion
            }


        [HttpDelete]
            public async Task<IActionResult> delete(int id)
            {
                
            var isDeleted = await _service.DeleteAsync(id);

            if(!isDeleted)
                return NotFound("Patient not found.");


            return Ok("Patient Deleted Successfully!");


            #region old
            //var patient = await _context.Patients.FirstOrDefaultAsync(x => x.Id == id);
            //    if (patient is null)
            //        return NotFound("not found");

            //    //_context.Patients.Remove(patient);
            //    patient.IsDeleted = true;
            //    await _context.SaveChangesAsync();

            //    return Ok("Patient Deleted Successfully!");

            #endregion
        }
    }
    }

