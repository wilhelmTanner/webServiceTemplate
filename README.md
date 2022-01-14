# :star2: Agregar título (*ej: Api para la gestión de...*) :star2:
---
Agregar descripción del proyecto (*ej:Proyecto para gestionar las publicaciones de los clientes morosos en Equifax y Sinacofi.*)

## Funcionalidades
- Revisar el estado ...
- Descargar los archivos...
- etc...


## Equipo (*indicar integrantes de equipo*)

- [Nombre Integrante](mailto:emailintegrante@tanner.cl) (Cargo en el proyecto, *ej: PM*)

## Herramientas

- [ASP.NET Core v6.0](https://dotnet.microsoft.com/download/dotnet-core/6.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/es/downloads/)
- [Docker](https://www.docker.com/) (opcional)
- [Visual Studio Code](https://code.visualstudio.com/) (opcional)
- Etc...


## Documentación (*Entregar un poco mas de información sobre el proyecto, que hace?, con quien se integra?*)

*Ej: Este repositorio consiste de una WebApi que expone los endpoints necesarios para supervisar...*

*Existen funcionalidades del sistema que utilizan las siguientes apis:*

- [Tanner Email](https://apimtanner-dev.azure-api.net/tanner-email/swagger/index.html).
- [Tracer Logger](https://aks-dev.tanner.cl/corporate/tracer-logger/api/swagger/index.html).

*El servicio cuenta con un endpoint `/health` para verificar que se encuentra disponible.*

## Release (*Entregar información pertinente a la publicación de la solución*)

*Ej: Para desplegar el aplicativo en los diferentes ambientes (Dev, QA ó Producción), debe tener en cuenta los siguientes cambios en el archivo `appsettings.json`:*

1. Configurar los niveles de logging a `Information`.

    ```json
    "Logging": {
        "LogLevel": {
          "Default": "Information"
        }
      }
    ```
	
2. Introducir el valor del *Instrumentation Key* del *Application Insights*.

    ```json
    "ApplicationInsights": {
        "InstrumentationKey": "12312312-1231-1231-1231-123123123123"
    }
    ```

3. Configurar la url y credenciales de las apis que utiliza (Tanner Email, Tracer Logger). Ejemplo:

    ```json
    "RestClientConfiguration": {
        "IntegrationClient": {
          "Url": "https://aks-dev.tanner.cl/...",
          "OcpApimSubscriptionKey": "<subscription-key>",
          "ApiVersion": "<version>",
          "Retry": 3,
          "TimeoutRetry": 2
        }
    },
    ```

4. Configurar la cadena de conexión del servidor de MongoDb. Ejemplo:

    ```json
    "MongoDbConfiguration": {
        "Uri": "mongodb+srv://...",
        "DatabaseName": "dicom-publications",
        "DelinquentDebtCollectionName": "delinquent-debt"
     }
    ```

> [!Tip]
> Tener en cuenta que tanto el InstrumentationKey, las credenciales de apis, y el usuario y contraseña de la cadena de conexión a MongoDb deben ser secretos y obtenidos desde el AKV.