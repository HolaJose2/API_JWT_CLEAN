API de Usuarios - Documentación

Uso de la API
    1. Ejecutar el archivo SQLScript.sql para crear la base de datos y poder usar la aplicacion.
    2. Debe crear un nuevo usuario para autenticarse y poder realizar las otras tareas con el token generado.

Creación de Usuario:
    Para crear un nuevo usuario, se debe hacer una solicitud HTTP POST al endpoint /api/usuario con los datos del usuario a crear en formato JSON. La contraseña debe cumplir con un mínimo de caracteres y el correo electrónico debe tener un formato válido.

    Ejemplo de solicitud:
        {
            "nombre": "NombreUsuario",
            "apellido": "ApellidoUsuario",
            "tipoDocumentoId": 1,
            "documento": 123456789,
            "clave": "contraseñaSegura123",
            "email": "correo@example.com"
        }

Autenticación de Usuario:
    Para autenticarse y obtener un token JWT válido, se debe hacer una solicitud HTTP POST al endpoint /api/autenticar con las credenciales de usuario en formato JSON.
    (El token tiene una duración de solo 2 minutos)
    
    Ejemplo de solicitud:
        {
            "email": "correo@example.com",
            "clave": "contraseñaSegura123"
        }

Características Principales:
 - Creación, edición y eliminación de usuarios.
 - Consulta de usuarios registrados.
 - Seguridad basada en tokens JWT.
 - Manejo de errores y logs integrados.

Arquitectura:
La API sigue una arquitectura limpia que separa claramente las capas de presentación, lógica de negocio y acceso a datos. 
Se han aplicado patrones de diseño como el patrón repositorio y DTO para mejorar la escalabilidad y mantenibilidad del código.

Próximos Pasos:
 - Documentación de la API utilizando herramientas como Swagger.
 - Desarrollo de pruebas unitarias para garantizar la fiabilidad de la aplicación.
 - Establecimiento de respuestas de API estandarizadas para mejorar la experiencia del usuario.
 - Implementar Refresh Token para generar nuevos tokens de acceso JWT sin necesidad de solicitar las credenciales de inicio de sesión nuevamente al usuario.
