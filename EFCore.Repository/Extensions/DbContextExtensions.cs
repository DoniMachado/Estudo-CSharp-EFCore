using EFCore.Domain.Common.Entities;
using EFCore.Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EFCore.Infrastructure.Extensions;

public static class DbContextExtensions
{
    public static async Task<T> WithNoLock<T>(this DbContext context, Func<Task<T>> query)
    {
        if (!context.Database.IsInMemory())
        {
            using var transaction = context.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            var result = await query();
            transaction.Commit();
            return result;
        }
        else
        {
            var result = await query();           
            return result;
        }
    }

    public static ChangeTracker OnSaveChangesCustom(this ChangeTracker changeTracker)
    {
        foreach(var entry in changeTracker.Entries<Entity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.SetCreator();
                    break;
                case EntityState.Modified:
                    entry.Entity.SetUpdate();
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.SetDeleted();
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return changeTracker;
    }

    public static async Task OnDispatchEventsAsyncCustom(this ChangeTracker changeTracker, IDomainEventHandler domainEventHandler)
    {
        if (domainEventHandler is null)
            return;

        while(true)
        {
            var domainEventEntity = changeTracker.Entries<IHasDomainEvent>()
                .Select(x => x.Entity.DomainEvents)
                .SelectMany(x => x)
                .FirstOrDefault(domainEvent => !domainEvent.IsPublished);

            if (domainEventEntity is null)
                break;

            domainEventEntity.IsPublished = true;

            await domainEventHandler.Publish(domainEventEntity);
        }
    }
}
