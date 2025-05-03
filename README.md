# Documentación Backend del proyecto "PokeCore"
![net](https://img.shields.io/badge/dotnet-purple?logo=dotnet&label=.NET%208.0)






### 🌀 Backend PokeCore 🌀

> En esta documentacion se explicara el prototipado del avance del core, se trata de una analisis de comparacion de datos de los pokemones mediante la base de datos de pokemones mediante su propio API.
> En la planeacion del proyecto se planeo dos funcionalidades, la primera es la comaparacion de poderes de dos pokomon al momento de dar batalla, para analizar las fortalezas y debilidades de cada uno, mediante
> el analisis de los stats proporcionados por el PokeApi. La segunda fucnionalidad es el analisis de la fuerza en un equipo de 6 pokemones, donde al colocar en un rango de uno a 6 pokemones en el equipo, se analizara,
> la fortaleza, el ataque, la defensa, la velocidad y la salud de cada uno, para determinar si el equipo esta equiibrado o si mismo falta velocidad, tanque o ataque. Esta aplicacion sirve para que todos los jugadores de
> pokemones puedan analizar y estudiar para ganar competencias en el juego.

## Lista de elementos aplicados y aprendidos en este proyecto

| #  | Tema                   | Descripción                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           | Complejidad |
|----|------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-------------|
| 00 | **Patron Repositorio** | Este es uno de mis patrones favoritos al implementar mi backend en .NET porque gracias a ello me facilita la separación de responsabilidades, debido a la implementación de la interfaz. Con uno con varias interfaces es posible crear varios repositorio basados en las operaciones CRUD, y con ello facilita implementar el controlador.                                                                                                                                                                                           |![Static Badge](https://img.shields.io/badge/100-green?style=flat&label=Baja)
| 01 | **WebAPI**             | Una de las caracteristicas mas importantes de .NET es el WebApi donde como uusario es facil implementar APIRest tales como operaciones CRUD, autenticacion, JWT, Autorizacion, GraphQL entre otros. Es decir posee una versatilidad al momento de implementar el Backend.                                                                                                                                                                                                                                                             |![Static Badge](https://img.shields.io/badge/100-green?style=flat&label=Baja)
| 02 | **MVC**                | **MVC** es considerado el patron mas antiguo lo cual en su tiempo era el mas efectivo al momento de desarrollar una aplicación. Este patron cosiste en la separacion de resposabilidades, el modelo, la vista y el controlador. Existen varias maneras de implementar el patron MVC, en mi caso aplique el MC para el backend y el V para la vista que es el FrontEnd y lo unifique en un solo proyecto. Este patron sirve mucho para nivel educativo pero para proyectos mas grandes se hae el uso de patrones modernos como el MVVM. |![Static Badge](https://img.shields.io/badge/90-green?style=flat&label=Baja)
| 03 | **DTOs**               | Como en sus siglas signififca **Data Transfer Object**, es un patrón de diseño utilizado en programación orientada a objetos para transferir datos entre diferentes capas de un programa. El objetivo principal de un DTO es proporcionar una interfaz sencilla para transferir datos de una capa a otra sin lógica de negocio.                                                                                                                                                                                                       |![Static Badge](https://img.shields.io/badge/100-green?style=flat&label=Baja)
| 04 | **Autenticación Jwt**  | Un JWT es un mecanismo para verificar el propietario de algunos datos JSON. Es una cadena codificada y segura para URL que puede contener una cantidad ilimitada de datos (a diferencia de una cookie) y está firmada criptográficamente. Cuando un servidor recibe un JWT, puede garantizar que los datos que contiene sean confiables porque están firmados por la fuente. Ningún intermediario puede modificar un JWT una vez enviado. Es importante tener en cuenta que un JWT garantiza la propiedad de los datos pero no el cifrado. Cualquier persona que intercepte el token puede ver los datos JSON que almacena en un JWT porque solo está serializado, no cifrado. Por esta razón, se recomienda encarecidamente utilizar HTTPS con JWT (y HTTPS en general, por cierto).                                                                                                                                                                             |![Static Badge](https://img.shields.io/badge/100-green?style=flat&label=Baja)
| 05 | **Supabase**           | Supabase es una plataforma BaaS (Backend as a Service) de código abierto que sirve como alternativa a Firebase, ofreciendo una base de datos PostgreSQL, autenticación, funciones en tiempo real y más.                                                                                                                                                                                 |![Static Badge](https://img.shields.io/badge/100-green?style=flat&label=Baja)
| 06 | **AWS Lambda**         | Al haber aprendido este servicio y sus funciones, realice la implementación y empaquetación del API usando uno de los servicios mas demandados de AWS, antes de empaquetar mi API, realice ciertas modificaciones desde .NET para poder enviar hacia la nube. Tuve exito al migrar hacia la nube de AWS ya que me resulto mas facil ejecutar desde mi FrontEnd.                                                                                                                                                                       |![Static Badge](https://img.shields.io/badge/60-yellow?style=flat&label=Medio)

## Tecnologías usadas

![Static Badge](https://img.shields.io/badge/.NET%208.0-%23512BD4?style=for-the-badge&logo=dotnet&label=TOOL&labelColor=black) ![Static Badge](https://img.shields.io/badge/CSharp-%23512BD4?style=for-the-badge&logo=dotnet&label=LANGUAGE&labelColor=black)





## Instrucciones

**Si desea revisar el proyecto, puede clonar con git clone o descargar Zip.**

**Para usuarios de Visual Studio o Rider**

1. Una vez clonado o descargado el proyecto, solamente debera abrir la solución con el IDE de su preferencia, en este caso Visual Studio o Rider.

**Para usuarios de Visual Studio Code**

1. Una vez clonado o descargado el proyecto, debera abrir la carpeta del proyecto en Visual Studio Code. Para la ejecucion debe tener instalado el SDK de .NET 8.0 y el plugin de C#.

**Nota adicional de AWS**

Para poder ejecutar el proyecto desde su IDE, debera tener instalado el plugin de AWS Toolkit para su IDE, y 
tener configurado su cuenta de AWS. Para la base de datos DynamoDB, debera crear una tabla con el nombre "ProjectManagement" y 
los atributos "Id" y "Name" como clave primaria. Para la API Gateway, debera crear un endpoint con el nombre "ProjectManagement" y asociarlo a la Lambda creada.

---

## <img src="https://github.com/AngelDavidStudios/calculadora-propinas/blob/main/src/resources/ads-emote.JPG" width="55" height="55"> Hola, mi nombre es David Angel.
### Multimedia Desginer & Software Architect

Soy diseñador Multimedia cursando una segunda carrera en Ingeniería de Software, me estoy especializando en el desarrollo Backend como arquitecto de software, en este recorrido dia tras dia aprendo tecnologias nuevas.

David Angel Studios es mi marca personal donde mi misión es desarrollar diversos proyectos basado en mi apredizaje de nuevas tecnologias y almacenarlas en mi portafolio personal:

[![Linkedin](https://img.shields.io/badge/Linkedin-4479A1?style=for-the-badge&logo=9gag&label=Angel%20David%20Studios&labelColor=black)](https://www.linkedin.com/in/angeldavidstudios/)
[![Instagram](https://img.shields.io/badge/Instagram-FF0069?style=for-the-badge&logo=instagram&label=Angel%20David%20Studios&labelColor=black)](https://www.instagram.com/angeldavidstudios/) [![Youtube](https://img.shields.io/badge/Angel--David--Studios-FF0000?style=for-the-badge&logo=youtube&label=Youtube.com%2F&labelColor=black)](https://www.youtube.com/channel/UC2VYRq169QluoLeagCYrjVg)