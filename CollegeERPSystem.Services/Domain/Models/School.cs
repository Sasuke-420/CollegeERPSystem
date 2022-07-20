using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeERPSystem.Services.Domain.Models
{
    [Table("Schools")]
    [Microsoft.EntityFrameworkCore.Index(nameof(Code),IsUnique =true)]
    public class School
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        [MaxLength(Constants.NameSize, ErrorMessage = Constants.LengthError)]
        public string? Name { get; set; }

        [MaxLength(Constants.CodeSize, ErrorMessage = Constants.LengthError)]
        public string? Code { get; set; }

        [ForeignKey("OrgId")]
        public virtual Org? Organisation { get; set; }
        public int? OrgId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
