using AutoMapper;
using CollegeERPSystem.Services.Domain.Models;
using CollegeERPSystem.Services.Domain.Services;
using CollegeERPSystem.Services.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CollegeERPSystem.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        private readonly SchoolService _service;
       
        public SchoolsController(SchoolService Service)
        {
            _service = Service;
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<SchoolDTO>>> GetAllAsync()
        {               
            return Ok(await _service.GetAllAsync().ConfigureAwait(false));
        }

        [HttpGet("GetById/{id:int}")]
        [ActionName(nameof(GetByIdAsync))]
        public virtual async Task<ActionResult<SchoolDTO>> GetByIdAsync(int? id)
        {
            var entity = await _service.GetByIdAsync(id!.Value);

            return entity != null ? Ok(entity) : NotFound();
        }

        [HttpPost]
        public virtual async Task<ActionResult<SchoolDTO>> CreateAsync(SchoolDTO school)
        {
            var entity = await _service.CreateAsync(school).ConfigureAwait(false);
            return entity.IsSuccess==true?
             CreatedAtAction(nameof(GetByIdAsync), new { id = ((SchoolDTO)entity.Content!).Id }, entity)
             :StatusCode(400,entity);
        }

        [HttpPut]
        public virtual async Task<ActionResult<SchoolDTO>> UpdateAsync(SchoolDTO schoolDTO)
        {
            return Ok(await _service.UpdateAsync(schoolDTO).ConfigureAwait(false));
        }

        [HttpDelete]
        public virtual async Task<ActionResult<SchoolDTO>> DeleteAsync(int id)
        {
            return Ok(await _service.DeleteAsync(id).ConfigureAwait(false));
        }
    }
}
