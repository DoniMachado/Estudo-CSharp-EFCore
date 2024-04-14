namespace EFCore.Domain.Entities;

public class Heroi
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public IdentidadeSecreta IdentidadeSecreta { get; set; }
    public virtual ICollection<HeroiBatalha> Batalhas { get; set; }
    public virtual ICollection<Arma> Armas { get; set; }
}
