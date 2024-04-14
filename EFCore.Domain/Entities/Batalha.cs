namespace EFCore.Domain.Entities;

public class Batalha
{    
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public DateTime DtInicio { get; set; }
    public DateTime DtFim { get; set; }

    public virtual ICollection<HeroiBatalha> Herois { get; set; }
}
