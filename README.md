# Proyecto Migración de Comparendos Systrans 3
- El proyecto está hecho en Arquitectura Limpia: con las capas de dominio, aplicación, infraestructura, presentación
- Tiene las reglas configuradas para crear las relaciones entre las diferentes tablas
- Se ha configurado la herramienta de Mappers
- La capa de presentación simula la migración de datos de Simit
- Se ha configurado la herramienta de MediatR para configurar el patrón Mediator que facilita la comunicación entre capas y asegura el desacoplamiento de la aplicación
- Se ha programado en el patrón de diseño CQRS para dividir las funcionalidades de Consulta de datos de tipo lectura (Querys) y la creación, actualización y eliminación de datos (Commands)
- Se ha configurado el patrón repository para crear una capa para abstraer la lógica de las diferentes consultas realizadas a bases de datos
- Se ha implementado el patrón de inyección de dependencias para que el programa sea capas de crear las instancias de las funcionalidades que han sido instanciadas en las diferentes implementaciones con el ciclo de vida Scope.

# Ver la versión de Dotnet instalada en su sistema operativo
- Asegurarse de tener la versión de .NET o Dotnet especificada en el archivo global.json
# Clonar Repositorio
- Para Clonar el respositorio correr el siguiente comando:
~~~
git clone <URL_del_repositorio> <nombre_de_directorio_local>
~~~
# Reconstruir Proyecto después de Clonarlo
- Para reconstruir un proyecto de DOTNET o .NET utilizar el siguiente comando en la carpeta raiz, para que se instalen las librerias de los archivos .csproj
~~~
dotnet restore
~~~
# Compilar la aplicación para ver los errores
- Ejecutar el siguiente comando para compilar la aplicación y ver advertencias y errores
~~~
dotnet build
~~~
# Ejecutar la aplicación
- Abrir la carpeta de la capa de presentación y ejecutar el siguiente comando
~~~
dotnet run
~~~
