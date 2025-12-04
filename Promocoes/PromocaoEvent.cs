using fiap_fcg_elastic_func._Shared;

namespace fiap_fcg_elastic_func.Promocoes;

public class PromocaoEvent
{
    public int PromocaoId { get; set; }
    public decimal Desconto { get; set; }
    public string Tipo { get; set; }
    public List<JogoEventoDto> Jogos { get; set; } = new();
    public string Titulo { get; set; }
    public DateTime? DataFim { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime DataEvento { get; set; } = DateTime.UtcNow;
}

public class JogoEventoDto
{
    public int JogoId { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
}