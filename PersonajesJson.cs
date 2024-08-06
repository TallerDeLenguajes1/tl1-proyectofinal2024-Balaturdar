using System.ComponentModel;
using System.Text.Json;

public static class PersonajesJson{

    private static string rutaPJ = Path.GetFullPath(@"../tl1-proyectofinal2024-Balaturdar/personajes/");
    private static string rutaPartidaGuardada = Path.GetFullPath(@"../tl1-proyectofinal2024-Balaturdar/partidasGuardadas/");
    private static string rutaGanadores = Path.GetFullPath(@"../tl1-proyectofinal2024-Balaturdar/ganadores/");
    public static void GuardarEnemigos(List<Personaje> personajes, string nombrePartida){
        string personajesJson = JsonSerializer.Serialize(personajes);
        if (!Directory.Exists(rutaPartidaGuardada + nombrePartida))
        {
            Directory.CreateDirectory(rutaPartidaGuardada + nombrePartida);
        }
        
        GestorJson.GuardarArchivoTexto(rutaPartidaGuardada + nombrePartida + "/enemigos.json", personajesJson);
    }

    public static void GuardarPersonaje(Personaje jugador, string nombrePartida){
        string jugadorJson = JsonSerializer.Serialize(jugador);
        if (!Directory.Exists(rutaPartidaGuardada + nombrePartida))
        {
            Directory.CreateDirectory(rutaPartidaGuardada + nombrePartida);
        }
        
        GestorJson.GuardarArchivoTexto(rutaPartidaGuardada + nombrePartida + "/jugador.json", jugadorJson);
    }

    public static List<Personaje>? LeerEnemigos(string nombrePartida){
        //var carpetapartidaguardada = Path.GetFullPath(rutaPartidaGuardada + nombrePartida + "/enemigos.json");
        //if(Existe(carpetapartidaguardada)){
        //    return null;
        //}
        
        var personajesJson = GestorJson.AbrirArchivoTexto(rutaPartidaGuardada + nombrePartida + "/enemigos.json");
        var listadoPersonajes = JsonSerializer.Deserialize<List<Personaje>>(personajesJson);
        return listadoPersonajes;
    }
    public static Personaje? LeerJugador(string nombrePartida){
        //if(!Existe(rutaPartidaGuardada + nombrePartida)){
        //    return null;
        //}
        var jugadorJson = GestorJson.AbrirArchivoTexto(rutaPartidaGuardada + nombrePartida + "/Jugador.json");
        var jugador = JsonSerializer.Deserialize<Personaje>(jugadorJson);
        return jugador;
    }

    public static bool Existe(string nombreArchivo){
        return File.Exists(rutaPJ + nombreArchivo);
    }


    public static void HistorialJson(){
        //Armar una clase llamada HistorialJson para guardar y leer desde un archivo Json
    }

    public static void GuardarGanador(Personaje ganador){
        /*
            Crear un método llamado GuardarGanador que reciba el personaje ganador e
            información relevante de las partidas, el nombre del archivo y lo guarde en formato Json.  
        */
        string personajeJson = JsonSerializer.Serialize(ganador);
        if (!Directory.Exists(rutaGanadores))
        {
            Directory.CreateDirectory(rutaGanadores);
        }
        string nombreArchivo = ganador.Nombre + "json";
        GestorJson.GuardarArchivoTexto(rutaGanadores + nombreArchivo, personajeJson);
    }

    public static List<Personaje>? LeerGanadores(string nombreArchivo){
        /*
            Crear un método llamado LeerGanadores que reciba un nombre de archivo y retorne la
            lista de personajes ganadores e información relevante incluidos en el Json.
        */
        string[] ganadores = Directory.GetFiles(rutaGanadores);
        if (ganadores != null && ganadores.Count() > 0)
        {
            
            List<Personaje> listadoGanadores = new List<Personaje>();
            foreach (var ganador in ganadores)
            {
                
                var ganadorJson = GestorJson.AbrirArchivoTexto(rutaGanadores + ganador);
                if (ganadorJson != null){
                    listadoGanadores.Add(JsonSerializer.Deserialize<Personaje>(ganadorJson));
                }
            }
            return listadoGanadores;
        }else{
            return null;
        }
    }

    public static string CrearNuevaPartida(){
        string? nombre;

        if (!Directory.Exists(rutaPartidaGuardada))
        {
            Directory.CreateDirectory(rutaPartidaGuardada);
        }
        

        List<string> ListadoDePartidas = Directory.GetDirectories(rutaPartidaGuardada).ToList();

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
        int contador=0;
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

        Directory.CreateDirectory(rutaPartidaGuardada + nombre);


        return nombre;
    }

    public static void BorrarPartida(string nombrePartida){
        if (Directory.Exists(rutaPartidaGuardada+nombrePartida))
        {
            // Eliminar la carpeta y todo su contenido de forma recursiva
            Directory.Delete(rutaPartidaGuardada+nombrePartida, true);
        }            
    }

    public static string cargarPartida(Personaje? Jugador,List<Personaje>? Enemigos){
        bool aux;
        int indice = 1;

        List<string> ListadoDePartidas = Directory.GetDirectories(rutaPartidaGuardada).ToList();

        if(ListadoDePartidas == null || ListadoDePartidas.Count()< 1){
            Console.WriteLine("no saved games found");
            Console.WriteLine("Press any key to exit... ");
            Console.ReadKey();
            return null;
        }
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
                Console.WriteLine("ingrese una opcion valida");
            }
        } while (!aux || indice > ListadoDePartidas.Count() || indice < 0);
        Console.Clear();
        string nombrePartida = ListadoDePartidas.ElementAt(indice-1);
        return nombrePartida;
    }

    public static List<string> ListaDePartidasGuardadas(){
        if (!Directory.Exists(rutaPartidaGuardada))
        {
            Directory.CreateDirectory(rutaPartidaGuardada);
        }
        return Directory.GetDirectories(rutaPartidaGuardada).ToList();
    }
    
}