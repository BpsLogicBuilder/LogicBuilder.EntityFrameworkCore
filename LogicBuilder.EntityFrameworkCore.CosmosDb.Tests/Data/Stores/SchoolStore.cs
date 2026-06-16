using LogicBuilder.EntityFrameworkCore.Crud.DataStores;

namespace LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Data.Stores
{
    public class SchoolStore(SchoolContext context) : StoreBase(context), ISchoolStore
    {
    }
}
