using EFCore.Domain.Core.Entities;

namespace EFCore.WebAPI.Models;

public class HeroResponseModel
{
    public long Id { get; set; }

    public string Name { get; set;  }  

    public static HeroResponseModel ConvertFromEntity(Hero hero)
    {
        return new HeroResponseModel()
        {
            Id = hero.Id,
            Name = hero.Name
        };      
    }

    public static IEnumerable<HeroResponseModel> ConvertFromListEntity(IEnumerable<Hero> heroes)
        => heroes.Select(ConvertFromEntity);
}
