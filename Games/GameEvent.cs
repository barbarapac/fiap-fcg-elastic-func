using fiap_fcg_elastic_func._Shared;

public class GameEvent
{
    public string Tipo { get; set; }
    public int JogoId { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public DateTime DataEvento { get; set; } = DateTime.UtcNow;
}