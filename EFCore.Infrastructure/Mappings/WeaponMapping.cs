using EFCore.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Infrastructure.Mappings;

public class WeaponMapping : EntityMapping<Weapon>
{
    public override void Configure(EntityTypeBuilder<Weapon> builder)
    {        
        builder.Property(p => p.Name)
            .HasMaxLength(256)
            .IsRequired()
            .HasColumnType("VARCHAR(256)");

        builder.Property(p => p.Description)          
            .IsRequired(false)
            .HasColumnType("VARCHAR(MAX)");
            
        builder.HasOne(w => w.Hero)
            .WithMany(h => h.Weapons)
            .HasForeignKey(w => w.HeroId)
            .IsRequired();

        builder.ToTable("Weapon");

        base.Configure(builder);
    }
}
