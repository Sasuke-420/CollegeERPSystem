using System.ComponentModel.DataAnnotations;

namespace CollegeERPSystem.BusRoute.Domain.Models
{
    public class BusRoute
    {
        [Key]
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int? RouteNo { get; set; }
    }
}
