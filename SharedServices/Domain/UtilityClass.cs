using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedServices.Domain
{
    [ProtoContract]
    public class BusRouteDTO
    {
        [ProtoMember(1)]
        public int? Id { get; set; }
        [ProtoMember(2)]
        public string? Name { get; set; }
        [ProtoMember(3)]
        public int? RouteNo { get; set; }

        // navigational properties
    }

    [System.ServiceModel.ServiceContract]
    public interface IBusService
    {
        Task<IEnumerable<BusRouteDTO>> GetBusRoute();
    }

}
