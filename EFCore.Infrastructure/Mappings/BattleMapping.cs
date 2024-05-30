using EFCore.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Infrastructure.Mappings;

public class BattleMapping : EntityMapping<Battle>
{
    public override void Configure(EntityTypeBuilder<Battle> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(p => p.Description)                
            .IsRequired();

        builder.Property(p => p.InitialDate)          
          .IsRequired();

        builder.Property(p => p.FinalDate)          
          .IsRequired(false);

        builder.HasMany(b => b.Heroes)
            .WithOne(hb => hb.Battle);


        builder.ToTable("Battle");
    }
}
