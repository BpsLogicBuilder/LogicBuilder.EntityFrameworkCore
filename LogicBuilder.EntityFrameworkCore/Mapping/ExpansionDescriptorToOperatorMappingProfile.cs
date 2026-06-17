using AutoMapper;
using LogicBuilder.Expressions.Utils.ExpansionDescriptors;
using LogicBuilder.Expressions.Utils.Expansions;
using LogicBuilder.Expressions.Utils.Strutures;

namespace LogicBuilder.EntityFrameworkCore.Mapping
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class ExpansionDescriptorToOperatorMappingProfile : Profile
    {
        public ExpansionDescriptorToOperatorMappingProfile()
        {
            CreateMap<SelectExpandDefinitionDescriptor, SelectExpandDefinition>();
            CreateMap<SelectExpandItemFilterDescriptor, SelectExpandItemFilter>();
            CreateMap<SelectExpandItemDescriptor, SelectExpandItem>();
            CreateMap<SelectExpandItemQueryFunctionDescriptor, SelectExpandItemQueryFunction>();
            CreateMap<SortCollectionDescriptor, SortCollection>();
            CreateMap<SortDescriptionDescriptor, SortDescription>();
        }
    }
}
