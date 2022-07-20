namespace CollegeERPSystem.Services.DTO
{
    public class OrgDTO
    {
        public int? Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Pincode { get; set; }
        public string? Email { get; set; }
        public bool IsDeleted { get; set; }
    }
}
