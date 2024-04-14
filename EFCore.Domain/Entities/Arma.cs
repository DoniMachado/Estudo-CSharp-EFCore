namespace EFCore.Domain.Entities;

public class Arma
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public int HeroiId { get; set; }

    public Heroi Heroi { get; set; }
}
