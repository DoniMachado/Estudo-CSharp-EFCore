using EFCore.Domain.Common.Interfaces;
using EFCore.Domain.Core.Entities;
using EFCore.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection;

namespace EFCore.Infrastructure.Context;

public class HeroContext : DbContext
{
    readonly IDomainEventHandler _domainEventHandler;

    public HeroContext(DbContextOptions<HeroContext> options, IDomainEventHandler domainEventHandler) : base(options)
    {
        _domainEventHandler = domainEventHandler;
    }

    public DbSet<Hero> Heroes { get; set; }
    public DbSet<Battle> Battles { get; set; }
    public DbSet<Weapon> Weapons { get; set; }
    public DbSet<SecretIdentity> SecretIdentities { get; set; }
    public DbSet<HeroBattle> HeroBattles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        ValueConverter<DateTimeOffset, DateTimeOffset> converter = new(d => d, d => TimeZoneInfo.ConvertTime(d, TimeZoneInfo.Local));

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            foreach(var property in entity.GetProperties())
            {
                if(property.ClrType == typeof(DateTimeOffset) || property.ClrType == typeof(DateTimeOffset?))
                    property.SetValueConverter(converter);
            }

            modelBuilder.AddIsDeletedBehavior(entity);

        }

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        ChangeTracker.OnSaveChangesCustom();
        var result = await base.SaveChangesAsync(cancellationToken);
        await ChangeTracker.OnDispatchEventsAsyncCustom(_domainEventHandler);
        return result;
    }
}
