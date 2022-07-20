using CollegeERPSystem.Services.Domain;
using CollegeERPSystem.Services.Domain.Services;
using CollegeERPSystem.Services.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CollegeERPSystem.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController:ControllerBase
    {
        private readonly StudentService _service;
        public StudentsController(StudentService Service)
        {
            _service = Service;
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<StudentDTO>>> GetAllAsync([FromQuery] PaginationModel _pagination)
        {
            return Ok(await _service.GetAllAsync(_pagination).ConfigureAwait(false));
        }

        [HttpGet("GetById/{id:int}")]
        [ActionName(nameof(GetByIdAsync))]
        public virtual async Task<ActionResult<StudentDTO>> GetByIdAsync(int? id)
        {
            var entity = await _service.GetByIdAsync(id!.Value);

            return entity != null ? Ok(entity) : NotFound();
        }

        [HttpPost]
        public virtual async Task<ActionResult<StudentDTO>> CreateAsync(StudentDTO school)
        {
            var entity = await _service.CreateAsync(school).ConfigureAwait(false);

            return entity.IsSuccess == true ?
             CreatedAtAction(nameof(GetByIdAsync), new { id = ((StudentDTO)entity.Content!).Id }, entity)
             : StatusCode(400, entity);
        }

        [HttpPut]
        public virtual async Task<ActionResult<StudentDTO>> UpdateAsync(StudentDTO StudentDTO)
        {

            return Ok(await _service.UpdateAsync(StudentDTO).ConfigureAwait(false));
        }

        [HttpDelete]
        public virtual async Task<ActionResult<StudentDTO>> DeleteAsync(int id)
        {
            return Ok(await _service.DeleteAsync(id).ConfigureAwait(false));
        }
    }
}
