using CollegeERPSystem.Services.Domain.Models;
using CollegeERPSystem.Services.DTO;

namespace CollegeERPSystem.Services.Domain
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<SchoolDTO, School>();
            CreateMap<School, SchoolDTO>();

            CreateMap<MarksDTO, Marks>();
            CreateMap<Marks, MarksDTO>();

            CreateMap<StudentDTO, Student>();
            CreateMap<Student, StudentDTO>()
                .ForMember(dest => dest.Tenure, opt => opt.MapFrom(src=>src.Program!.Tenure))
                .ForMember(dest => dest.Section, opt => { opt.PreCondition(x => x.ClassId.HasValue==true); opt.MapFrom(src => src.Grade!.Section); });

            CreateMap<ClassDTO, Class>();
            CreateMap<Class, ClassDTO>();

            CreateMap<ProgrammeDTO, Programme>();
            CreateMap<Programme, ProgrammeDTO>();

            CreateMap<OrgDTO, Org>();
            CreateMap<Org, OrgDTO>();
        }
    }
}
