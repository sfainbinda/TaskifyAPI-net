# API de Tareas

API para la gestión de tareas, permitiendo a los usuarios crear, actualizar y eliminar tareas. También se incluyen funcionalidades como asignar prioridades, fechas de vencimiento y etiquetas a las tareas.

## Introducción

Este proyecto implementa una API para la gestión de tareas, brindando a los usuarios la capacidad de administrar sus actividades diarias. La API permite realizar operaciones como crear nuevas tareas, actualizar su estado, asignar prioridades, establecer fechas de vencimiento y asignar etiquetas para una mejor organización.

## Características

- Registro de usuarios con validación de datos.
- Inicio de sesión de usuarios existentes.
- Creación de nuevas tareas con información detallada.
- Actualización del estado y detalles de las tareas.
- Asignación de prioridades y fechas de vencimiento a las tareas.
- Etiquetado de tareas para una mejor organización.
- Eliminación de tareas existentes.

## Tecnologías Utilizadas

- ASP.NET Core: Framework para la construcción de aplicaciones web y APIs.
- Entity Framework Core: ORM utilizado para el acceso a la base de datos.
- JWT (JSON Web Tokens): Tecnología utilizada para la autenticación y autorización basada en tokens.
- Cookies: Utilizadas para la gestión de sesiones y autenticación persistente.

## Instalación

1. Clona el repositorio: `git clone <URL del repositorio>`
2. Abre el proyecto en tu IDE de preferencia (por ejemplo, Visual Studio).
3. Configura la cadena de conexión a la base de datos en el archivo `appsettings.json`.
4. Ejecuta el proyecto y navega a la URL local especificada para acceder a la API.

## Documentación de la API

A continuación, se describen los principales endpoints disponibles en la API. Para obtener información detallada sobre los parámetros de solicitud, las respuestas y los posibles errores, consulta la documentación completa en [URL de la documentación].

### Registro de Usuario

- Método: POST
- URL: `/api/users/register`
- Descripción: Permite registrar un nuevo usuario en el sistema.
- Parámetros de solicitud:
  - `firstName` (obligatorio): Nombre del usuario.
  - `lastName` (obligatorio): Apellido del usuario.
  - `email` (obligatorio): Correo electrónico del usuario.
  - `password` (obligatorio): Contraseña del usuario.
  - `repeatPassword` (opcional): Repetición de la contraseña para confirmación.
- Respuesta exitosa:
  - Código de estado: 201 (Created)
  - Cuerpo de respuesta: Objeto JSON con los datos del usuario registrado.
- Posibles errores:
  - Código de estado: 400 (Bad Request)
  - Cuerpo de respuesta: Mensaje de error indicando los problemas en los datos de entrada.

### Inicio de Sesión

- Método: POST
- URL: `/api/users/login`
- Descripción: Permite que un usuario inicie sesión en el sistema.
- Parámetros de solicitud:
  - `email` (obligatorio): Correo electrónico del usuario.
  - `password` (obligatorio): Contraseña del usuario.
- Respuesta exitosa:
  - Código de estado: 200 (OK)
  - Cuerpo de respuesta: Token JWT que representa la sesión del usuario.
- Posibles errores:
  - Código de estado: 401 (Unauthorized)
  - Cuerpo de respuesta: Mensaje de error indicando las credenciales inválidas.

### Obtener Usuario por ID

- Método: GET
- URL: `/api/users/{id}`
- Descripción: Permite obtener información de un usuario por su ID.
- Parámetros de solicitud:
  - `id` (obligatorio): ID del usuario.
- Respuesta exitosa:
  - Código de estado: 200 (OK)
  - Cuerpo de respuesta: Objeto JSON con los datos del usuario solicitado.
- Posibles errores:
  - Código de estado: 404 (Not Found)
  - Cuerpo de respuesta: Mensaje de error indicando que el usuario no fue encontrado.

### Eliminar Usuario

- Método: DELETE
- URL: `/api/users/{id}`
- Descripción: Permite eliminar un usuario del sistema.
- Parámetros de solicitud:
  - `id` (obligatorio): ID del usuario.
- Respuesta exitosa:
  - Código de estado: 200 (OK)
  - Cuerpo de respuesta: Sin contenido.
- Posibles errores:
  - Código de estado: 404 (Not Found)
  - Cuerpo de respuesta: Mensaje de error indicando que el usuario no fue encontrado.

## Licencia

Este proyecto se distribuye bajo la Licencia MIT. Para obtener más información, consulta el archivo [LICENSE](LICENSE).

## Contacto

Para consultas o comentarios sobre este proyecto, puedes ponerte en contacto con nosotros a través de sfainbinda@gmail.com
