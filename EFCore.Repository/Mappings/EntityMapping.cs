using EFCore.Domain.Entities;
using EFCore.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Repository.Mappings;

public abstract class EntityMapping<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : Entity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.ModifiedAt).IsRequired();
        builder.Property(p => p.DeletedAt).IsRequired(false);
        builder.Property(p => p.EntityStatus)
        .HasConversion(
            p => p.ToString(),
            p => (EntityStatus)Enum.Parse(typeof(EntityStatus), p)
        )
        .IsRequired();
    }
}
