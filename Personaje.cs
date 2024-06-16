public class Personaje{
    private string nombre;
    private string apodo;
    private DateTime fecNac;
    private int edad;
    private int fue;
    private int des;
    private int con;
    private int nivel;
    private int hAtaBase;
    private int hDefBase;
    private Categoria cat;
    private int pVida;
    private Arma arma;
    private Arma escudo;
    private Armadura armadura;
    private int Turno;
    private int hDefParada;
    private int hDefEsquiva;

    public string Nombre { get => nombre; set => nombre = value; }
    public string Apodo { get => apodo; set => apodo = value; }
    public DateTime FecNac { get => fecNac; set => fecNac = value; }
    public int Edad { get => edad; set => edad = value; }
    public int Fue { get => fue; set => fue = value; }
    public int Des { get => des; set => des = value; }
    public int Con { get => con; set => con = value; }
    public int Nivel { get => nivel; set => nivel = value; }
    public int HAtaBase { get => hAtaBase; set => hAtaBase = value; }
    public int HDefBase { get => hDefBase; set => hDefBase = value; }
    public Categoria Cat { get => cat; set => cat = value; }
    public int PVida { get => pVida; set => pVida = value; }
    public Arma Arma { get => arma; set => arma = value; }
    public Arma Escudo { get => escudo; set => escudo = value; }
    public Armadura Armadura { get => armadura; set => armadura = value; }
    public int Turno1 { get => Turno; set => Turno = value; }
    public int HDefParada { get => hDefParada; set => hDefParada = value; }
    public int HDefEsquiva { get => hDefEsquiva; set => hDefEsquiva = value; }
}