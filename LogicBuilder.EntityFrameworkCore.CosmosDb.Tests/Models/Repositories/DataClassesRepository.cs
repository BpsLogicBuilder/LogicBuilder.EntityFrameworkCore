using AutoMapper;
using LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Data.Stores;
using LogicBuilder.EntityFrameworkCore.Repositories;

namespace LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Models.Repositories
{
    public class DataClassesRepository(IDataClassesStore store, IMapper mapper) : ContextRepositoryBase(store, mapper), IDataClassesRepository
    {
    }
}
