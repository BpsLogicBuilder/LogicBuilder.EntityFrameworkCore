using AutoMapper;
using LogicBuilder.EntityFrameworkCore.PostgreSql.Tests.Data;
using LogicBuilder.EntityFrameworkCore.PostgreSql.Tests.Models;

namespace LogicBuilder.EntityFrameworkCore.PostgreSql.Tests.AutoMapperProfiles
{
    public class DataClassesMappings : Profile
    {
        public DataClassesMappings()
        {
            CreateMap<Address, AddressModel>()
                .ForAllMembers(o => o.ExplicitExpansion());
            CreateMap<AddressModel, Address>();
            CreateMap<AlternateAddress, AlternateAddressModel>()
                .ForAllMembers(o => o.ExplicitExpansion());
            CreateMap<AlternateAddressModel, AlternateAddress>();
            CreateMap<Category, CategoryModel>()
                    .ForAllMembers(o => o.ExplicitExpansion());
            CreateMap<CategoryModel, Category>();
            CreateMap<DataTypes, DataTypesModel>()
                    .ForAllMembers(o => o.ExplicitExpansion());
            CreateMap<DataTypesModel, DataTypes>();   
            CreateMap<Product, ProductModel>()
                .ForAllMembers(o => o.ExplicitExpansion());
            CreateMap<ProductModel, Product>();
        }
    }
}
