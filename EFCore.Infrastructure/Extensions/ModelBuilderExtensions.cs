using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;

namespace EFCore.Infrastructure.Extensions;

public static class ModelBuilderExtensions
{
    public static ModelBuilder AddIsDeletedBehavior(this ModelBuilder modelBuilder, IMutableEntityType entityType)
    {
        var entityStatusProperty = entityType.FindProperty("IsDeleted");

        if (entityStatusProperty is not null && entityStatusProperty.ClrType == typeof(bool))
        {
            var parameter = Expression.Parameter(entityType.ClrType);
            var property = Expression.PropertyOrField(parameter, "IsDeleted");
            var filter = Expression.Lambda(Expression.Not(property), parameter);

            modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
        }
        return modelBuilder;
    }
}
