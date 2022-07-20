namespace CollegeERPSystem.Services.DTO
{
    public class StudentDTO
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? RegistrationNo { get; set; }
        public int? ProgramId { get; set; }
        public int? Batch { get; set; }
        public int? ClassId { get; set; }
        public string? Section { get; set; }
        public string? Tenure { get; set; }
        public bool IsDeleted { get; set; }
    }
}
