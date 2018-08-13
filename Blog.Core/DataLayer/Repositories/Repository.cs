using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Blog.Core.DataLayer.Contracts;
using Blog.Core.EntityLayer;
using Blog.Core.EntityLayer.Dbo;
using Microsoft.EntityFrameworkCore;

namespace Blog.Core.DataLayer.Repositories
{
    public abstract class Repository : IRepository
    {
        public Repository(IUserInfo userInfo, DbContext dbContext)
        {
            UserInfo = userInfo;
            DbContext = dbContext;
        }

        protected IUserInfo UserInfo { get; }
        protected DbContext DbContext { get; }

        protected virtual void Add<TEntity>(TEntity entity)
            where TEntity : class, IAuditableEntity
        {
            if (entity is IAuditableEntity auditableEntity)
            {
                auditableEntity.CreationUser = UserInfo.Name;

                if (!auditableEntity.CreationDateTime.HasValue)
                {
                    auditableEntity.CreationDateTime = DateTime.Now;
                }
            }

            DbContext.Set<TEntity>().Add(entity);
        }

        protected virtual void Update<TEntity>(TEntity entity)
            where TEntity : class, IAuditableEntity
        {
            if (entity is IAuditableEntity auditableEntity)
            {
                auditableEntity.LastUpdateUser = UserInfo.Name;

                if (!auditableEntity.CreationDateTime.HasValue)
                {
                    auditableEntity.CreationDateTime = DateTime.Now;
                }
            }

            DbContext.Set<TEntity>().Add(entity);
        }

        protected virtual void Remove<TEntity>(TEntity entity)
            where TEntity : class, IAuditableEntity
            => DbContext.Set<TEntity>().Remove(entity);

        protected virtual IEnumerable<ChangeLog> GetChanges()
        {
            var exclusions = DbContext.Set<ChangeLogExclusion>().ToList();

            foreach (var entry in DbContext.ChangeTracker.Entries())
            {
                if (entry.State != EntityState.Modified)
                    continue;

                var entityType = entry.Entity.GetType();

                if (HasOnlyOneMatch(entityType))
                    yield break;

                foreach (var property in entityType.GetTypeInfo().DeclaredProperties)
                {
                    if (HasASingleExclusion(entityType, property))
                        continue;

                    var originalValue = entry.Property(property.Name).OriginalValue;
                    var currentValue = entry.Property(property.Name).CurrentValue;

                    if (string.Concat(originalValue) == string.Concat(currentValue))
                        continue;

                    var key = GetPrimaryKeyFromEntity(entry.Entity);

                    yield return new ChangeLog
                    {
                        ClassName = entityType.Name,
                        PropertyName = property.Name,
                        Key = key,
                        OriginalValue = originalValue?.ToString() ?? string.Empty,
                        CurrentValue = currentValue?.ToString() ?? string.Empty,
                        UserName = UserInfo.Name,
                        ChangeDate = DateTime.Now,
                    };
                }
            }

            bool HasOnlyOneMatch(Type entityType) =>
                exclusions.Where(item => item.EntityName == entityType.Name && item.PropertyName == "*").Count() == 1;

            bool HasASingleExclusion(Type entityType, PropertyInfo property)
            {
                return
                    MatchesAnyEntityProperty().Count() == 1 ||
                    MatchesSpecificEntityProperty().Count() == 1;

                // find exclusions for all occurences of *.Property
                IEnumerable<ChangeLogExclusion> MatchesAnyEntityProperty() =>
                    exclusions.Where(item =>
                        item.EntityName == "*" &&
                        string.Compare(item.PropertyName, property.Name, true) == 0);
                // find exclusions for all occurrences of Entity.Property
                IEnumerable<ChangeLogExclusion> MatchesSpecificEntityProperty() =>
                    exclusions.Where(item =>
                        item.EntityName == entityType.Name &&
                        string.Compare(item.PropertyName, property.Name, true) == 0);

            }

            // TODO: improve the way to retrieve primary key value from entity instance
            string GetPrimaryKeyFromEntity(object entity) =>
                entity.GetType().GetProperties()[0].GetValue(entity, null).ToString();
        }

        public int CommitChanges()
        {
            var dbChangeSet = DbContext.Set<ChangeLog>();

            foreach (var change in GetChanges().ToList())
            {
                dbChangeSet.Add(change);
            }

            return DbContext.SaveChanges();
        }

        public Task<int> CommitChangesAsync()
        {
            var dbChangeSet = DbContext.Set<ChangeLog>();

            foreach (var change in GetChanges().ToList())
            {
                dbChangeSet.Add(change);
            }

            return DbContext.SaveChangesAsync();
        }
    }
}
