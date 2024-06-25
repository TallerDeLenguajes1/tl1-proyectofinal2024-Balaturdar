using System.Text.Json.Serialization;

public class Arma{
    private string nombre;
    private int danio;
    private int turno;
    private int fueR;
    private string tAtaque;
    private string? tAtaque2;
    private string tArma;
    private string especial;


    [JsonPropertyName("Nombre")]
    public string Nombre { get => nombre; set => nombre = value; }
    [JsonPropertyName("Danio")]
    public int Danio { get => danio; set => danio = value; }
    [JsonPropertyName("Turno")]
    public int Turno { get => turno; set => turno = value; }
    [JsonPropertyName("FueR")]
    public int FueR { get => fueR; set => fueR = value; }
    [JsonPropertyName("TAtaque")]
    public string TAtaque { get => tAtaque; set => tAtaque = value; }
    [JsonPropertyName("TAtaque2")]
    public string? TAtaque2 { get => tAtaque2; set => tAtaque2 = value; }
    [JsonPropertyName("Tarma")]
    public string TArma { get => tArma; set => tArma = value; }
    [JsonPropertyName("Especial")]
    public string Especial { get => especial; set => especial = value; }
}

