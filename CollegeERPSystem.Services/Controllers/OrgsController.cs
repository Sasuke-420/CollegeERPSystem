using CollegeERPSystem.Services.Domain;
using CollegeERPSystem.Services.Domain.Services;
using CollegeERPSystem.Services.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CollegeERPSystem.Services.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class OrgsController: ControllerBase
    {
        private readonly OrgService _service;
        public OrgsController(OrgService Service)
        {
            _service = Service;
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<OrgDTO>>> GetAllAsync([FromQuery] PaginationModel pagination)
        {
            return Ok(await _service.GetAllAsync(pagination).ConfigureAwait(false));
        }

        [HttpGet("GetById/{id:int}")]
        [ActionName(nameof(GetByIdAsync))]
        public virtual async Task<ActionResult<OrgDTO>> GetByIdAsync(int? id)
        {
            var entity = await _service.GetByIdAsync(id!.Value);

            return entity != null ? Ok(entity) : NotFound();
        }

        [HttpPost]
        public virtual async Task<ActionResult<OrgDTO>> CreateAsync(OrgDTO Programme)
        {
            var entity = await _service.CreateAsync(Programme).ConfigureAwait(false);

            return entity.IsSuccess == true ?
             CreatedAtAction(nameof(GetByIdAsync), new { id = ((OrgDTO)entity.Content!).Id }, entity)
             : StatusCode(400, entity);
        }

        [HttpPut]
        public virtual async Task<ActionResult<OrgDTO>> UpdateAsync(OrgDTO OrgDTO)
        {

            return Ok(await _service.UpdateAsync(OrgDTO).ConfigureAwait(false));
        }

        [HttpDelete]
        public virtual async Task<ActionResult<OrgDTO>> DeleteAsync(int id)
        {
            return Ok(await _service.DeleteAsync(id).ConfigureAwait(false));
        }
    }
}