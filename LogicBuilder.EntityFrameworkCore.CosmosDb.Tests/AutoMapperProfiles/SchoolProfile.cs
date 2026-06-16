using AutoMapper;
using LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Data;
using LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Models;
using System.Linq;

namespace LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.AutoMapperProfiles
{
    public class SchoolProfile : Profile
    {
        public SchoolProfile()
        {
            CreateMap<CourseAssignmentModel, CourseAssignment>()
                .ReverseMap()

                .ForAllMembers(o => o.ExplicitExpansion());

            CreateMap<CourseModel, Course>()
                .ReverseMap()
                .ForAllMembers(o => o.ExplicitExpansion());

            CreateMap<DepartmentModel, Department>()
                .ReverseMap()
                .ForAllMembers(o => o.ExplicitExpansion());

            CreateMap<EnrollmentModel, Enrollment>()
                .ReverseMap()
                .ForMember(dest => dest.CourseTitle, opts => opts.MapFrom(x => x.CourseTitle))
                .ForMember(dest => dest.Grade, opts => opts.MapFrom(x => x.Grade.HasValue ? (Models.Grade?)(int)x.Grade.Value : null))
                .ForMember(dest => dest.GradeLetter, opts => opts.MapFrom(x => x.Grade.ToString()))
                .ForAllMembers(o => o.ExplicitExpansion());

            CreateMap<InstructorModel, Instructor>()
                .ReverseMap()
                .ForMember(dest => dest.FullName, opts => opts.MapFrom(x => x.FirstName + " " + x.LastName))
                .ForMember(dest => dest.OfficeAssignment, opt => opt.MapFrom(src => new OfficeAssignmentModel
                {
                    InstructorID = src.OfficeAssignment!.InstructorID,/*ForMember does not execute if src.SupplierAddress is null*/
                    Location = src.OfficeAssignment!.Location
                }))
                .ForAllMembers(o => o.ExplicitExpansion());

            CreateMap<OfficeAssignmentModel, OfficeAssignment>()
                .ReverseMap()
                .ForAllMembers(o => o.ExplicitExpansion());

            CreateMap<StudentModel, Student>()
                .ReverseMap()
            .ForMember(dest => dest.FullName, opts => opts.MapFrom(x => x.FirstName + " " + x.LastName))
            .ForMember(dest => dest.CourseIds, opt => opt.MapFrom(src => src.Enrollments!.Select(a => a.CourseID).ToList()))
            .ForAllMembers(o => o.ExplicitExpansion());

            CreateMap<LookUpsModel, LookUps>().ReverseMap();
        }
    }
}
