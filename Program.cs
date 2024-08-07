using System.Text.Json;

int opc;
bool aux;

List<Insulto> insultos = null;
var insulto = await GetInsultAsync();
if (insulto != null)
{
    for (int i = 0; i < 12; i++)
    {
        insulto = await GetInsultAsync();
        if (insulto == null)
        {
            break;
        }
        insultos.Add(insulto);
    }
}
if (insultos == null)
{
    var jsonInsultos = GestorJson.AbrirArchivoTexto(Path.GetFullPath(@"../tl1-proyectofinal2024-Balaturdar/archivos/insults.json"));
    insultos = JsonSerializer.Deserialize<List<Insulto>>(jsonInsultos);
}
if (insultos.Count() < 9)
{
    insultos.Clear();
    var jsonInsultos = GestorJson.AbrirArchivoTexto(Path.GetFullPath(@"../tl1-proyectofinal2024-Balaturdar/archivos/insults.json"));
    insultos = JsonSerializer.Deserialize<List<Insulto>>(jsonInsultos);
}

string? nombrePartida;
List<Personaje>? Enemigos = null;
Personaje? Jugador = null;

do
{


    Console.WriteLine("***********************");
    Console.WriteLine("** 1-Nueva Partida   **");
    Console.WriteLine("** 2-Cargar Partida  **");
    Console.WriteLine("** 3-Ganadores       **");
    Console.WriteLine("** 4-eliminar partida**");
    Console.WriteLine("** 5-Salir           **");
    Console.WriteLine("***********************");
    Console.WriteLine("\nenter a number to choose an option");
    do
    {
        aux = int.TryParse(Console.ReadLine(), out opc);
        if (!aux)
        {
            Console.WriteLine("You must enter a number to select an option");
        }
        Console.Clear();
    } while (!aux);

    switch (opc)
    {
        case 1://Nueva partida
            nombrePartida = PersonajesJson.CrearNuevaPartida();
            FabricaPersonajes fabrica = new FabricaPersonajes();
            Jugador = fabrica.CrearPersonaje(1);
            Enemigos = fabrica.CrearPersonajes(10);
            PersonajesJson.GuardarPersonaje(Jugador, nombrePartida);
            PersonajesJson.GuardarEnemigos(Enemigos, nombrePartida);
            Console.Clear();
            Console.WriteLine("The fighter who will represent you is:\n");
            Thread.Sleep(1200);
            Console.WriteLine(Jugador.InfoPj() + "\n");
            Thread.Sleep(1200);
            Console.WriteLine("Press any key to exit... ");
            Console.ReadKey();
            Console.Clear();

            EjecutarPartida(Jugador, Enemigos, nombrePartida, insultos);

            break;
        case 2://cargar partida
            nombrePartida = PersonajesJson.cargarPartida(Jugador, Enemigos);
            if (nombrePartida == null)
            {
                break;
            }

            Enemigos = PersonajesJson.LeerEnemigos(nombrePartida);
            Jugador = PersonajesJson.LeerJugador(nombrePartida);
            if (Jugador == null && Enemigos == null)
            {
                Console.WriteLine("No saved characters found in this game");
                break;
            }

            EjecutarPartida(Jugador, Enemigos, nombrePartida, insultos);

            break;
        case 3://Ganadores
            var indice = 1;
            List<Personaje> ganadores = PersonajesJson.LeerGanadores();
            if (ganadores == null || ganadores.Count() < 1)
            {
                Console.WriteLine("no one has reached the top of the tower yet");
                Console.WriteLine("Press any key to continue... ");
                Console.ReadKey();
                break;
            }
            foreach (Personaje ganador in ganadores)
            {
                Console.WriteLine($"{indice} - {ganador.Nombre}");
                indice++;
            }


            Console.WriteLine("Select one of the winners to see their information");
            do
            {
                aux = int.TryParse(Console.ReadLine(), out indice);
                if (!aux)
                {
                    Console.WriteLine("You must enter a number to select a character");
                }
                if (indice > ganadores.Count() || indice < 0)
                {
                    Console.WriteLine("enter a valid option");
                }
            } while (!aux || indice > ganadores.Count() || indice < 0);
            Console.Clear();
            Console.WriteLine("¡¡¡THE NEW KING OF THE TOWER!!!");
            Console.WriteLine(ganadores[indice - 1].InfoPj());
            Console.WriteLine("Press any key to continue... ");
            Console.ReadKey();
            Console.Clear();
            break;
        case 4://eliminar partida
            PersonajesJson.EliminarPartida();
            break;
        case 5://salir
            break;
    }
} while (opc != 5);

