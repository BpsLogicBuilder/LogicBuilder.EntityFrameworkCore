using System.ComponentModel.DataAnnotations;

namespace LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Data
{
    public class OfficeAssignment
    {
        public string InstructorID { get; set; } = "";
        [StringLength(50)]
        public string Location { get; set; } = "";
    }
}
