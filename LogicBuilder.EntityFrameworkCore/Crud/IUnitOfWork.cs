using LogicBuilder.Data;
using LogicBuilder.EntityFrameworkCore.Crud.DbMappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogicBuilder.EntityFrameworkCore.Crud
{
    internal interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; }
        Dictionary<Type, object> RepositoryDictionary { get; }
        Dictionary<Type, object> MapperDictionary { get; }
        Task<bool> SaveChangesAsync();
        GenericRepository<T> GetRepository<T>() where T : class, IBaseData;
        DbMapperBase<T> GetMapper<T>() where T : class, IBaseData;
    }
}
