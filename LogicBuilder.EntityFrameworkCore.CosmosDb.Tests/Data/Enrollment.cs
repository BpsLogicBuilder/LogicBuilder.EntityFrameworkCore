namespace LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Data
{
    public class Enrollment
    {
        public string CourseID { get; set; } = "";
        public Grade? Grade { get; set; }
        public string CourseTitle { get; set; } = "";
    }

    public enum Grade
    {
        A, B, C, D, F
    }
}
