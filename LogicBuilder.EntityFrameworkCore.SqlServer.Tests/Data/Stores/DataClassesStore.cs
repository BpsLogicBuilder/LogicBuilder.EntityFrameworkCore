using LogicBuilder.EntityFrameworkCore.Crud.DataStores;

namespace LogicBuilder.EntityFrameworkCore.SqlServer.Tests.Data.Stores
{
    public class DataClassesStore(DataClassesContext context) : StoreBase(context), IDataClassesStore
    {
    }
}
