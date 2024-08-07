# Proyecto Taller 1 - Villafañe Cesar Nahuel

Este es un juego de combates por turno de personajes de rol basados en el sistema _"Anima Beyond Fantasy"_, donde el personaje que representa al jugador se enfrentara a 10 contrincantes para llegar a la cima de la torre del desafio.

todos los personajes son generados aleatoriamente, despues de cada combate se guarda la partida dando la posibilidad cargar partida en otro momento, tambien se pueden eliminar partidas y ver la informacion de los personajes que completaron el desafio.

se implementaron una seleccion reducida de las reglas de creacion de personajes y combate

### API: EvilInsultGenerator
La API `EvilInsultGenerator: https://evilinsult.com/generate_insult.php?lang=en&type=json` proporciona insultos generados aleatoriamente. La respuesta es un json con un insulto, fecha de creacion, lenguaje y en que web se encontro el insulto. Uso una clase para deserializar el json y capturar solo lo que me interesa que es el insulto generando una lista de insultos para elegir aleatoriamente en cada turno de la pelea. 
Dadas frecuentes fallas en la respuesta de la API superando un tiempo de respuesta establecido de 400ms y más alto se carga la lista de insultos con insultos precargados de un json local, logrando con esto mas robustez y evitando lentitud en el programa por la espera de las respuestas.

## Mecanicas Implementadas

### Creacion de personaje

Las caracteristicas basicas del personaje solo se utilizaran fuerza, constitucion, destreza y agilidad

- **Nombre**: Nombre del personaje
- **Apodo**: Apodo del personaje.
- **Fecha**: Fecha de nacimiento del personaje.
- **Edad**: Edad del personaje.
- **Fuerza**: se utiliza para calcular daño y si el personaje puede utilizar un escudo o arma
- **Destreza**: se utiliza para calcular parada , ataque, turno
- **Agilidad**: se utiliza para calcular la esquiva y el turno
- **Constitucion**: se utiliza para calcular la vida
- **Nivel**: es el nivel actual del personaje
- **Habilidad de Ataque Base**: se utiliza para calcular el ataque
- **Habilidad de Defensa Base**: se utiliza para calcular la parada y esquiva
- **Categoria**: es la clase del personaje, contiene que valores aumenta por nivel
- **Puntos de vidaVida**: vida base 20 + la caracteristica de constitucion * 10 + BonoAtributo(Con) + la vida de la categoria por nivel;
- **Arma**: es el arma que tiene el personaje, 
- **Escudo**: es el escudo que tiene el personaje, solo si tiene arma a una mano
- **Armadura**: la armadura del personaje, contiene los puntos de absorcion segun el tipo de ataque
- **turno**: se utiliza para calcular la iniciativa del personaje, tomando en cuenta bonificadores de destreza, agilidad y penalizadores del arma, escudo y armadura
- **Habilidad de Parada**: es la habilidad defensiva para detener un ataque
- **habilidad de Esquiva**: es la habilidad defensiva para esquivar un ataque

### Las Tiradas Abiertas y las Pifias en Combate – Core Exxet – 96

Todas las tiradas de D100 que se realizan en los combates emplean las reglas de Tirada Abierta y Pifia explicadas en la introducción (la única excepción aparece en el cálculo del Nivel de Crítico, donde no se admite ninguna de las dos).

* La Pifia en defensa
    Cuando un personaje obtenga una Pifia en cualquiera de sus tiradas de defensa, significa que ha realizado una maniobra errónea, beneficiando así a su adversario.
    Deberá calcular el Nivel de Pifia y restar la cantidad directamente de su habilidad de defensa. Si el resultado es además superior a 80, el Director de Juego podrá decidir si sufre alguna otra consecuencia, como tropezar, perder su arma…
* La Pifia en el ataque
    Obtener una Pifia en una tirada de ataque representa, de manera automática, que el golpe ha errado. Si se encuentra trabado en combate cuerpo a cuerpo, el personaje dará una oportunidad a su contrincante de realizar una contra. Si este es capaz de aprovecharla (es decir, aún le queda la posibilidad de realizar ataques), podrá sumar el nivel de fracaso a su habilidad en el ataque. Sea cual sea el resultado, la Pifia niega al personaje la capacidad de realizar más acciones activas durante ese asalto. Si el Nivel de Pifia es superior a 80, el Director de Juego decidirá otras posibles consecuencias, como golpear a un compañero, perder el arma…

### Los Escudos – Core Exxet – 77

Los escudos tienen un funcionamiento diferente al del resto de las armas. Aunque pueden utilizarse perfectamente para atacar contundentemente, son objetos principalmente de protección, puesto que otorgan un bono a la habilidad defensiva del personaje. Salvo en el caso de la rodela, los escudos
medios y corporales obligan a usar una mano para llevarlos, pero al contrario de lo que ocurre con otras armas, emplearlos con la zurda no provoca ningún negativo a la habilidad de parada (aunque sí a la de ataque). Estos objetos, al igual que las armaduras, tienen una restricción directa sobre la iniciativa, que se resta del turno final como un penalizador especial.

### Los bonos y su aplicación – Core Exxet – 11 

Ahora que ya conocemos las características de nuestro personaje, debes de saber que
cada una de ellas tiene un bono, una cifra que modificará todas las habilidades que
dependan de ella. Según el valor de nuestra característica, el bono correspondiente
será una cifra positiva o negativa, tal como se indica en la Tabla 2.
| Características | Bonos |
| --------------- | ----- |
|1 | -30 |
|2 | -20 |
|3 | -10 |
|4 | -5 |
|5 | 0 |
|6-7 | +5 |
|8-9 | +10 |
|10 | +15 |
|11-12 | +20 |
|13-14 | +25 |
|15 | +30 |
|16-17 | +35 |
|18-19 | +40 |
|20 | +45 |

