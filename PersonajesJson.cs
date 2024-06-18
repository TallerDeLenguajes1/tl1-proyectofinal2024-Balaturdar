using System.Text.Json;

public class PersonajeJson{

    string rutaDirectorioPJ = Path.GetFullPath(@"../../../personajes/");
    string rutaDirectorioGanadores = Path.GetFullPath(@"../../../ganadores/");

    public void GuardarPersonajes(List<Personaje> personajes, string nombreArchivo){
        /*Crear un método llamado GuardarPersonajes que reciba una lista de personajes, el
        nombre del archivo y lo guarde en formato Json.*/
        var miGestor = new GestorJson();
        string personajesJson = JsonSerializer.Serialize(personajes);
        if (!Directory.Exists(rutaDirectorioPJ))
        {
            Directory.CreateDirectory(rutaDirectorioPJ);
        }
        miGestor.GuardarArchivoTexto(rutaDirectorioPJ + nombreArchivo, personajesJson);

    }

    public List<Personaje>? LeerPersonajes(String nombreArchivo){
        /*
        Crear un método llamado LeerPersonajes que reciba un nombre de archivo y retorne la
        lista de personajes incluidos en el son.
        */
        if(!Existe(nombreArchivo)){
            return null;
        }
        var miGestor = new GestorJson();
        var personajesJson = miGestor.AbrirArchivoTexto(rutaDirectorioPJ + nombreArchivo);
        var listadoPersonajes = JsonSerializer.Deserialize<List<Personaje>>(personajesJson);
        return listadoPersonajes;
    }

    public bool Existe(string nombreArchivo){
        /*4) Crear un método llamado Existe que reciba un nombre de archivo y que retorne un True
        si existe y tiene datos o False en caso contrario.*/
        return File.Exists(rutaDirectorioPJ + nombreArchivo);
    }

    public void HistorialJson(){
        //Armar una clase llamada HistorialJson para guardar y leer desde un archivo Json
    }

    public void GuardarGanador(Personaje ganador, string informacionRelevante, string nombreArchivo){
        /*
            Crear un método llamado GuardarGanador que reciba el personaje ganador e
            información relevante de las partidas, el nombre del archivo y lo guarde en formato Json.  
        */
        var miGestor = new GestorJson();
        string personajeJson = JsonSerializer.Serialize(ganador);
        if (!Directory.Exists(rutaDirectorioGanadores))
        {
            Directory.CreateDirectory(rutaDirectorioGanadores);
        }
        miGestor.GuardarArchivoTexto(rutaDirectorioGanadores + nombreArchivo, personajeJson);
    }

    public List<Personaje>? LeerGanadores(string nombreArchivo){
        /*
            Crear un método llamado LeerGanadores que reciba un nombre de archivo y retorne la
            lista de personajes ganadores e información relevante incluidos en el Json.
        */
        string[] ganadores = Directory.GetFiles(rutaDirectorioGanadores);
        if (ganadores != null && ganadores.Count() > 0)
        {
            var miGestor = new GestorJson();
            List<Personaje>? listadoGanadores = new List<Personaje>();
            foreach (var ganador in ganadores)
            {
                
                var ganadorJson = miGestor.AbrirArchivoTexto(rutaDirectorioGanadores + ganador);
                listadoGanadores.Add(JsonSerializer.Deserialize<Personaje>(ganadorJson));
            }
            return listadoGanadores;
        }else{
            return null;
        }
    }

}