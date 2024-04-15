using EFCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Repository.Mappings;

public class HeroBattleMapping : EntityMapping<HeroBattle>
{
    public override void Configure(EntityTypeBuilder<HeroBattle> builder)
    {
        base.Configure(builder);

        builder.HasOne(hb => hb.Hero)
            .WithMany(h => h.Battles)
            .HasForeignKey(hb => hb.HeroId)
            .IsRequired();

        builder.HasOne(hb => hb.Battle)
         .WithMany(h => h.Heroes)
         .HasForeignKey(hb => hb.BattleId)
         .IsRequired();

        builder.HasIndex(hb => new { hb.HeroId ,hb.BattleId })
            .IsUnique()
            .HasDatabaseName("UX_HeroBattle_HeroId_BattleId");


        builder.ToTable("HeroBattle");
    }
}
