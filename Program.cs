
using Microsoft.VisualBasic;

int opc;
bool aux;
Console.WriteLine("***********************");
Console.WriteLine("** 1-Nueva Partida   **");
Console.WriteLine("** 2-Cargar Partida  **");
Console.WriteLine("** 3-Ganadores       **");
Console.WriteLine("** 4-Opciones        **");
Console.WriteLine("** 5-Salir           **");
Console.WriteLine("***********************");
Console.WriteLine("i");
do
{
    aux = int.TryParse(Console.ReadLine(), out opc);
    if(!aux){
        Console.WriteLine("debe ingresar un numero para seleccionar una opcion");
    }
    Console.Clear();
} while (!aux);

string rutaDirectorioPartidaGuardada = Path.GetFullPath(@"../../../partidasGuardadas/");

if (!Directory.Exists(rutaDirectorioPartidaGuardada))
{
    Directory.CreateDirectory(rutaDirectorioPartidaGuardada);
}
List<string> ListadoDePartidas = Directory.GetDirectories(rutaDirectorioPartidaGuardada).ToList();
int contador = 0 ;
string nombre;
switch (opc)
{
    case 1://Nueva partida
        Console.WriteLine("** Ingrese un nombre para la partida **");
        
        do
        {
            nombre = Console.ReadLine();
            if (nombre == null || nombre.Count() == 0)
            {
                Console.Clear();
                Console.WriteLine("** por favor,ingrese un nombre para guardar la partida **");
            }
        } while (nombre == null || nombre.Count() == 0);
        
        foreach (string partida in ListadoDePartidas)
        {
            if(partida.Contains(nombre)){
                contador++;
            }
        }
        contador++;
        if(contador > 0){
            nombre.Concat(""+contador);
        }
        FabricaPersonajes fabrica = new FabricaPersonajes();
        PersonajesJson.GuardarEnemigos(fabrica.CrearPersonajes(10),nombre);
        PersonajesJson.GuardarPersonaje(fabrica.CrearPersonaje(1),nombre);

        //EmpezarPartida
        
        break;
    case 2://cargar partida
        int indice = 1;
        foreach (string partida in ListadoDePartidas)
        {
            Console.WriteLine(indice + "- " + partida);
            indice++;
        }
        do
        {
            aux = int.TryParse(Console.ReadLine(), out indice);
            if(!aux){
                Console.WriteLine("debe ingresar un numero para seleccionar una partida");
            }
            if(indice > ListadoDePartidas.Count() || indice < 0){
                Console.WriteLine("ingrese una opcion valida");;
            }
        } while (!aux || indice > ListadoDePartidas.Count() || indice < 0);
        Console.Clear();
        List<Personaje>? Enemigos = PersonajesJson.LeerEnemigos(ListadoDePartidas.ElementAt(indice));
        Personaje? Jugador = PersonajesJson.LeerJugador(ListadoDePartidas.ElementAt(indice));
        if(Enemigos == null || Jugador == null){
            Console.WriteLine("no se encontraron personajes en esta partida");
            break;
        }
        //Continuar Partida
        
        break;
    case 3://Ganadores
        break;
    case 4://Opciones
        Console.WriteLine("no hay opciones XD");
        break;
    case 5://salir
        break;
    case 6:
        break;
}


static void combate(List<Personaje> Enemigos, Personaje Jugador){
    var Enemigo = Enemigos.ElementAt(0);
    Personaje primero, segundo;

    if (Enemigo.Turno + Tirada() > Jugador.Turno + Tirada())
    {
        primero = Enemigo;
        segundo = Jugador;
    }else{
        primero = Jugador;
        segundo = Enemigo;
    }
    do
    {
        Pelea(primero, segundo, false);
        if(Jugador.PVida<=0){
            //fin partida
        }
        if(Enemigo.PVida<=0){
            //termina pelea
            break;
        }
        Pelea(segundo,primero, false);
        if(Jugador.PVida<=0){
            //fin partida
        }
        if(Enemigo.PVida<=0){
            //termina pelea
            break;
        }
    } while (true);
    
    
    
    
    //guardar
    if(Enemigos.Count()>0){
        Console.WriteLine("¿Seguir Peleando?");
        //si / no breack;
        if(Console.ReadLine() != "S" && Console.ReadLine() != "s"){
            //break;
        }
    }else{
        //agregar jugador a ganadores,
        //mensaje
        //info jugador
        //borrar partida
        //break;
    }

}


static int Tirada(){
    int tirada = new Random().Next(1,101);
    tirada += (tirada > 90)? new Random().Next(1,101) : 0;//la accion sale excepcionalmente bien
    tirada -= (tirada < 5)? new Random().Next(1,101) : 0;//la accion sale excepcionalmente mal
    return tirada;
}

static void Pelea(Personaje atacante, Personaje defensor , bool contraataque)    
{
    var tiradaAtaque = Tirada();
    var tiradaDefensa = Tirada();
    var ataqueFinal = atacante.Ataque() +tiradaAtaque;
    var defensaFinal = (defensor.HDefEsquiva > defensor.HDefParada) ? defensor.HDefEsquiva : defensor.HDefParada;
    defensaFinal += tiradaDefensa;
    var aux = ataqueFinal - defensaFinal;
    Console.WriteLine($"Habilidad de Ataque: {ataqueFinal}\nHabilidad de Defensa: {defensaFinal}");
    switch (aux)
    {
        case < -50://contraataque
            if (!contraataque)
            {
                Console.WriteLine($"{atacante.Nombre} falla el ataque, ¡{defensor.Nombre} puede contraatacar!");
                Pelea(defensor, atacante, true);
                break;
            }
            Console.WriteLine($"{atacante.Nombre} falla el contraataque");
            break;
        case <= 0://ataque rechazado por el defensor
            Console.WriteLine($"{defensor.Nombre}, logra rechazar el ataque de {atacante.Nombre}");
            Console.WriteLine($"le quedan {defensor.PVida} puntos de vida");
            break;
        case < 10://impacta pero no logra hacer daño
            Console.WriteLine($"¡¡¡IMPACTO!!!\nEl ataque apenas logra superar la defensa de {defensor.Nombre}, pero no lo suficiente para hacer daño");
            Console.WriteLine($"le quedan {defensor.PVida} puntos de vida");
            break;
        case >= 10://impacta
            var danio = atacante.Arma.Danio + (int)Math.Floor((double)(aux / 10));
            if (danio - defensor.Absorcion(atacante.Arma.TArma) > 10)
            {
                //mensaje impacta y hace daño
                danio -= defensor.Absorcion(atacante.Arma.TArma);
                defensor.PVida -= danio;
                Console.WriteLine($"¡¡¡IMPACTO!!!\nEl ataque atraviesa la armadura de {defensor.Nombre} y realiza {danio} puntos de daño");
                Console.WriteLine($"le quedan {defensor.PVida} puntos de vida");
                
            }
            else
            {
                //mensaje impacta y pero su armadura resiste
                Console.WriteLine($"¡¡¡IMPACTO!!!\nEl ataque supera la defensa de {defensor.Nombre}, pero no logra atravezar su armadura");
                Console.WriteLine($"le quedan {defensor.PVida} puntos de vida");
            }
            break;
    }
}