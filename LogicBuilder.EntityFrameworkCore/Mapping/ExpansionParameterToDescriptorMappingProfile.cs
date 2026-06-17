using AutoMapper;
using LogicBuilder.Expressions.Utils.ExpansionDescriptors;
using LogicBuilder.Forms.Parameters.Expansions;

namespace LogicBuilder.EntityFrameworkCore.Mapping
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class ExpansionParameterToDescriptorMappingProfile : Profile
    {
        public ExpansionParameterToDescriptorMappingProfile()
        {
            CreateMap<SelectExpandDefinitionParameters, SelectExpandDefinitionDescriptor>();
            CreateMap<SelectExpandItemFilterParameters, SelectExpandItemFilterDescriptor>();
            CreateMap<SelectExpandItemParameters, SelectExpandItemDescriptor>();
            CreateMap<SelectExpandItemQueryFunctionParameters, SelectExpandItemQueryFunctionDescriptor>();
            CreateMap<SortCollectionParameters, SortCollectionDescriptor>();
            CreateMap<SortDescriptionParameters, SortDescriptionDescriptor>();
        }
    }
}
