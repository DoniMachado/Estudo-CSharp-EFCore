using EFCore.Domain.Core.Entities;

namespace EFCore.Domain.Core.Dtos;

public class HeroDto
{
    public long Id { get; set; }

    public string Name { get; set;  }  

    public static HeroDto ConvertFromEntity(Hero hero)
    {
        return new HeroDto()
        {
            Id = hero.Id,
            Name = hero.Name
        };      
    }

    public static IEnumerable<HeroDto> ConvertFromListEntity(IEnumerable<Hero> heroes)
        => heroes.Select(ConvertFromEntity);
}
