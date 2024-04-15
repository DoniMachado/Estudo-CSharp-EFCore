using EFCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Repository.Mappings;

public class WeaponMapping : EntityMapping<Weapon>
{
    public override void Configure(EntityTypeBuilder<Weapon> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(p => p.Description)          
            .IsRequired(false);

        builder.Property(p => p.HeroId)            
            .IsRequired();

        builder.HasOne(w => w.Hero)
            .WithMany(h => h.Weapons)
            .HasForeignKey(w => w.HeroId)
            .IsRequired();

        builder.ToTable("Weapon");
    }
}
