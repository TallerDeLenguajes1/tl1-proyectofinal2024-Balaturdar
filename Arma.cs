public class Arma{
    private string nombre;
    private int danio;
    private int turno;
    private int fueR;
    private string crt1;
    private string crt2;
    private string tArma;
    private string especial;

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

    public string Nombre { get => nombre; set => nombre = value; }
    public int Danio { get => danio; set => danio = value; }
    public int Turno { get => turno; set => turno = value; }
    public int FueR { get => fueR; set => fueR = value; }
    public string Crt1 { get => crt1; set => crt1 = value; }
    public string Crt2 { get => crt2; set => crt2 = value; }
    public string TArma { get => tArma; set => tArma = value; }
    public string Especial { get => especial; set => especial = value; }
}