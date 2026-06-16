using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Data
{
    [Table("Instructor")]
    public class Instructor : BaseDataClass
    {
        [Key]
        public string ID { get; set; } = "";

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = "";
        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string FirstName { get; set; } = "";

        [DataType(DataType.Date)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        public ICollection<CourseAssignment>? Courses { get; set; }
        public OfficeAssignment? OfficeAssignment { get; set; }
    }
}
