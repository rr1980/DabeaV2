using DabeaV2.Common.Enums;
using DabeaV2.DB;
using DabeaV2.Entities;
using DabeaV2.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DabeaV2.Repositories
{
    public class Repository : IRepository
    {
        private readonly ILogger<Repository> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _dbContext;

        public Repository(ILogger<Repository> logger, IHttpContextAccessor httpContextAccessor, DataContext dbContext)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction();
        }

        public Task<T> Get<T>(Expression<Func<T, bool>> parameter) where T : BaseEntity
        {
            return _dbContext.Set<T>().FirstOrDefaultAsync(parameter);
        }

        public Task<T> GetActive<T>(Expression<Func<T, bool>> parameter) where T : BaseEntity
        {
            return GetAll<T>().Where(x => x.IsActive).FirstOrDefaultAsync(parameter);
        }

        public IQueryable<T> GetAll<T>() where T : BaseEntity
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public IQueryable<T> GetAllActive<T>() where T : BaseEntity
        {
            return GetAll<T>().Where(x => x.IsActive);
        }

        public Task Add<T>(T entity) where T : BaseEntity
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                _logger.LogTrace("Add Entity", entity.GetType().Name);
                AddModification(entity, EntityModificationType.Added);

                _dbContext.Add<T>(entity);
                return _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new DabeaV2RepositoryException($"Entity konnte nicht hinzugefügt werden!", ex);
            }
        }

        public Task Update<T>(T entity, bool updateModified = true) where T : BaseEntity
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                _logger.LogTrace("Update Entity", entity.GetType().Name);
                if (updateModified)
                {
                    AddModification(entity, EntityModificationType.Updated);
                }

                _dbContext.Update(entity);
                return _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new DabeaV2RepositoryException($"Entity konnte nicht upgedated werden!", ex);
            }
        }

        public Task RemoveActive<T>(T entity, bool updateModified = true) where T : BaseEntity
        {
            _logger.LogTrace("RemoveActive Entity", entity.GetType().Name);
            if (updateModified)
            {
                AddModification(entity, EntityModificationType.Activation);
            }
            entity.IsActive = false;

            _dbContext.Update(entity);
            return _dbContext.SaveChangesAsync();
        }

        public Task Remove<T>(T entity) where T : BaseEntity
        {
            _logger.LogTrace("RemoveActive Entity", entity.GetType().Name);
            _dbContext.Remove(entity);
            return _dbContext.SaveChangesAsync();
        }

        private void AddModification(BaseEntity entity, EntityModificationType modificationType)
        {
            if (entity is IModifiableEntity)
            {
                var e = entity as IModifiableEntity;

                var modification = new Modification
                {
                    ModificationType = modificationType,
                    Date = DateTime.Now,
                    Benutzer = GetCurrentBenutzer().Result
                };

                if (modificationType == EntityModificationType.Added)
                {
                    modification.ModificationItems.Add(new ModificationItem
                    {
                        PropertyName = "Added"
                    });
                }
                else if (modificationType == EntityModificationType.Updated || modificationType == EntityModificationType.Activation)
                {
                    var entry = _dbContext.Entry(e);

                    foreach (var item in entry.Properties)
                    {
                        if (item.IsModified)
                        {
                            modification.ModificationItems.Add(new ModificationItem
                            {
                                PropertyName = item.Metadata.Name,
                                OldValue = item.OriginalValue.ToString(),
                                NewValue = item.CurrentValue.ToString()
                            });
                        }
                    }
                }

                e.Modifications.Add(modification);
            }
        }

        private Task<Benutzer> GetCurrentBenutzer()
        {
            if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User != null)
            {
                var _uId = _httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == "UserId").Select(x => x.Value).FirstOrDefault();

                if (!string.IsNullOrEmpty(_uId))
                {
                    var uId = long.Parse(_uId);

                    if (uId == 0)
                    {
                        throw new Exception("UserID not Found!");
                    }

                    return Get<Benutzer>(x => x.Id == uId);
                }
            }

            return Task.FromResult<Benutzer>(null);
        }
    }
}
