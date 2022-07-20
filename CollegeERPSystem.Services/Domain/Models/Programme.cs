using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeERPSystem.Services.Domain.Models
{
    [Table("Programmes")]
    [Microsoft.EntityFrameworkCore.Index(nameof(Code), IsUnique = true)]
    public class Programme
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public string? Code { get; set; }

        [Required]
        [MaxLength(Constants.NameSize, ErrorMessage = Constants.RequiredError)]
        public string? Name { get; set; }

        [Required]
        public int? Tenure { get; set; }

        [ForeignKey("SchoolId")]
        public virtual School? Schools { get; set; }
        public int? SchoolId { get; set; }
        public bool IsDeleted { get; set; }

        // navigation properties
        public virtual List<Student>? Students { get;set; }
    }
}
