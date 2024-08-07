﻿using EFCore.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Infrastructure.Mappings;

public class SecretIdentityMapping : EntityMapping<SecretIdentity>
{
    public override void Configure(EntityTypeBuilder<SecretIdentity> builder)
    {       
        builder.Property(p => p.Name)
            .HasMaxLength(256)
            .IsRequired()
            .HasColumnType("VARCHAR(256)");

        builder.HasOne(s => s.Hero)
            .WithOne(h => h.SecretIdentity)
            .HasForeignKey<SecretIdentity>(s => s.HeroId)
            .IsRequired();

        builder.ToTable("SecretIdentity");

        base.Configure(builder);
    }
}
