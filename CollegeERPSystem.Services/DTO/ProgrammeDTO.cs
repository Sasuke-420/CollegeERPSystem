namespace CollegeERPSystem.Services.DTO
{
    public class ProgrammeDTO
    {
        public int? Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int? Tenure { get; set; }
        public int? SchoolId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
