using AutoMapper;
using CollegeERPSystem.BusRoute.Domain;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using SharedServices.Domain;

namespace CollegeERPSystem.BusRoute.Grpc
{
    public class BusServiceGrpcImplementation : IBusService
    {
        private readonly IMapper _mapper;
        private readonly BusRepository _repository;
        public BusServiceGrpcImplementation(IMapper Mapper,BusRepository Repository)
        {
            _mapper = Mapper;
            _repository = Repository;
        }
        public async Task<IEnumerable<BusRouteDTO>> GetBusRoute()
        {
            Empty request = new Empty();
           var result = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<BusRouteDTO>>(result);
        }
    }
#nullable disable
    public class Context : ServerCallContext
    {
        protected override string MethodCore { get; }
        protected override string HostCore { get; }
        protected override string PeerCore { get; }
        protected override DateTime DeadlineCore { get; }
        protected override Metadata RequestHeadersCore { get; }
        protected override CancellationToken CancellationTokenCore { get; }
        protected override Metadata ResponseTrailersCore { get; }
        protected override global::Grpc.Core.Status StatusCore { get; set; }
        protected override WriteOptions WriteOptionsCore { get; set; }
        protected override AuthContext AuthContextCore { get; }

        protected override ContextPropagationToken CreatePropagationTokenCore(ContextPropagationOptions options)
        {
            throw new NotImplementedException();
        }

        protected override Task WriteResponseHeadersAsyncCore(Metadata responseHeaders)
        {
            throw new NotImplementedException();
        }
    }
#nullable enable
}
