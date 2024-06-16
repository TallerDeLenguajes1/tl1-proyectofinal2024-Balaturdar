public class Categoria{
    private string nombre;
    private int pV;
    private int turno;
    private int hAtaque;
    private int hParada;
    private int hEsquiva;

    public Categoria(string nombre, int pV, int turno, int hAtaque, int hParada, int hEsquiva)
    {
        this.nombre = nombre;
        this.pV = pV;
        this.turno = turno;
        this.hAtaque = hAtaque;
        this.hParada = hParada;
        this.hEsquiva = hEsquiva;
    }

    public string Nombre { get => nombre;}
    public int PV { get => pV;}
    public int Turno { get => turno;}
    public int HAtaque { get => hAtaque;}
    public int HParada { get => hParada;}
    public int HEsquiva { get => hEsquiva;}
}