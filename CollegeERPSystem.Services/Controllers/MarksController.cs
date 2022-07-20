using CollegeERPSystem.Services.Domain;
using CollegeERPSystem.Services.Domain.Models;
using CollegeERPSystem.Services.Domain.Services;
using CollegeERPSystem.Services.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CollegeERPSystem.Services.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class MarksController : ControllerBase
    {
        private readonly MarksService _service;
        public MarksController(MarksService Service)
        {
            _service = Service;
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<Marks>>> GetAllAsync([FromQuery] PaginationModel pagination)
        {
            return Ok(await _service.GetAllAsync(pagination).ConfigureAwait(false));
        }

        [HttpGet("GetById/{id:length(24)}")]
        [ActionName(nameof(GetByIdAsync))]
        public virtual async Task<ActionResult<Marks>> GetByIdAsync(string? id)
        {
            var entity = await _service.GetByIdAsync(id!);

            return entity != null ? Ok(entity) : NotFound();
        }

        [HttpPost]
        public virtual async Task<ActionResult<Marks>> CreateAsync(MarksDTO classDTO)
        {
            var entity = await _service.CreateAsync(classDTO).ConfigureAwait(false);

            return entity.IsSuccess == true ?
             CreatedAtAction(nameof(GetByIdAsync), new { id = ((MarksDTO)entity.Content!).Id }, entity)
             : StatusCode(400, entity);
        }

        [HttpPut]
        public virtual async Task<ActionResult<Marks>> UpdateAsync(MarksDTO classDTO)
        {

            return Ok(await _service.UpdateAsync(classDTO).ConfigureAwait(false));
        }

        [HttpDelete]
        public virtual async Task<ActionResult<Marks>> DeleteAsync(string id)
        {
            return Ok(await _service.DeleteAsync(id).ConfigureAwait(false));
        }
    }
}

