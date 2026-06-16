using System.ComponentModel.DataAnnotations;


namespace LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Models
{
    public class OfficeAssignmentModel
    {
		public string InstructorID { get; set; } = "";

        [StringLength(50)]
		public string Location { get; set; } = "";
    }
}