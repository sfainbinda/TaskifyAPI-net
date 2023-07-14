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

### Usuarios API

A continuación se describen los endpoints principales disponibles en la API de usuarios. Para obtener información detallada sobre los parámetros de solicitud, las respuestas y los posibles errores, consulta la documentación completa en [URL de la documentación].

### Eliminar usuario

- Método: DELETE
- URL: `/api/users/{id}`
- Descripción: Permite eliminar un usuario existente.
- Parámetros de solicitud:
  - `id` (obligatorio): Identificador único del usuario.
- Respuesta exitosa:
  - Código de estado: 200 (OK)
- Posibles errores:
  - Código de estado: 404 (Not Found)

### Obtener todos los usuarios

- Método: GET
- URL: `/api/users`
- Descripción: Obtiene una lista de todos los usuarios registrados.
- Respuesta exitosa:
  - Código de estado: 200 (OK)
  - Cuerpo de respuesta: Lista de objetos JSON que representan los usuarios.

### Obtener usuario por ID

- Método: GET
- URL: `/api/users/{id}`
- Descripción: Obtiene un usuario por su identificador único.
- Parámetros de solicitud:
  - `id` (obligatorio): Identificador único del usuario.
- Respuesta exitosa:
  - Código de estado: 200 (OK)
  - Cuerpo de respuesta: Objeto JSON que representa al usuario.
- Posibles errores:
  - Código de estado: 404 (Not Found)

### Registrar usuario

- Método: POST
- URL: `/api/users`
- Descripción: Permite registrar un nuevo usuario en el sistema.
- Parámetros de solicitud:
  - `entity` (obligatorio): Objeto JSON que contiene los datos del usuario a registrar.
- Respuesta exitosa:
  - Código de estado: 201 (Created)
  - Cuerpo de respuesta: Objeto JSON que representa al usuario registrado.
- Posibles errores:
  - Código de estado: 401 (Unauthorized)
  - Cuerpo de respuesta: Mensaje de error indicando que se requiere autenticación.

### Actualizar usuario

- Método: PUT
- URL: `/api/users/{id}`
- Descripción: Permite actualizar los datos de un usuario existente.
- Parámetros de solicitud:
  - `id` (obligatorio): Identificador único del usuario.
  - `entity` (obligatorio): Objeto JSON que contiene los datos actualizados del usuario.
- Respuesta exitosa:
  - Código de estado: 200 (OK)
  - Cuerpo de respuesta: Objeto JSON que representa al usuario actualizado.
- Posibles errores:
  - Código de estado: 400 (Bad Request)

### Iniciar sesión

- Método: POST
- URL: `/api/users/SignIn`
- Descripción: Permite a un usuario iniciar sesión en el sistema.
- Parámetros de solicitud:
  - `userSignIn` (obligatorio): Objeto JSON que contiene las credenciales de inicio de sesión del usuario.
- Respuesta exitosa:
  - Código de estado: 200 (OK)
  - Cuerpo de respuesta: Token de autenticación.
- Posibles errores:
  - Código de estado: 404 (Not Found)
  - Código de estado: 400 (Bad Request)

### Cerrar sesión

- Método: GET
- URL: `/api/users/LogOut`
- Descripción: Permite a un usuario cerrar sesión en el sistema.
- Respuesta exitosa:
  - Código de estado: 200 (OK)

Recuerda reemplazar `{id}` con el identificador real del usuario en los endpoints correspondientes.

### Tareas API

A continuación se describen los endpoints principales disponibles en la API de tareas. Para obtener información detallada sobre los parámetros de solicitud, las respuestas y los posibles errores, consulta la documentación completa en [URL de la documentación].

### Eliminar tarea

- Método: DELETE
- URL: `/api/taskitems/{id}`
- Descripción: Permite eliminar una tarea existente.
- Parámetros de solicitud:
  - `id` (obligatorio): Identificador único de la tarea.
- Respuesta exitosa:
  - Código de estado: 200 (OK)
- Posibles errores:
  - Código de estado: 404 (Not Found)

### Obtener todas las tareas

- Método: GET
- URL: `/api/taskitems`
- Descripción: Obtiene una lista de todas las tareas registradas.
- Respuesta exitosa:
  - Código de estado: 200 (OK)
  - Cuerpo de respuesta: Lista de objetos JSON que representan las tareas.

### Obtener tarea por ID

- Método: GET
- URL: `/api/taskitems/{id}`
- Descripción: Obtiene una tarea por su identificador único.
- Parámetros de solicitud:
  - `id` (obligatorio): Identificador único de la tarea.
- Respuesta exitosa:
  - Código de estado: 200 (OK)
  - Cuerpo de respuesta: Objeto JSON que representa la tarea.
- Posibles errores:
  - Código de estado: 404 (Not Found)

### Crear tarea

- Método: POST
- URL: `/api/taskitems`
- Descripción: Permite crear una nueva tarea en el sistema.
- Parámetros de solicitud:
  - `entity` (obligatorio): Objeto JSON que contiene los datos de la tarea a crear.
- Respuesta exitosa:
  - Código de estado: 201 (Created)
  - Cuerpo de respuesta: Objeto JSON que representa la tarea creada.
- Posibles errores:
  - Código de estado: 401 (Unauthorized)
  - Cuerpo de respuesta: Mensaje de error indicando que se requiere autenticación.

### Actualizar tarea

- Método: PUT
- URL: `/api/taskitems/{id}`
- Descripción: Permite actualizar los datos de una tarea existente.
- Parámetros de solicitud:
  - `id` (obligatorio): Identificador único de la tarea.
  - `entity` (obligatorio): Objeto JSON que contiene los datos actualizados de la tarea.
- Respuesta exitosa:
  - Código de estado: 200 (OK)
  - Cuerpo de respuesta: Objeto JSON que representa la tarea actualizada.
- Posibles errores:
  - Código de estado: 400 (Bad Request)

Recuerda reemplazar `{id}` con el identificador real de la tarea en los endpoints correspondientes.


## Licencia

Este proyecto se distribuye bajo la Licencia MIT. Para obtener más información, consulta el archivo [LICENSE](LICENSE).

## Contacto

Para consultas o comentarios sobre este proyecto, puedes ponerte en contacto con nosotros a través de sfainbinda@gmail.com
