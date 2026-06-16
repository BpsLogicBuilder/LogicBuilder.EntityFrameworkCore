using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Data
{
    public class Student : BaseDataClass
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
        public DateTime EnrollmentDate { get; set; }


        public ICollection<Enrollment>? Enrollments { get; set; }
    }

}
