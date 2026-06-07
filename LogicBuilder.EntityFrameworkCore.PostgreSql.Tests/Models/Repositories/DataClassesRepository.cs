using AutoMapper;
using LogicBuilder.EntityFrameworkCore.PostgreSql.Tests.Data.Stores;
using LogicBuilder.EntityFrameworkCore.Repositories;

namespace LogicBuilder.EntityFrameworkCore.PostgreSql.Tests.Models.Repositories
{
    public class DataClassesRepository(IDataClassesStore store, IMapper mapper) : ContextRepositoryBase(store, mapper), IDataClassesRepository
    {
    }
}
