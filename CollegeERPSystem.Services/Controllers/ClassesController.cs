using CollegeERPSystem.Services.Domain;
using CollegeERPSystem.Services.Domain.Models;
using CollegeERPSystem.Services.Domain.Services;
using CollegeERPSystem.Services.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CollegeERPSystem.Services.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly ClassService _service;
        public ClassController(ClassService Service)
        {
            _service = Service;
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<Class>>> GetAllAsync([FromQuery] PaginationModel pagination)
        {
            return Ok(await _service.GetAllAsync(pagination).ConfigureAwait(false));
        }

        [HttpGet("GetById/{id:int}")]
        [ActionName(nameof(GetByIdAsync))]
        public virtual async Task<ActionResult<Class>> GetByIdAsync(int? id)
        {
            var entity = await _service.GetByIdAsync(id!.Value);

            return entity != null ? Ok(entity) : NotFound();
        }

        [HttpPost]
        public virtual async Task<ActionResult<Class>> CreateAsync(ClassDTO classDTO)
        {
            var entity = await _service.CreateAsync(classDTO).ConfigureAwait(false);

            return entity.IsSuccess == true ?
             CreatedAtAction(nameof(GetByIdAsync), new { id = ((Class)entity.Content!).Id }, entity)
             : StatusCode(400, entity);
        }

        [HttpPut]
        public virtual async Task<ActionResult<Class>> UpdateAsync(ClassDTO classDTO)
        {

            return Ok(await _service.UpdateAsync(classDTO).ConfigureAwait(false));
        }

        [HttpDelete]
        public virtual async Task<ActionResult<Class>> DeleteAsync(int id)
        {
            return Ok(await _service.DeleteAsync(id).ConfigureAwait(false));
        }
    }
}