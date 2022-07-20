namespace CollegeERPSystem.Services.DTO
{
    public class ClassDTO
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int? Grade { get; set; }
        public int? ProgramId { get; set; }
        public string? Section { get; set; }
        public bool IsDeleted { get; set; }
    }
}
