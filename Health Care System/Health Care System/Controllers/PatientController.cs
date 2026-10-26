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

           
        }



        [HttpGet]
        [Route("GetPatientById/{id:int}")]
        [ProducesResponseType(
            typeof(GenericResult<GetPatientResponse>),
            StatusCodes.Status200OK)]

        [ProducesResponseType(
            typeof(GenericResult<GetPatientResponse>),
            StatusCodes.Status404NotFound)]

        [ProducesResponseType(
            typeof(ProblemDetails),
            StatusCodes.Status500InternalServerError)]

            public async Task<IActionResult> getPatientById([FromRoute]int id)
            {

                var result = await _service.GetByIdAsync(id);

                return result.IsSuccess? Ok(result) : NotFound(result);
            
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

