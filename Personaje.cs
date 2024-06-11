public class Personaje{
//Características:
//velocidad;// 1 a 10
//destreza; //1 a 5
//fuerza;//1 a 10
//Nivel; //1 a 10
//Armadura; //1 a 10
//Salud://100

//Datos:
//Tipo;
//Nombre;
//Apodo;
//Fecha de Nacimiento;
//Edad; //entre 0 a 300

    private String nombre;
    private String apodo;
    private DateTime fecNac;
    private int edad;

    private int velocidad;
    private int destreza;
    private int fuerza;
    private int nivel;
    private int armadura;
    private float salud;

    public Nombre{ get => nombre; set => nombre=value;}
    public Apodo{ get => apodo ; set => apodo=value;}
    public FecNac{ get => fecNac ; set =>  fecNac=value;}
    public Edad{ get => edad ; set =>  edad=value;}
    public Velocidad{ get => velocidad ; set =>  velocidad=value;}
    public Destreza{ get => destreza ; set =>  destreza=value;}
    public Fuerza{ get => fuerza ; set =>  fuerza=value;}
    public Nivel{ get => nivel ; set =>  Nivel=value;}
    public Armadura{ get => armadura ; set =>  armadura=value;}
    public Salud{ get =>  salud; set =>  salud=value;}
    
    //{ get => duracion; set => duracion = value; }



}