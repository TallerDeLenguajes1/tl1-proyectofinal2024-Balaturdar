using System.Text.Json;

public static class PersonajesJson{

    private static string rutaPJ = Path.GetFullPath(@"../../../personajes/");
    private static string rutaPartidaGuardada = Path.GetFullPath(@"../../../partidasGuardadas/");
    private static string rutaGanadores = Path.GetFullPath(@"../../../ganadores/");

    public static void GuardarEnemigos(List<Personaje> personajes, string nombrePartida){
        var miGestor = new GestorJson();
        string personajesJson = JsonSerializer.Serialize(personajes);
        if (!Directory.Exists(rutaPartidaGuardada + nombrePartida))
        {
            Directory.CreateDirectory(rutaPartidaGuardada + nombrePartida);
        }
        
        miGestor.GuardarArchivoTexto(rutaPartidaGuardada + nombrePartida + "/enemigos.json", personajesJson);
    }

    public static void GuardarPersonaje(Personaje jugador, string nombrePartida){
        var miGestor = new GestorJson();
        string jugadorJson = JsonSerializer.Serialize(jugador);
        if (!Directory.Exists(rutaPartidaGuardada + nombrePartida))
        {
            Directory.CreateDirectory(rutaPartidaGuardada + nombrePartida);
        }
        
        miGestor.GuardarArchivoTexto(rutaPartidaGuardada + nombrePartida + "/jugador.json", jugadorJson);
    }

    public static List<Personaje>? LeerEnemigos(string nombrePartida){
        if(!Existe(rutaPartidaGuardada + nombrePartida)){
            return null;
        }
        var miGestor = new GestorJson();
        var personajesJson = miGestor.AbrirArchivoTexto(rutaPartidaGuardada + nombrePartida + "/enemigos.json");
        var listadoPersonajes = JsonSerializer.Deserialize<List<Personaje>>(personajesJson);
        return listadoPersonajes;
    }
    public static Personaje? LeerJugador(string nombrePartida){
        if(!Existe(rutaPartidaGuardada + nombrePartida)){
            return null;
        }
        var miGestor = new GestorJson();
        var jugadorJson = miGestor.AbrirArchivoTexto(rutaPartidaGuardada + nombrePartida + "/enemigos.json");
        var jugador = JsonSerializer.Deserialize<Personaje>(jugadorJson);
        return jugador;
    }

    public static bool Existe(string nombreArchivo){
        return File.Exists(rutaPJ + nombreArchivo);
    }


    public static void HistorialJson(){
        //Armar una clase llamada HistorialJson para guardar y leer desde un archivo Json
    }

    public static void GuardarGanador(Personaje ganador, string informacionRelevante, string nombreArchivo){
        /*
            Crear un método llamado GuardarGanador que reciba el personaje ganador e
            información relevante de las partidas, el nombre del archivo y lo guarde en formato Json.  
        */
        var miGestor = new GestorJson();
        string personajeJson = JsonSerializer.Serialize(ganador);
        if (!Directory.Exists(rutaGanadores))
        {
            Directory.CreateDirectory(rutaGanadores);
        }
        miGestor.GuardarArchivoTexto(rutaGanadores + nombreArchivo, personajeJson);
    }

    public static List<Personaje>? LeerGanadores(string nombreArchivo){
        /*
            Crear un método llamado LeerGanadores que reciba un nombre de archivo y retorne la
            lista de personajes ganadores e información relevante incluidos en el Json.
        */
        string[] ganadores = Directory.GetFiles(rutaGanadores);
        if (ganadores != null && ganadores.Count() > 0)
        {
            var miGestor = new GestorJson();
            List<Personaje>? listadoGanadores = new List<Personaje>();
            foreach (var ganador in ganadores)
            {
                
                var ganadorJson = miGestor.AbrirArchivoTexto(rutaGanadores + ganador);
                listadoGanadores.Add(JsonSerializer.Deserialize<Personaje>(ganadorJson));
            }
            return listadoGanadores;
        }else{
            return null;
        }
    }

}