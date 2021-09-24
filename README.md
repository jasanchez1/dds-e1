# dds-e1

DDS-E1-G22 repository:

The 3D Breakout game implementation you cant find in this repository
it's a slight modification from the game you can find here:

https://assetstore.unity.com/packages/templates/tutorials/c-game-examples-116#description

All credits to assetstore.unity.com for serving as the base code for this project.

How to run the game:

(Spanish ahead)

Para probar es necesario tener instalado unity, al abrir el proyecto es posible apretar “build and run” en file. La escena con la que se inicia el juego es MainMenu, su ruta es ```Assets/MainMenu/MainMenu.unity```. La escena principal del juego es Breakout y su ruta es ```Assets/Game 4 - 3D breakout/Breakout.unity``` .

Como jugador debes poner un username al iniciar el juego y luego poner start game. A partir de ahí comienza juego este se termina cuando se acaban las pelotas o los ladrillos, puedes volver a comenzar o volver al menú principal.

El código del juego está estructurado de la siguiente manera, tenemos la carpeta ```Assets```, donde se encuentra toda la lógica y los principales assets asociados al juego. Los scripts dentro de ```Assets/Game - 4 3D breakout/scripts```  controlan la lógica del juego en sí, mientras que para la lógica del menú es ```Assets/MainMenu/MainMenu.cs```.

El archivo del puntaje se guarda una carpeta más arriba de la raíz del código, ```../puntajes.txt```. Estos guardan el nombre insertado y el puntaje más alto.

Grupo22:
- Felipe Lois
- Tomás Rivera
- Joaquín Sanchez
- Trinidad Vargas
