﻿using EFCore.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Infrastructure.Mappings;

public class HeroMapping: EntityMapping<Hero>
{
    public override void Configure(EntityTypeBuilder<Hero> builder)
    {       
        builder.Property(p => p.Name)
            .HasMaxLength(256)
            .IsRequired()
            .HasColumnType("VARCHAR(256)");

        builder.HasOne(h => h.SecretIdentity)
            .WithOne(s => s.Hero);

        builder.HasMany(h => h.Battles)
            .WithOne(hb => hb.Hero);

        builder.HasMany(h => h.Weapons)
            .WithOne(w => w.Hero);

        builder.ToTable("Hero");

        base.Configure(builder);
    }
}
