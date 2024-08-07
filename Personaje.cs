using System.Reflection.Metadata.Ecma335;

public class Personaje
{

    private string nombre;
    private string apodo;
    private DateTime fecNac;
    private int edad;
    private int fue;
    private int des;
    private int agi;
    private int con;
    private int nivel;
    private int hAtaBase;
    private int hDefBase;
    private Categoria cat;
    private int pVida;
    private Arma arma;
    private Arma escudo;
    private Armadura armadura;
    private int turno;
    private int hDefParada;
    private int hDefEsquiva;

    public Personaje(string nombre, string apodo, DateTime fecNac, int edad, int fue, int des, int agi, int con, int nivel, int hAtaBase, int hDefBase, Categoria cat, Arma arma, Armadura armadura, Arma escudo)
    {
        this.nombre = nombre;
        this.apodo = apodo;
        this.fecNac = fecNac;
        this.edad = edad;
        this.fue = fue;
        this.des = des;
        this.agi = agi;
        this.con = con;
        this.nivel = nivel;
        this.hAtaBase = hAtaBase;
        this.hDefBase = hDefBase;
        this.cat = cat;

        this.pVida = 20 + Con * 10 + BonoAtributo(Con) + cat.PV * Nivel;
        this.arma = arma;
        this.armadura = armadura;
        this.escudo = escudo;
        turno = 20 + Agi + Des + (Cat.Turno * Nivel) + arma.Turno + escudo.Turno + armadura.Penalizador;
        this.hDefParada = new Random().Next(0, HDefBase + 1);
        this.hDefEsquiva = HDefBase - HDefParada;

        HDefParada += BonoAtributo(Des) + Cat.HParada*Nivel + int.Parse(paradaYEsquivaEscudo(escudo)[0]);
        HDefEsquiva += BonoAtributo(Agi) + Cat.HEsquiva*Nivel + int.Parse(paradaYEsquivaEscudo(escudo)[1]);
    }

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
    public int Turno { get => turno; set => turno = value; }
    public int HDefParada { get => hDefParada; set => hDefParada = value; }
    public int HDefEsquiva { get => hDefEsquiva; set => hDefEsquiva = value; }
    public int Agi { get => agi; set => agi = value; }

    public int BonoAtributo(int atributo)
    {
        /*
            1 = -30
            2 = -20
            3 = -10
            4 = -5
            5 = 0
            6 = 5
            7 = 5
            8 = 10
            9 = 10
            10 = 15
        */
        if (atributo <= 3)
        {
            return 40 - 10 * atributo;
        }
        if (atributo == 4)
        {
            return -5;
        }
        if (atributo == 5)
        {
            return 0;
        }
        if (atributo % 2 == 0)
        {
            return (atributo - 4) / 2 * 5;
        }
        else
        {
            return (atributo - 5) / 2 * 5;
        }
    }

    public int CantAciones()
    {
        switch (Agi + Des)
        {
            case <= 10:
                return 1;
            case <= 14:
                return 2;
            case <= 19:
                return 3;
            case <= 22:
                return 4;
            case <= 25:
                return 5;
            case <= 28:
                return 6;
            case <= 31:
                return 8;
            case >= 32:
                return 10;
        }
    }
    public void SubirNivel()
    {
        Nivel++;
        HAtaBase += Cat.HAtaque;
        PVida += Cat.PV;
        HDefParada += Cat.HParada;
        HDefEsquiva += Cat.HEsquiva;
        turno += Cat.Turno;
    }

    private string[] paradaYEsquivaEscudo(Arma escudo){
        return escudo.Especial.Split(" / ");
    }

    public int Ataque(){
        return HAtaBase + BonoAtributo(Des);
    }

    public int Absorcion(string tipoAtaque){
        int ptoarmadura = 0;
        switch (tipoAtaque)
        {
            case "FIL":
                ptoarmadura = Armadura.Fil;
                break;
            case "CON":
                ptoarmadura = Armadura.Con;
                break;
            case "PEN":
                ptoarmadura = Armadura.Pen;
                break;
            case "CAL":
                ptoarmadura = Armadura.Cal;
                break;
            case "ELE":
                ptoarmadura = Armadura.Ele;
                break;
            case "FRI":
                ptoarmadura = Armadura.Fri;
                break;
            case "ENE":
                ptoarmadura = Armadura.Ene;
                break;
        }

        return 20+ptoarmadura;
    }

    public string InfoPj(){

        return "Name: " + Nombre + 
               "\nNickname: " + Apodo + 
               "\nCharacteristics: " +
               "\nStrong: " + Fue +
               "\nDextry: " + Des +
               "\nAgility: " + Agi +
               "\nConstitution: " + Con +
               "\nlevel: " + Nivel +
               "\nClass: " + Cat +
               "\nweapon: " + Arma +
               "\nShield: " + Escudo +
               "\nArmor: " + Armadura;
    }
}