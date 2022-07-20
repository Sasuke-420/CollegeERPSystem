using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CollegeERPSystem.Services.Domain.Models
{
    [System.ComponentModel.DataAnnotations.Schema.Table("Org")]
    [Index(nameof(Code),IsUnique =true)]
    public class Org
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public string? Code { get; set; }

        [Required]
        [MaxLength(Constants.NameSize, ErrorMessage = Constants.RequiredError)]
        public string? Name { get; set; }

        [MaxLength(Constants.AddressSize,ErrorMessage = Constants.RequiredError)]
        public string? Address { get; set; }              // Another table to be made later

        [MaxLength(Constants.AddressSize, ErrorMessage = Constants.RequiredError)]
        public string? Pincode { get; set; }             // Can be added to address table if made

        [EmailAddress]
        public string? Email { get; set; }

        public bool IsDeleted { get; set; }
    }
}
