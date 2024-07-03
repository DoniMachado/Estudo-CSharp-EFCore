using EFCore.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Infrastructure.Mappings;

public class BattleMapping : EntityMapping<Battle>
{
    public override void Configure(EntityTypeBuilder<Battle> builder)
    {      
        builder.Property(p => p.Name)
            .HasMaxLength(256)
            .IsRequired()
            .HasColumnType("VARCHAR(256)");

        builder.Property(p => p.Description)                
            .IsRequired()
            .HasColumnType("VARCHAR(MAX)");

        builder.Property(p => p.InitialDate)
          .IsRequired()
          .HasColumnType("DATETIMEOFFSET");

        builder.Property(p => p.FinalDate)
          .IsRequired(false)
          .HasColumnType("DATETIMEOFFSET");

        builder.HasMany(b => b.Heroes)
            .WithOne(hb => hb.Battle);


        builder.ToTable("Battle");

        base.Configure(builder);
    }
}
