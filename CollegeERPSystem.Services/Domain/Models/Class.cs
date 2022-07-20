using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeERPSystem.Services.Domain.Models
{
    [Table("Classes")]
    public class Class
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        [MaxLength(Constants.NameSize,ErrorMessage = Constants.LengthError)]
        public string? Name { get; set; }

        [Required]
        public int? Grade { get; set; }

        [ForeignKey("ProgramId")]
        public virtual Programme? Program { get; set; }
        public int? ProgramId { get; set; }

        [Required]
        [MaxLength(1, ErrorMessage = Constants.LengthError)]
        public string? Section { get; set; }
        public bool IsDeleted { get; set; }
    }
}
