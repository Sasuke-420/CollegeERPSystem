using CollegeERPSystem.Services.Domain.Services;
using CollegeERPSystem.Services.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CollegeERPSystem.Services.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ProgrammesController:ControllerBase
    {
        private readonly ProgrammeService _service;
        public ProgrammesController(ProgrammeService Service)
        {
            _service = Service;
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<ProgrammeDTO>>> GetAllAsync()
        {
            return Ok(await _service.GetAllAsync().ConfigureAwait(false));
        }

        [HttpGet("GetById/{id:int}")]
        [ActionName(nameof(GetByIdAsync))]
        public virtual async Task<ActionResult<ProgrammeDTO>> GetByIdAsync(int? id)
        {
            var entity = await _service.GetByIdAsync(id!.Value);

            return entity != null ? Ok(entity) : NotFound();
        }

        [HttpPost]
        public virtual async Task<ActionResult<ProgrammeDTO>> CreateAsync(ProgrammeDTO Programme)
        {
            var entity = await _service.CreateAsync(Programme).ConfigureAwait(false);

            return entity.IsSuccess == true ?
             CreatedAtAction(nameof(GetByIdAsync), new { id = ((ProgrammeDTO)entity.Content!).Id }, entity)
             : StatusCode(400, entity);
        }

        [HttpPut]
        public virtual async Task<ActionResult<ProgrammeDTO>> UpdateAsync(ProgrammeDTO ProgrammeDTO)
        {

            return Ok(await _service.UpdateAsync(ProgrammeDTO).ConfigureAwait(false));
        }

        [HttpDelete]
        public virtual async Task<ActionResult<ProgrammeDTO>> DeleteAsync(int id)
        {
            return Ok(await _service.DeleteAsync(id).ConfigureAwait(false));
        }
    }
}