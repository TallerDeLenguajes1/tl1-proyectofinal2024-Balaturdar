using System.Text.Json.Serialization;

public class Categoria{
    private string nombre;
    private int pV;
    private int turno;
    private int hAtaque;
    private int hParada;
    private int hEsquiva;

    public Categoria(string nombre, int pV, int turno, int hAtaque, int hParada, int hEsquiva)
    {
        this.Nombre = nombre;
        this.PV = pV;
        this.Turno = turno;
        this.HAtaque = hAtaque;
        this.HParada = hParada;
        this.HEsquiva = hEsquiva;
    }

    [JsonPropertyName("Nombre")]
    public string Nombre { get => nombre; set => nombre = value; }
    [JsonPropertyName("PuntosVida")]
    public int PV { get => pV; set => pV = value; }
    [JsonPropertyName("Turno")]
    public int Turno { get => turno; set => turno = value; }
    [JsonPropertyName("Hataque")]
    public int HAtaque { get => hAtaque; set => hAtaque = value; }
    [JsonPropertyName("Hparada")]
    public int HParada { get => hParada; set => hParada = value; }
    [JsonPropertyName("Hesquiva")]
    public int HEsquiva { get => hEsquiva; set => hEsquiva = value; }
}