### Puntos de Vida y Turno  - Core Exxet -16

Los puntos de vida se basan en la Constitución: la base de todo ser vivo es siempre de 20 puntos, a los que debe añadirle su característica de Constitución multiplicada por diez y posteriormente sumarle o restarle su bono. Para vuestra mayor comodidad, todos estos cálculos están ya realizados en la Tabla 4. A esta cantidad deberás agregarle el bono innato de la categoría de tu personaje.

El turno base de cualquier personaje de tamaño normal es siempre de 20. Para calcular su turno final, debes de sumar los bonos de Destreza y Agilidad a esta base. A continuación resta cualquier penalizador que puedas tener por utilizar una armadura y suma o resta el turno del arma que emplees. Si no utilizas ninguna, ir desarmado otorga un bonificador de +20 al turno. Finalmente, añadele el bono innato de tu categoría. El resultado de todo será el turno final de tu personaje. Recuerda que es posible que alguien tenga diversos turnos según las distintas armas o armaduras que emplee. Utiliza siempre la primera columna del recuadro de reacción de la ficha para calcular su turno final desarmado y sin armadura, y las otras para las distintas combinaciones posibles.

### El daño y la velocidad de las armas – Core Exxet -73

Todas las armas producen un daño base determinado, al que se le debe sumar el bono de Fuerza de quien las esgrima para calcular su daño final, redondeándolo en grupos de 10 hacia arriba. En el caso de que esté preparada para utilizarla a dos manos y el luchador la emplee así, podrá doblar este bono. La velocidad del arma es la cifra que el personaje usa para calcular su turno con ella. Un arma puede tener una velocidad positiva o negativa, que se sumará o restará a la iniciativa. Si alguien lleva un arma en cada mano y pretende esgrimir ambas en combate, empleará la velocidad de la más lenta. Luchar desarmado tiene una velocidad 20 y un daño base 10, salvo si se domina algún arte marcial.

### Core Exxet -74

* Arma a dos manos: Son las que se usan necesariamente con ambas manos. Las de este tipo permiten al personaje doblar el bono de Fuerza para calcular su daño final.
* Arma a una o dos manos: Pueden utilizarse indistintamente con una o ambas manos. Si se usan con las dos, también permiten doblar el bono de Fuerza para calcular su daño final. En ellas aparecen indicados dos números en la fuerza requerida; el primero es el necesario para usarla con dos manos, y el segundo con sólo una.

### Absorcion

Cualquier personaje o criatura es siempre una base de 20 más la protección que le proporcione su armadura, que es de 10 por cada Tipo de Armadura que posea contra esa determinada clase de ataque. Es decir, un personaje con Tipo de Armadura 1 absorbería 30 puntos de ataque (20 natural más 10 de su armadura), mientras que otro con Tipo de Armadura 6 absorbería
80 (20 natural más 60 de la armadura). Ten en cuenta que, dependiendo del medio que el agresor utilice (Filo, Contundente, Penetrante...), la TA defensora puede ser diferente. Para calcular
correctamente cual es la Absorción del defensor contra un determinado tipo de impacto, primero hay que saber cual de las TA deberemos sumar.


### Combate

El combate se compone de 4 fases:
1.comprobar la iniciativa para determinar que personaje actua primero siendo el que tenga el valor mas alto quien actua primero
2. Calcular el Ataque Final: Lanza un D100 y suma el resultado a su habilidad ofensiva. A Continuación, se añaden los posibles modificadores por situaciones de combate de la Tabla 43. El resultado obtenido será nuestro Ataque
3. Calcular la Defensa Final: Para defenderse, se emplea la habilidad de parada o esquiva. Se  calcula de la misma manera que en el ataque, aunque en este caso el resultado se sumará a la habilidad defensiva que decidamos emplear. Al ser una acción pasiva, un personaje tendrá siempre derecho a defenderse: como mínimo podrá lanzar los dados, esperando que la suerte pueda salvarle por baja que sea su pericia.
4. Comparación de resultados: Ya con ambos resultados, restamos al Ataque Final del agresor la Defensa Final de su contrincante. La diferencia obtenida nos indicará el Resultado del Asalto, que determina el efecto del ataque. Si el atacante es superior, habrá alcanzado su objetivo. Si lo es el defensor, habrá conseguido defenderse y contraatacar.
* El ataque impacta 
    Si el valor es positivo, el defensor sufre un daño porcentual equivalente a dicha cifra redondeada a la baja en grupos de 10, o lo que es lo mismo; cada 10 puntos que el atacante tenga a su favor, provoca a su vez un 10% de daño. Es decir, si el resultado fuera 27 sufriría un 20% de daño, mientras que si fuera 185, sufriría un 180%. Este porcentaje se aplica al daño final del ataque, y el resultado se resta directamente del total de PV que tenga el objetivo (para agilizar el cálculo de estos porcentajes, se ha incluido la Tabla 42 como referencia).
    Si tras restar la Absorción el valor f inal es inferior a 10, el defensor no sufre daños. Simboliza que el atacante, pese haber superado su guardia y haberle golpeado, no consigue provocar una verdadera herida; ha cortado únicamente ropa o su arma ha rebotado contra la armadura del defensor.

* El ataque falla y es posible realizar una contra
    Dicho bono es equivalente a la mitad del valor que ha obtenido a su favor en el Resultado del Asalto, redondeado a la baja en grupos de 5.
    Es decir, si el Resultado del Asalto es -30 el defensor obtendría un +15 a su contra, si es -100 obtendría un +50, y así sucesivamente. El bono máximo que un personaje puede obtener de este modo nunca puede ser mayor de +150.

