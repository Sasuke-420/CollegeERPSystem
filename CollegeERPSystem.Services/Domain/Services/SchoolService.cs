using AutoMapper;
using CollegeERPSystem.Services.Domain.Models;
using CollegeERPSystem.Services.Domain.Repositories;
using CollegeERPSystem.Services.DTO;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace CollegeERPSystem.Services.Domain.Services
{
    public class SchoolService
    {
        private readonly IMapper _mapper;
        private readonly SchoolRepository _repository;
        private static SemaphoreSlim _lock = new SemaphoreSlim(1, 1);
        private readonly IMemoryCache _cache;
        public SchoolService(IMemoryCache cache,IMapper mapper, SchoolRepository Repository)
        {
            _cache = cache;
            _mapper = mapper;
            _repository = Repository;
        }

        public async Task<Response> GetAllAsync()
        {
            try
            {
                bool isAvailable = _cache.TryGetValue("Schools", out Response result);
                if (isAvailable)
                    return result;
                try
                {
                    await _lock.WaitAsync();
                    isAvailable = _cache.TryGetValue("Schools", out result);
                    if (isAvailable)
                        return result;
                    result = new Response(null, true, 200, null,
                     _mapper.Map<IEnumerable<SchoolDTO>>(await _repository.GetAllAsync()));
                    var options = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddMinutes(3),
                        SlidingExpiration = TimeSpan.FromSeconds(120),
                        Size = 1024
                    };
                    _cache.Set("Schools", result, options);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    _lock.Release();
                }

                return result;
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
                var result = _mapper.Map<SchoolDTO>(await _repository.GetByIdAsync(id));
                return new Response(null, true, result!=null?200:404, null,result);
            }
            catch (Exception Ex)
            {
                return new Response(null, false, 500, new List<string>() { Ex.Message });
            }
        }

        public async Task<Response> CreateAsync(SchoolDTO schoolDTO)
        {
            try
            {
                _cache.Remove("Schools");
                return new Response(null, true, 201, null, _mapper.Map<SchoolDTO>(await _repository.CreateAsync(_mapper.Map<School>(schoolDTO)).ConfigureAwait(false)));
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
                _cache.Remove("Schools");
                SchoolDTO schoolDTO = (SchoolDTO)(await GetByIdAsync(id).ConfigureAwait(false)).Content!;
                return new Response(null, true, 202, null, _mapper.Map<SchoolDTO>(await _repository.DeleteAsync(_mapper.Map<School>(schoolDTO)).ConfigureAwait(false)));
            }
            catch (Exception Ex)
            {
                return new Response(null, false, 500, new List<string>() { Ex.Message });
            }
        }

        public async Task<Response> UpdateAsync(SchoolDTO schoolDTO)
        {
            try
            {
                _cache.Remove("Schools");
                return new Response(null, true, 204,null, _mapper.Map<SchoolDTO>(await _repository.UpdateAsync(_mapper.Map<School>(schoolDTO)).ConfigureAwait(false)));
            }
            catch (Exception Ex)
            {
                return new Response(null, false, 500, new List<string>() { Ex.Message });
            }
        }
    }
}
