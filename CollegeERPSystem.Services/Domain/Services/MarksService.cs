using AutoMapper;
using CollegeERPSystem.Services.Domain.Models;
using CollegeERPSystem.Services.Domain.Repositories;
using CollegeERPSystem.Services.DTO;

namespace CollegeERPSystem.Services.Domain.Services
{
    public class MarksService
    {
        private readonly IMapper _mapper;
        private readonly MarksRepository _repository;
        public MarksService(IMapper mapper, MarksRepository Repository)
        {
            _mapper = mapper;
            _repository = Repository;
        }

        public virtual async Task<Response> GetAllAsync(PaginationModel pagination)
        {
            try
            {
                var result = await _repository.GetAllAsync(pagination);
                return new Response(null, true, 200, null,
                    _mapper.Map<IEnumerable<MarksDTO>>(result));
            }
            catch (Exception Ex)
            {
                return new Response(null, false, 500, new List<string>() { Ex.Message });
            }
        }
        public virtual async Task<Response> GetByIdAsync(string id)
        {
            try
            {
                var result = _mapper.Map<MarksDTO>(await _repository.GetByIdAsync(id));
                return new Response(null, true, result != null ? 200 : 404, null, result);
            }
            catch (Exception Ex)
            {
                return new Response(null, false, 500, new List<string>() { Ex.Message });
            }
        }

        public virtual async Task<Response> CreateAsync(MarksDTO marksDTO)
        {
            try
            {
                return new Response(null, true, 201, null, _mapper.Map<MarksDTO>(await _repository.CreateAsync(_mapper.Map<Marks>(marksDTO)).ConfigureAwait(false)));
            }
            catch (Exception Ex)
            {
                return new Response(null, false, 500, new List<string>() { Ex.Message });
            }
        }

        public virtual async Task<Response> DeleteAsync(string id)
        {
            try
            {
                var result = await GetByIdAsync(id).ConfigureAwait(false);
                return new Response(null, true, 202, null, _mapper.Map<MarksDTO>(await _repository.DeleteAsync(_mapper.Map<Marks>((MarksDTO)result.Content!)).ConfigureAwait(false)));
            }
            catch (Exception Ex)
            {
                return new Response(null, false, 500, new List<string>() { Ex.Message });
            }
        }

        public virtual async Task<Response> UpdateAsync(MarksDTO marksDTO)
        {
            try
            {
                return new Response(null, true, 204, null, _mapper.Map<MarksDTO>(await _repository.UpdateAsync(_mapper.Map<Marks>(marksDTO)).ConfigureAwait(false)));
            }
            catch (Exception Ex)
            {
                return new Response(null, false, 500, new List<string>() { Ex.Message });
            }
        }
    }
}
 
