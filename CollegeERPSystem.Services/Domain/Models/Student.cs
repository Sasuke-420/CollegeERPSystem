using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeERPSystem.Services.Domain.Models
{
    [Table("Students")]
    public class Student
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        [MaxLength(Constants.NameSize, ErrorMessage = Constants.LengthError)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(Constants.NameSize, ErrorMessage = Constants.LengthError)]
        public string? RegistrationNo { get; set; }

        [ForeignKey("ProgramId")]
        public virtual Programme? Program { get; set; }
        public int? ProgramId { get; set; }

        [Required]
        public int? Batch { get; set; }

        [ForeignKey("ClassId")]
        public virtual Class? Grade { get; set; }
        public int? ClassId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
