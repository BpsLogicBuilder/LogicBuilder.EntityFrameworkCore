using LogicBuilder.EntityFrameworkCore.Crud.DataStores;

namespace LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Data.Stores
{
    public class DataClassesStore(DataClassesContext context) : StoreBase(context), IDataClassesStore
    {
    }
}
