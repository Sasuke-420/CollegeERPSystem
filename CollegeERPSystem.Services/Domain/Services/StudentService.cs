using AutoMapper;
using CollegeERPSystem.Services.Domain.Models;
using CollegeERPSystem.Services.Domain.Repositories;
using CollegeERPSystem.Services.DTO;

namespace CollegeERPSystem.Services.Domain.Services
{
    public class StudentService
    {
        private readonly IMapper _mapper;
        private readonly StudentRepository _repository;
        public StudentService(IMapper mapper, StudentRepository Repository)
        {
            _mapper = mapper;
            _repository = Repository;
        }

        public virtual async Task<Response> GetAllAsync(PaginationModel pagination)
        {
            try
            {
                if(pagination.OrderList==null)
                {
                    pagination.OrderList = "Id";
                }
                return new Response(null, true, 200, null,
                    _mapper.Map<IEnumerable<StudentDTO>>(await _repository.GetAllAsync(pagination)));
            }
            catch (Exception Ex)
            {
                return new Response(null, false, 500, new List<string>() { Ex.Message });
            }
        }
        public virtual async Task<Response> GetByIdAsync(int id)
        {
            try
            {
                var result = _mapper.Map<StudentDTO>(await _repository.GetByIdAsync(id));
                return new Response(null, true, result != null ? 200 : 404, null, result);
            }
            catch (Exception Ex)
            {
                return new Response(null, false, 500, new List<string>() { Ex.Message });
            }
        }

        public virtual async Task<Response> CreateAsync(StudentDTO StudentDTO)
        {
            try
            {
                return new Response(null, true, 201, null, _mapper.Map<StudentDTO>(await _repository.CreateAsync(_mapper.Map<Student>(StudentDTO)).ConfigureAwait(false)));
            }
            catch (Exception Ex)
            {
                return new Response(null, false, 500, new List<string>() { Ex.Message });
            }
        }

        public virtual async Task<Response> DeleteAsync(int id)
        {
            try
            {
                StudentDTO StudentDTO = (StudentDTO)(await GetByIdAsync(id).ConfigureAwait(false)).Content!;
                return new Response(null, true, 202, null, _mapper.Map<StudentDTO>(await _repository.DeleteAsync(_mapper.Map<Student>(StudentDTO)).ConfigureAwait(false)));
            }
            catch (Exception Ex)
            {
                return new Response(null, false, 500, new List<string>() { Ex.Message });
            }
        }

        public virtual async Task<Response> UpdateAsync(StudentDTO StudentDTO)
        {
            try
            {
                return new Response(null, true, 204, null, _mapper.Map<StudentDTO>(await _repository.UpdateAsync(_mapper.Map<Student>(StudentDTO)).ConfigureAwait(false)));
            }
            catch (Exception Ex)
            {
                return new Response(null, false, 500, new List<string>() { Ex.Message });
            }
        }
    }
}