static bool combate(List<Personaje> Enemigos, Personaje Jugador, List<Insulto> insultos)
{
    var Enemigo = Enemigos[0];
    var vidaMaxPJ = Jugador.PVida;
    Personaje primero, segundo;

    Console.WriteLine("####################################################################################################\n");
    Thread.Sleep(1200);
    Console.WriteLine(Jugador.InfoPj() + "\n");
    Thread.Sleep(1200);
    Console.WriteLine("\tVS\n");
    Thread.Sleep(1200);
    Console.WriteLine(Enemigo.InfoPj() + "\n");
    Console.WriteLine("Press any key to continue... ");
    Console.ReadKey();
    Console.Clear();


    if (Enemigo.Turno + Tirada() > Jugador.Turno + Tirada())
    {
        primero = Enemigo;
        segundo = Jugador;
    }
    else
    {
        primero = Jugador;
        segundo = Enemigo;
    }
    do
    {
        Pelea(primero, segundo, false, insultos);
        if (Jugador.PVida <= 0)
        {
            return false;
        }
        if (Enemigo.PVida <= 0)
        {
            Console.WriteLine($"¡¡¡{Jugador.Nombre} win the fight and move on to the next round!!!");
            Enemigos.Remove(Enemigo);
            break;
        }
        Pelea(segundo, primero, false, insultos);
        if (Jugador.PVida <= 0)
        {
            return false;
        }
        if (Enemigo.PVida <= 0)
        {
            Console.WriteLine($"¡¡¡{Jugador.Nombre} win the fight and move on to the next round!!!");
            Enemigos.Remove(Enemigo);
            break;
        }
    } while (Enemigo.PVida > 0);
    Console.WriteLine("\nPress any key to continue... ");
    Console.ReadKey();
    Console.Clear();
    Jugador.PVida = vidaMaxPJ;
    return true;
}


static int Tirada()
{
    int tirada = new Random().Next(1, 101);
    tirada += (tirada > 90) ? new Random().Next(1, 101) : 0;//la accion sale excepcionalmente bien
    tirada -= (tirada < 5) ? new Random().Next(1, 101) : 0;//la accion sale excepcionalmente mal
    return tirada;
}

static void Pelea(Personaje atacante, Personaje defensor, bool contraataque, List<Insulto> insultos)
{
    var tiradaAtaque = Tirada();
    var tiradaDefensa = Tirada();
    var ataqueFinal = atacante.Ataque() + tiradaAtaque;
    var defensaFinal = (defensor.HDefEsquiva > defensor.HDefParada) ? defensor.HDefEsquiva : defensor.HDefParada;
    defensaFinal += tiradaDefensa;
    var aux = ataqueFinal - defensaFinal;
    //Insulto insulto = await GetInsultAsync();
    //Thread.Sleep(500);

    var insulto = insultos.ElementAt(new Random().Next(0, insultos.Count())).Insult;
    Thread.Sleep(1000);
    Console.WriteLine("\n--------------------------------------------------------------------------------\n");
    Console.WriteLine($"{atacante.Nombre}: {insulto} ");
    Console.WriteLine($"attack skill: {ataqueFinal}\ndefense skill: {defensaFinal}");
    switch (aux)
    {
        case < -50://contraataque
            if (!contraataque)
            {
                Console.WriteLine($"{atacante.Nombre} fails the attack, {defensor.Nombre} can counterattack!");
                Pelea(defensor, atacante, true, insultos);
                break;
            }
            Console.WriteLine($"{atacante.Nombre} fails the counterattack!");
            break;
        case <= 0://ataque rechazado por el defensor
            Console.WriteLine($"{defensor.Nombre}, manages to repel the attack of {atacante.Nombre}");
            Console.WriteLine($"has {defensor.PVida} life points left");
            break;
        case < 10://impacta pero no logra hacer daño
            Console.WriteLine($"IMPACT!!!\nThe attack barely breaks through {defensor.Nombre}'s defense, but not enough to do any damage");
            Console.WriteLine($"has {defensor.PVida} life points left");
            break;
        case >= 10://impacta
            var danio = atacante.Arma.Danio + (int)Math.Floor((double)(aux / 10));
            if (danio - defensor.Absorcion(atacante.Arma.TArma) > 10)
            {
                //mensaje impacta y hace daño
                danio -= defensor.Absorcion(atacante.Arma.TArma);
                defensor.PVida -= danio;
                Console.WriteLine($"IMPACT!!!\nThe attack pierces {defensor.Nombre} 's armor and deals {danio} points of damage");
                Console.WriteLine($"has {defensor.PVida} life points left");

            }
            else
            {
                //mensaje impacta y pero su armadura resiste
                Console.WriteLine($"IMPACT!!!\nThe attack overcomes {defensor.Nombre}'s defense, but fails to penetrate his armor");
                Console.WriteLine($"has {defensor.PVida} life points left");
            }
            break;
    }
}

