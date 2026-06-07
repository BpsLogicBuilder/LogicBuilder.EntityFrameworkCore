using LogicBuilder.EntityFrameworkCore.Crud.DataStores;

namespace LogicBuilder.EntityFrameworkCore.PostgreSql.Tests.Data.Stores
{
    public class SchoolStore(SchoolContext context) : StoreBase(context), ISchoolStore
    {
    }
}
