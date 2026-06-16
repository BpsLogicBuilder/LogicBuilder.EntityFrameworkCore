using AutoMapper;
using LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Data.Stores;
using LogicBuilder.EntityFrameworkCore.Repositories;

namespace LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Models.Repositories
{
    public class SchoolRepository(ISchoolStore store, IMapper mapper) : ContextRepositoryBase(store, mapper), ISchoolRepository
    {
    }
}
