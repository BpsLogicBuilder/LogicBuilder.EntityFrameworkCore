using LogicBuilder.EntityFrameworkCore.Crud.DataStores;

namespace LogicBuilder.EntityFrameworkCore.PostgreSql.Tests.Data.Stores
{
    public class DataClassesStore(DataClassesContext context) : StoreBase(context), IDataClassesStore
    {
    }
}
