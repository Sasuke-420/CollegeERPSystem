using AutoMapper;
using CollegeERPSystem.Services.Domain.Models;
using CollegeERPSystem.Services.Domain.Repositories;
using CollegeERPSystem.Services.DTO;

namespace CollegeERPSystem.Services.Domain.Services
{
    public class OrgService
    {
        private readonly IMapper _mapper;
        private readonly OrgRepository _repository;
        public OrgService(IMapper mapper, OrgRepository Repository)
        {
            _mapper = mapper;
            _repository = Repository;
        }

        public async Task<Response> GetAllAsync(PaginationModel pagination)
        {
            try
            {
                return new Response(null, true, 200, null,
                    _mapper.Map<IEnumerable<OrgDTO>>(await _repository.GetAllAsync(pagination)));
            }
            catch (Exception Ex)
            {
                return new Response(null, false, 500, new List<string>() { Ex.Message });
            }
        }
        public async Task<Response> GetByIdAsync(int id)
        {
            try
            {
                var result = _mapper.Map<OrgDTO>(await _repository.GetByIdAsync(id));
                return new Response(null, true, result != null ? 200 : 404, null, result);
            }
            catch (Exception Ex)
            {
                return new Response(null, false, 500, new List<string>() { Ex.Message });
            }
        }

        public async Task<Response> CreateAsync(OrgDTO orgDTO)
        {
            try
            {
                return new Response(null, true, 201, null, _mapper.Map<OrgDTO>(await _repository.CreateAsync(_mapper.Map<Org>(orgDTO)).ConfigureAwait(false)));
            }
            catch (Exception Ex)
            {
                return new Response(null, false, 500, new List<string>() { Ex.Message });
            }
        }

        public async Task<Response> DeleteAsync(int id)
        {
            try
            {
                OrgDTO orgDTO = (OrgDTO)(await GetByIdAsync(id).ConfigureAwait(false)).Content!;
                return new Response(null, true, 202, null, _mapper.Map<OrgDTO>(await _repository.DeleteAsync(_mapper.Map<Org>(orgDTO)).ConfigureAwait(false)));
            }
            catch (Exception Ex)
            {
                return new Response(null, false, 500, new List<string>() { Ex.Message });
            }
        }

        public async Task<Response> UpdateAsync(OrgDTO orgDTO)
        {
            try
            {
                return new Response(null, true, 204, null, _mapper.Map<OrgDTO>(await _repository.UpdateAsync(_mapper.Map<Org>(orgDTO)).ConfigureAwait(false)));
            }
            catch (Exception Ex)
            {
                return new Response(null, false, 500, new List<string>() { Ex.Message });
            }
        }
    }
}
