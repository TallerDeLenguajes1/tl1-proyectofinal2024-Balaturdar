using System.Text.Json;

int opc;
bool aux;
do
{


    Console.WriteLine("***********************");
    Console.WriteLine("** 1-Nueva Partida   **");
    Console.WriteLine("** 2-Cargar Partida  **");
    Console.WriteLine("** 3-Ganadores       **");
    Console.WriteLine("** 4-Opciones        **");
    Console.WriteLine("** 5-Salir           **");
    Console.WriteLine("***********************");
    Console.WriteLine("\ningrese un numero para elegir una opcion");
    do
    {
        aux = int.TryParse(Console.ReadLine(), out opc);
        if (!aux)
        {
            Console.WriteLine("debe ingresar un numero para seleccionar una opcion");
        }
        Console.Clear();
    } while (!aux);

    List<string> ListadoDePartidas = PersonajesJson.ListaDePartidasGuardadas();
    int contador = 0;

    List<Insulto> insultos = null;
    var insulto =  await GetInsultAsync();
    if(insulto != null){
        for (int i = 0; i < 12; i++)
        {
            insulto =  await GetInsultAsync();
            if(insulto == null){
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
    if(insultos.Count() <9 ){
        insultos.Clear();
        var jsonInsultos = GestorJson.AbrirArchivoTexto(Path.GetFullPath(@"../tl1-proyectofinal2024-Balaturdar/archivos/insults.json"));
        insultos = JsonSerializer.Deserialize<List<Insulto>>(jsonInsultos);
    }

    //var insultos = GestorJson.AbrirArchivoTexto(Path.GetFullPath(@"../tl1-proyectofinal2024-Balaturdar/archivos/insults.json"));
    //List<Insulto> insultosprecargados = JsonSerializer.Deserialize<List<Insulto>>(insultos);

    string? nombre;
    List<Personaje>? Enemigos = null;
    Personaje? Jugador = null;
    switch (opc)
    {
        case 1://Nueva partida
            nombre = PersonajesJson.CrearNuevaPartida();
            bool resultadoPelea = true;
            FabricaPersonajes fabrica = new FabricaPersonajes();
            Jugador = fabrica.CrearPersonaje(1);
            Enemigos = fabrica.CrearPersonajes(10);
            PersonajesJson.GuardarPersonaje(Jugador, nombre);
            PersonajesJson.GuardarEnemigos(Enemigos, nombre);

            do
            {
                //EmpezarPartida
                resultadoPelea = combate(Enemigos, Jugador, insultos);

                PersonajesJson.GuardarPersonaje(Jugador, nombre);
                PersonajesJson.GuardarEnemigos(Enemigos, nombre);
                if (resultadoPelea)
                {
                    if (Enemigos.Count() > 0)
                    {
                        Console.WriteLine("¿Seguir Peleando? S/N");
                        var SeguirPeleando = Console.ReadLine();
                        if (SeguirPeleando != "S" && SeguirPeleando != "s")
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Llegas a la cima de la torre luego de haber derrotado a todos tus enemigos");
                        Console.WriteLine("Encuentras un libro con los nombres de todos los guerreros que llegaron hasta aqui");
                        Console.WriteLine("Al lado un cofre con tu premio por superar el desafio\n");
                        Console.WriteLine("Escribes tu nombre y abres el cofre para reclamar tu premio");
                        Console.WriteLine("Dentro del cofre encuentras varias remeras con un estampado que dice");
                        Console.WriteLine("\t\"Derrote todos los enemigos\n\tde la torre y lo único que\n\tconsegui es esta estúpida\n\t\tremera");

                        PersonajesJson.GuardarGanador(Jugador);

                        //info jugador

                        PersonajesJson.BorrarPartida(nombre);
                        Console.WriteLine("FIN DE LA PARTIDA");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine($"La Pelea fue encarnizada pero los esfuerzos de \n{Jugador.Nombre} no fueron suficientes y perece \na manos de {Enemigos.ElementAt(0).Nombre}, \nluego de haber vencido a {10 - Enemigos.Count()} enemigos");
                    Console.WriteLine("FIN DE LA PARTIDA");
                    PersonajesJson.BorrarPartida(nombre);
                }

            } while (resultadoPelea);

            break;
        case 2://cargar partida
            var nombrePartida = PersonajesJson.cargarPartida(Jugador, Enemigos);
            if(nombrePartida == null){
                break;
            }
            
            var lastIndex = nombrePartida.LastIndexOf('\\');
            nombrePartida = nombrePartida.Substring(lastIndex + 1);

            Enemigos = PersonajesJson.LeerEnemigos(nombrePartida);
            Jugador = PersonajesJson.LeerJugador(nombrePartida);
            if (Jugador == null && Enemigos == null)
            {
                Console.WriteLine("No se encontraron personajes guardados en esta partida");
                break;
            }
            do
            {
                resultadoPelea = combate(Enemigos, Jugador, insultos);

                PersonajesJson.GuardarPersonaje(Jugador, nombrePartida);
                PersonajesJson.GuardarEnemigos(Enemigos, nombrePartida);
                if (resultadoPelea)
                {
                    if (Enemigos.Count() > 0)
                    {
                        Console.WriteLine("¿Seguir Peleando? S/N");
                        var SeguirPeleando = Console.ReadLine();
                        if (SeguirPeleando != "S" && SeguirPeleando != "s")
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Llegas a la cima de la torre luego de haber derrotado a todos tus enemigos");
                        Console.WriteLine("Encuentras un libro con los nombres de todos los guerreros que llegaron hasta aqui");
                        Console.WriteLine("Al lado un cofre con tu premio por superar el desafio\n");
                        Console.WriteLine("Escribes tu nombre y abres el cofre para reclamar tu premio");
                        Console.WriteLine("Dentro del cofre encuentras varias remeras con un estampado que dice");
                        Console.WriteLine("\t\"Derrote todos los enemigos\n\tde la torre y lo único que\n\tconsegui es esta estúpida\n\t\tremera");

                        PersonajesJson.GuardarGanador(Jugador);

                        //info jugador

                        PersonajesJson.BorrarPartida(nombrePartida);
                        Console.WriteLine("FIN DE LA PARTIDA");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine($"La Pelea fue encarnizada pero los esfuerzos de \n{Jugador.Nombre} no fueron suficientes y perece \na manos de {Enemigos.ElementAt(0).Nombre}, \nluego de haber vencido a {10 - Enemigos.Count()} enemigos");
                    Console.WriteLine("FIN DE LA PARTIDA");
                    PersonajesJson.BorrarPartida(nombrePartida);
                }

            } while (resultadoPelea);
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
} while (true);

static bool combate(List<Personaje> Enemigos, Personaje Jugador, List<Insulto> insultos)
{
    var Enemigo = Enemigos[0];
    Personaje primero, segundo;

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
            Console.WriteLine($"¡¡¡{Jugador.Nombre} gana la pelea y pasa a la siguiente ronda!!!");
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
            Console.WriteLine($"¡¡¡{Jugador.Nombre} gana la pelea y pasa a la siguiente ronda!!!");
            Enemigos.Remove(Enemigo);
            break;
        }
    } while (Enemigo.PVida > 0);

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
    var insulto = insultos.ElementAt(new Random().Next(0,insultos.Count())).Insult;

    Thread.Sleep(500);
    Console.WriteLine($"{atacante.Nombre}: {insulto} ");
    Console.WriteLine($"Habilidad de Ataque: {ataqueFinal}\nHabilidad de Defensa: {defensaFinal}");
    switch (aux)
    {
        case < -50://contraataque
            if (!contraataque)
            {
                Console.WriteLine($"{atacante.Nombre} falla el ataque, ¡{defensor.Nombre} puede contraatacar!");
                Pelea(defensor, atacante, true, insultos);
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

static async Task<Insulto> GetInsultAsync()
{
    var url = "https://evilinsult.com/generate_insult.php?lang=en&type=json";

    using (HttpClient client = new HttpClient())
    {
        using (var cts = new CancellationTokenSource())
        {
            cts.CancelAfter(400); // Cancel after 400 milliseconds
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
                //return insultosprecargados.ElementAt(new Random().Next(0, insultosprecargados.Count()));
            }
            catch (HttpRequestException e)
            {
                //Console.WriteLine("Problemas de acceso a la API");
                //Console.WriteLine("Message :{0} ", e.Message);
                return null;
                //return insultosprecargados.ElementAt(new Random().Next(0, insultosprecargados.Count()));
            }
        }
    }
}

