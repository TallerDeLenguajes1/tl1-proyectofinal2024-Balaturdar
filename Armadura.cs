using System.Text.Json.Serialization;

public class Armadura{
    private string nombre;
    private int fil;
    private int con;
    private int pen;
    private int cal;
    private int ele;
    private int fri;
    private int ene;
    private int penalizador;

    public Armadura(string nombre, int fil, int con, int pen, int cal, int ele, int fri, int ene, int penalizador)
    {
        this.Nombre = nombre;
        this.Fil = fil;
        this.Con = con;
        this.Pen = pen;
        this.Cal = cal;
        this.Ele = ele;
        this.Fri = fri;
        this.Ene = ene;
        this.Penalizador = penalizador;
    }
    [JsonPropertyName("nombre")]
    public string Nombre { get => nombre; set => nombre = value; }
    [JsonPropertyName("FIL")]
    public int Fil { get => fil; set => fil = value; }
    [JsonPropertyName("CON")]
    public int Con { get => con; set => con = value; }
    [JsonPropertyName("PEN")]
    public int Pen { get => pen; set => pen = value; }
    [JsonPropertyName("CAL")]
    public int Cal { get => cal; set => cal = value; }
    [JsonPropertyName("ELE")]
    public int Ele { get => ele; set => ele = value; }
    [JsonPropertyName("FRI")]
    public int Fri { get => fri; set => fri = value; }
    [JsonPropertyName("ENE")]
    public int Ene { get => ene; set => ene = value; }
    [JsonPropertyName("Penalizador")]
    public int Penalizador { get => penalizador; set => penalizador = value; }
}
