using DabeaV2.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DabeaV2.Repositories.Interfaces
{
    public interface IRepository
    {
        IDbContextTransaction BeginTransaction();
        Task<T> Get<T>(Expression<Func<T, bool>> parameter) where T : BaseEntity;
        Task<T> GetActive<T>(Expression<Func<T, bool>> parameter) where T : BaseEntity;
        IQueryable<T> GetAll<T>() where T : BaseEntity;
        IQueryable<T> GetAllActive<T>() where T : BaseEntity;
        Task Add<T>(T entity) where T : BaseEntity;
        Task Update<T>(T entity, bool updateModified = true) where T : BaseEntity;
        Task RemoveActive<T>(T entity, bool updateModified = true) where T : BaseEntity;
        Task Remove<T>(T entity) where T : BaseEntity;
    }
}
