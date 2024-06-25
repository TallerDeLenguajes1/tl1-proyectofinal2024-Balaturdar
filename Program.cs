
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
            if (nombre == null && nombre.Count() == 0)
            {
                Console.Clear();
                Console.WriteLine("** por favor,ingrese un nombre para guardar la partida **");
            }
        } while (nombre == null && nombre.Count() == 0);
        
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


static void combate(Personaje Enemigo, Personaje Jugador){

}

static int Tirada(){
    int tirada = new Random().Next(1,101);
    tirada += (tirada > 90)? new Random().Next(1,101) : 0;//la accion sale excepcionalmente bien
    tirada -= (tirada < 5)? new Random().Next(1,101) : 0;//la accion sale excepcionalmente mal
    return tirada;
}