void EjecutarPartida(Personaje Jugador, List<Personaje> Enemigos, string nombrePartida, List<Insulto> insultos)
{
    bool resultadoPelea;
    do
    {
        // EmpezarPartida
        resultadoPelea = combate(Enemigos, Jugador, insultos);

        PersonajesJson.GuardarPersonaje(Jugador, nombrePartida);
        PersonajesJson.GuardarEnemigos(Enemigos, nombrePartida);
        if (resultadoPelea)
        {
            Jugador.SubirNivel();
            if (Enemigos.Count() > 0)
            {
                Console.WriteLine("Keep Fighting? Y/N");
                var seguirPeleando = Console.ReadLine();
                if (seguirPeleando != "Y" && seguirPeleando != "y")
                {
                    break;
                }
            }
            else
            {
                Console.WriteLine("You reach the top of the tower after having defeated all your enemies");
                Console.WriteLine("You find a book with the names of all the warriors who came here.");
                Console.WriteLine("Next to it there is a chest with your prize for overcoming the challenge.\n");
                Console.WriteLine("You write your name in the book and open the chest to claim your prize");
                Console.WriteLine("Inside the chest you find several t-shirts with a print that says");
                var remera = @"
              ._               _.
             /  `'''''   ''''''`  \
        .-''`'-..______________..-'`''-.
      /`\                             /`\
    /`   |                           |   `\
   /`    |         i beat the        |    `\
  /      |                           |      \
 /       /      challenge tower      \       \
/        |                           |        \
'-._____.|     and all i got was     |._____.-'
         |                           |
         |        this stupid        |
         |                           |
         |          T-shirt          |
         |                           |
         |                           |
         |                           |
         |._                       _.'
            `''-----------------''`
               ";
                Console.WriteLine(remera);

                PersonajesJson.GuardarGanador(Jugador);

                // info jugador
                Console.WriteLine("¡¡¡THE NEW KING OF THE TOWER!!!");
                Console.WriteLine(Jugador.InfoPj());
                Console.WriteLine("Press any key to exit... ");
                Console.ReadKey();

                PersonajesJson.BorrarPartida(nombrePartida);
                Console.WriteLine("GAME OVER");

                Thread.Sleep(900);
                Console.WriteLine("Press any key to exit... ");
                Console.ReadKey();
                break;
            }
        }
        else
        {
            Console.WriteLine($"The Fight was fierce but \n{Jugador.Nombre}'s efforts were not enough and he perished \na at the hands of {Enemigos.ElementAt(0).Nombre}, \nafter having defeated {10 - Enemigos.Count()} enemies");
            Thread.Sleep(900);

            Console.WriteLine("GAME OVER");
            Thread.Sleep(900);
            Console.WriteLine("Press any key to exit... ");
            Console.ReadKey();
            PersonajesJson.BorrarPartida(nombrePartida);
        }

    } while (resultadoPelea);
}


static async Task<Insulto> GetInsultAsync()
{
    var url = "https://evilinsult.com/generate_insult.php?lang=en&type=json";

    using (HttpClient client = new HttpClient())
    {
        using (var cts = new CancellationTokenSource())
        {
            cts.CancelAfter(400);
            try
            {
                HttpResponseMessage response = await client.GetAsync(url, cts.Token);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Insulto? insulto = JsonSerializer.Deserialize<Insulto>(responseBody);
                return insulto;
            }
            catch (TaskCanceledException)
            {
                return null;
            }
            catch (HttpRequestException e)
            {
                return null;
            }
        }
    }
}

