using EFCore.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Repository.Behaviors;

public static class AddIsDeletedBehaviorBuilder
{
    public static ModelBuilder AddIsDeletedBehavior(this ModelBuilder modelBuilder, IMutableEntityType entityType)
    {
        var entityStatusProperty = entityType.GetProperty("EntityStatus");

        if(entityStatusProperty is not null && entityStatusProperty.ClrType == typeof(EntityStatus)) 
        {
            var parameter = Expression.Parameter(entityType.ClrType);
            var property = Expression.PropertyOrField(parameter,"EntityStatus");
            var filter = Expression.Lambda(Expression.NotEqual(property, Expression.Constant(EntityStatus.Deleted)),parameter);

            modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);           
        }
        return modelBuilder;
    }
}
