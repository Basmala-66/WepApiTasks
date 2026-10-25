using AutoMapper;
using Health_Care_System.DoctorDTOs;
using Health_Care_System.Services;
using Microsoft.AspNetCore.Mvc;

namespace Health_Care_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<DoctorController> _logger;

        public DoctorController(
            IDoctorService service,
            IMapper mapper,
            ILogger<DoctorController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDoctor()
        {
            var doctors = await _service.GetAllAsync();

            var response = _mapper.Map<List<GetDoctorResponse>>(doctors);

            return Ok(response);
        }

        [HttpGet]
        [Route("GetDoctorById/{id:int}")]
        public async Task<IActionResult> getDoctorById(int id)
        {
            var doctor = await _service.GetByIdAsync(id);

            if (doctor is null)
            {
                _logger.LogWarning(
                    "Doctor with ID {Id} is not found", id);

                return NotFound($"Doctor with ID {id} is not found");
            }

            var response = _mapper.Map<GetDoctorResponse>(doctor);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> createDoctor(
            CreateDoctorRequest request)
        {
            var doctor = await _service.CreateAsync(request);

            return Ok(doctor);
        }

        [HttpPut]
        public async Task<IActionResult> updateDoctor(
            UpdateDoctorRequest request)
        {
            var result = await _service.UpdateAsync(request);

            if (!result)
            {
                _logger.LogWarning(
                    "Doctor with ID {Id} is not found", request.Id);

                return NotFound(
                    $"Doctor with ID {request.Id} is not found");
            }

            return Ok("Doctor Updated Successfully!");
        }

        [HttpDelete]
        public async Task<IActionResult> delete(int id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result)
            {
                _logger.LogWarning(
                    "Doctor with ID {Id} is not found", id);

                return NotFound($"Doctor with ID {id} is not found");
            }

            return Ok("Doctor Deleted Successfully!");
        }
    }
}
