using System.Text.Json.Serialization;

public class Arma{
    private string nombre;
    private int danio;
    private int turno;
    private int fueR;
    private string crt1;
    private string? crt2;
    private string tArma;
    private string especial;

    [JsonPropertyName("Nombre")]
    public string Nombre { get; set; }
    [JsonPropertyName("Danio")]
    public int Danio { get; set; }
    [JsonPropertyName("Turno")]
    public int Turno { get; set; }
    [JsonPropertyName("FueR")]
    public int FueR { get; set; }
    [JsonPropertyName("TAtaque")]
    public string TAtaque { get; set; }
    [JsonPropertyName("TAtaque 2")]
    public string TAtaque2 { get; set; }
    [JsonPropertyName("Tarma")]
    public string Tarma { get; set; }
    [JsonPropertyName("Especial")]
    public string Especial { get; set; }

    public Arma(string nombre, int danio, int turno, int fueR, string crt1, string crt2, string tArma, string especial)
    {
        this.nombre = nombre;
        this.danio = danio;
        this.turno = turno;
        this.fueR = fueR;
        this.crt1 = crt1;
        this.crt2 = crt2;
        this.tArma = tArma;
        this.especial = especial;
    }

}

