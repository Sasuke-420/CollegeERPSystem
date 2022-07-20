using CollegeERPSystem.BusRoute.Grpc;
using SharedServices.Domain;

namespace CollegeERPSystem.BusRoute.Domain
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BusRouteDTO, BusRoute.Domain.Models.BusRoute>();
            CreateMap<BusRoute.Domain.Models.BusRoute, BusRouteDTO>();
        }
    }
}
