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
        var miGestor = new GestorJson();
        string jsonDocument = miGestor.AbrirArchivoTexto("armaduras.json");
        this.armaduras = JsonSerializer.Deserialize<List<Armadura>>(jsonDocument);
                
        jsonDocument = miGestor.AbrirArchivoTexto("armas.json");
        this.armas = JsonSerializer.Deserialize<List<Arma>>(jsonDocument);

        jsonDocument = miGestor.AbrirArchivoTexto("categorias.json");
        this.categorias = JsonSerializer.Deserialize<List<Categoria>>(jsonDocument);

        jsonDocument = miGestor.AbrirArchivoTexto("escudos.json");
        this.escudos = JsonSerializer.Deserialize<List<Arma>>(jsonDocument);

        nombres = ["juan", "pepe", "jorge"];
        apodos = ["apodo1", "apodo2", "apodo3", "apodo4"];
    }

    public Personaje CrearPersonaje(int i)
    {
        Random rnd = new Random();
        int PD= (300+i*100)/2; 
        string nombre = nombres[rnd.Next(0,nombres.Count()+1)];
        string apodo = apodos[rnd.Next(0,apodos.Count())+1];;
        int edad = rnd.Next(20, 301);
        DateTime fecNaci = DateTime.Parse(rnd.Next(1, 31) + "/" +  rnd.Next(1, 13) + "/" + (DateTime.Today.Year - edad));
        int nivel= i;
        int fue = rnd.Next(i, 11);
        int des = rnd.Next(i, 11);
        int agi = rnd.Next(i, 11);
        int con = rnd.Next(i, 11);
        int hAtaBase = 50 + rnd.Next(i*20,PD+1-100);
        int hDefBase = PD - hAtaBase;

        Categoria cat = categorias.ElementAt(rnd.Next(0, categorias.Count+1));

        Arma armaPj;
        if(fue < 3){
            armaPj = armas.ElementAt(41);
        }else{
            do
            {
                armaPj = armas.ElementAt(rnd.Next(0, armas.Count+1));
            } while (armaPj.FueR < fue);
        }
        
        Armadura armadura = armaduras.ElementAt(rnd.Next(0, armaduras.Count+1));

        Arma escudo;
        if(fue < 5){
            escudo = escudos.ElementAt(3);
        }else{
            do
            {
                escudo = escudos.ElementAt(rnd.Next(0,escudos.Count+1));
            } while (escudo.FueR <= fue);
        }

        return new Personaje(nombre, apodo,fecNaci,edad,fue,des,agi,con,nivel,hAtaBase,hDefBase,cat,armaPj,armadura,escudo);
    }
    
    public List<Personaje> CrearPersonajes(int cant){
        List<Personaje> personajes = new List<Personaje>();
        for (int i = 0; i < cant; i++)
        {
            personajes.Add(CrearPersonaje(i));
        }
        return personajes;
    }
}