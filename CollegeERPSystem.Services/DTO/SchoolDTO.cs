namespace CollegeERPSystem.Services.DTO
{
    public class SchoolDTO
    {
        public int? Id { get; set; }     
        public string? Name { get; set; }  
        public string? Code { get; set; }
        public int? OrgId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
