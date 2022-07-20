using AutoMapper;
using CollegeERPSystem.Services.Domain.Models;
using CollegeERPSystem.Services.Domain.Repositories;
using CollegeERPSystem.Services.DTO;

namespace CollegeERPSystem.Services.Domain.Services
{
    public class ProgrammeService
    {
        private readonly IMapper _mapper;
        private readonly ProgrammeRepository _repository;
        public ProgrammeService(IMapper mapper, ProgrammeRepository Repository)
        {
            _mapper = mapper;
            _repository = Repository;
        }

        public async Task<Response> GetAllAsync()
        {
            try
            {
                return new Response(null, true, 200, null,
                    _mapper.Map<IEnumerable<ProgrammeDTO>>(await _repository.GetAllAsync()));
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
                var result = _mapper.Map<ProgrammeDTO>(await _repository.GetByIdAsync(id));
                return new Response(null, true, result != null ? 200 : 404, null, result);
            }
            catch (Exception Ex)
            {
                return new Response(null, false, 500, new List<string>() { Ex.Message });
            }
        }

        public async Task<Response> CreateAsync(ProgrammeDTO ProgrammeDTO)
        {
            try
            {
                return new Response(null, true, 201, null, _mapper.Map<ProgrammeDTO>(await _repository.CreateAsync(_mapper.Map<Programme>(ProgrammeDTO)).ConfigureAwait(false)));
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
                ProgrammeDTO _programme = (ProgrammeDTO)(await GetByIdAsync(id).ConfigureAwait(false)).Content!;
                return new Response(null, true, 202, null, _mapper.Map<ProgrammeDTO>(await _repository.DeleteAsync(_mapper.Map<Programme>(_programme)).ConfigureAwait(false)));
            }
            catch (Exception Ex)
            {
                return new Response(null, false, 500, new List<string>() { Ex.Message });
            }
        }

        public async Task<Response> UpdateAsync(ProgrammeDTO ProgrammeDTO)
        {
            try
            {
                return new Response(null, true, 204, null, _mapper.Map<ProgrammeDTO>(await _repository.UpdateAsync(_mapper.Map<Programme>(ProgrammeDTO)).ConfigureAwait(false)));
            }
            catch (Exception Ex)
            {
                return new Response(null, false, 500, new List<string>() { Ex.Message });
            }
        }
    }
}