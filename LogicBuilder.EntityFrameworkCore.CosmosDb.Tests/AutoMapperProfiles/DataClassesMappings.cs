using AutoMapper;
using LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Data;
using LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Models;
using System.Linq;

namespace LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.AutoMapperProfiles
{
    public class DataClassesMappings : Profile
    {
        public DataClassesMappings()
        {
            CreateMap<Address, AddressModel>()
                .ForAllMembers(o => o.ExplicitExpansion());
            CreateMap<AddressModel, Address>();
            CreateMap<AlternateAddress, AlternateAddressModel>()
                .ForMember(dest => dest.AlternateAddressID, opt => opt.MapFrom(src => src.AlternateAddressID))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.ZipCode))
                .ForAllMembers(o => o.ExplicitExpansion());
            CreateMap<AlternateAddressModel, AlternateAddress>();
            CreateMap<Category, CategoryModel>()
                .ForAllMembers(o => o.ExplicitExpansion());
            CreateMap<CategoryModel, Category>();
            CreateMap<DataTypes, DataTypesModel>()
                    .ForAllMembers(o => o.ExplicitExpansion());
            CreateMap<DataTypesModel, DataTypes>();
            CreateMap<Product, ProductModel>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => new CategoryModel
                {
                    CategoryID = src.Category!.CategoryID,/*ForMember does not execute if src.Category is null*/
                    CategoryName = src.Category.CategoryName
                }))
                .ForMember(dest => dest.SupplierAddress, opt => opt.MapFrom(src => new AddressModel
                {
                    AddressID = src.SupplierAddress!.AddressID,/*ForMember does not execute if src.SupplierAddress is null*/
                    City = src.SupplierAddress.City,
                    State = src.SupplierAddress.State,
                    ZipCode = src.SupplierAddress.ZipCode
                }))
                .ForMember(dest => dest.AlternateAddressCities, opt => opt.MapFrom(src => src.AlternateAddresses!.Select(a => a.City).ToList()))
                .ForAllMembers(o => o.ExplicitExpansion());
            CreateMap<ProductModel, Product>();
        }
    }
}
