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
        this.nombre = nombre;
        this.fil = fil;
        this.con = con;
        this.pen = pen;
        this.cal = cal;
        this.ele = ele;
        this.fri = fri;
        this.ene = ene;
        this.penalizador = penalizador;
    }

    public string Nombre { get => nombre;}
    public int Fil { get => fil;}
    public int Con { get => con;}
    public int Pen { get => pen;}
    public int Cal { get => cal;}
    public int Ele { get => ele;}
    public int Fri { get => fri;}
    public int Ene { get => ene;}
    public int Penalizador { get => penalizador;}
}