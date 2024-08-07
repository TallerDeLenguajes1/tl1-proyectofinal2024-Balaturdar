using System.Text.Json;

public class FabricaPersonajes
{
    List<Armadura>? armaduras;
    List<Arma>? armas;
    List<Arma>? escudos;
    List<Categoria>? categorias;
    string[] nombres;
    string[] apodos;

    public FabricaPersonajes()
    {
        string carpetaArchivos = @"../tl1-proyectofinal2024-Balaturdar/archivos/";
        string jsonDocument = GestorJson.AbrirArchivoTexto(Path.GetFullPath(carpetaArchivos + "ArmadurasAnima.json"));

        this.armaduras = JsonSerializer.Deserialize<List<Armadura>>(jsonDocument);

        jsonDocument = GestorJson.AbrirArchivoTexto(Path.GetFullPath(carpetaArchivos + "ArmasAnima.json"));
        this.armas = JsonSerializer.Deserialize<List<Arma>>(jsonDocument);

        jsonDocument = GestorJson.AbrirArchivoTexto(Path.GetFullPath(carpetaArchivos + "CategoriasAnima.json"));
        this.categorias = JsonSerializer.Deserialize<List<Categoria>>(jsonDocument);

        jsonDocument = GestorJson.AbrirArchivoTexto(Path.GetFullPath(carpetaArchivos + "EscudosAnima.json"));
        this.escudos = JsonSerializer.Deserialize<List<Arma>>(jsonDocument);

        nombres = [
    "Alaric", "Elowen", "Thalion", "Lyanna", "Fenris", "Eowulf", "Galadriel", "Drogan",
    "Isolde", "Kael", "Aeliana", "Thrain", "Seraphine", "Borin", "Elara", "Gwydion",
    "Morwenna", "Aldric", "Lysandra", "Faelan", "Roderic", "Elara", "Thalindra", "Beric",
    "Mirella", "Arin", "Leofric", "Sarya", "Dorian", "Nymeria", "Calen", "Elara",
    "Thorne", "Valeria", "Eamon", "Lilith", "Orion", "Seraphina", "Vaelin", "Iselda"
        ];

        apodos = [
            "El Valiente", "La Hechicera", "El Guardián del Bosque", "La Dama de la Luz", "El Lobo Solitario",
    "El Sabio", "La Reina de las Sombras", "El Dragón Rojo", "La Doncella de Hielo", "El Fénix Renacido",
    "La Portadora del Destino", "El Martillo del Norte", "La Encantadora de Estrellas", "El Escudo de Plata",
    "La Forjadora de Espadas", "El Caminante de los Sueños", "La Doncella de la Tormenta", "El Guardián del Secreto",
    "La Llama Eterna", "El Héroe de las Mil Batallas", "El Inquebrantable", "La Loba Blanca", "El Cazador Nocturno",
    "La Doncella de Hierro", "El Señor de las Bestias", "La Guardiana de los Secretos", "El Juglar Errante",
    "La Sombra Vengadora", "El Guerrero Sin Nombre", "La Dama del Crepúsculo", "El Arquero Fantasma",
    "La Reina de los Susurros", "El Espadachín Imbatible", "La Maestra de Pociones", "El Conjurador de Tormentas",
    "La Portadora de la Espada Sagrada", "El Vengador Oscuro", "La Doncella de Cristal", "El Señor del Rayo", "La Luz del Alba"
        ];
    }

    public Personaje CrearPersonaje(int i)
    {
        Random rnd = new Random();
        int PD = (300 + i * 100) / 2;
        string nombre = nombres[rnd.Next(0, nombres.Count())];
        string apodo = apodos[rnd.Next(0, apodos.Count())]; ;
        int edad = rnd.Next(20, 301);
        
        int anio = DateTime.Today.Year - edad;
        int mes = rnd.Next(1, 13);
        int dia;

        switch (mes){
            case 2:
                dia = DateTime.IsLeapYear(anio) ? rnd.Next(1, 30) : rnd.Next(1, 29);
                break;
            case 4:
            case 6:
            case 9:
            case 11:
                dia = rnd.Next(1, 31);
                break;
            default:
                dia = rnd.Next(1, 32);
                break;
        }
        DateTime fecNaci = new DateTime(anio, mes, dia);

        //DateTime fecNaci = DateTime.Parse(rnd.Next(1, 31) + "/" + rnd.Next(1, 13) + "/" + (DateTime.Today.Year - edad));
        int nivel = i;
        int fue = rnd.Next(i, 11);
        int des = rnd.Next(i, 11);
        int agi = rnd.Next(i, 11);
        int con = rnd.Next(i, 11);
        int hAtaBase = 50 + rnd.Next(i * 20, PD + 1 - 100);
        int hDefBase = PD - hAtaBase;

        Categoria cat = categorias.ElementAt(rnd.Next(0, categorias.Count));
        Armadura armadura = armaduras.ElementAt(rnd.Next(0, armaduras.Count));

        Arma armaPj;
        if (fue < 3)
        {
            armaPj = armas.ElementAt(40);// desarmado
        }
        else
        {
            do
            {
                armaPj = armas.ElementAt(rnd.Next(0, armas.Count));
            } while (armaPj.FueR > fue);
        }


        Arma escudo;
        if (fue < 5 || armaPj.Especial == "A dos manos")
        {
            escudo = escudos.ElementAt(3);//sin escudo
        }
        else
        {
            do
            {
                escudo = escudos.ElementAt(rnd.Next(0, escudos.Count));
            } while (escudo.FueR > fue);
        }

        return new Personaje(nombre, apodo, fecNaci, edad, fue, des, agi, con, nivel, hAtaBase, hDefBase, cat, armaPj, armadura, escudo);
    }

    public List<Personaje> CrearPersonajes(int cant)
    {
        List<Personaje> personajes = new List<Personaje>();
        for (int i = 0; i < cant; i++)
        {
            personajes.Add(CrearPersonaje(i));
        }
        return personajes;
    }
}