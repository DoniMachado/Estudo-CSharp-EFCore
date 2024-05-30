using EFCore.Domain.Common.Entities;
using EFCore.Domain.Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Infrastructure.Mappings;

public abstract class EntityMapping<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : Entity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(e => e.Id)
            .HasColumnName("Id")
            .ValueGeneratedNever();

        builder.OwnsOne(p => p.Audit)
            .Property(a => a.CreatedAt)
            .IsRequired()
            .HasColumnName("CreatedAt")
            .HasColumnType("DATETIMEOFFSET")
            .HasDefaultValue("SYSDATETIMEOFFSET()");

        builder.OwnsOne(p => p.Audit)
            .Property(a => a.ModifiedBy)
            .IsRequired()
            .HasColumnName("ModifiedBy")
            .HasMaxLength(256)
            .HasColumnType("VARCHAR(256)");

        builder.OwnsOne(p => p.Audit)
            .Property(a => a.UpdatedAt)
            .IsRequired(false)
            .HasColumnName("UpdatedAt")
            .HasColumnType("DATETIMEOFFSET");

        builder.OwnsOne(p => p.Audit)
             .Property(a => a.DeletedAt)
             .IsRequired(false)
             .HasColumnName("DeletedAt")
             .HasColumnType("DATETIMEOFFSET");


        builder.OwnsOne(p => p.Audit)
            .Property(a => a.LastAction)
            .IsRequired()
            .HasColumnName("LastAction")
            .HasMaxLength(64)
            .HasColumnType("VARCHAR(64)")
            .HasDefaultValue(nameof(ActionType.Insert));


        builder.Property(p => p.IsDeleted)
            .IsRequired()
            .HasColumnName("IsDeleted")
            .HasColumnType("BIT")
            .HasDefaultValue(false);

        builder.Ignore(e => e.DomainEvents);
    }
}
