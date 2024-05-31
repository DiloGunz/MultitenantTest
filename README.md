# CLEAN ARCHITECTURE - MULTITENANT

Este proyecto establece de forma din�mica la conexion a base de datos de productos dependiendo de la organizacion a la que pertenece el usuario logueado

## Requisitos

- .Net 8
- SQL Server
- Visual Studio 2022

## Instalaci�n

Pasos para instalar el proyecto:

1. Clonar el repositorio: `git clone https://github.com/DiloGunz/MultitenantTest.git`
2. Abrir la soluci�n en visual studio.
3. Restaurar los paquetes NuGet.
4. Configurar la cadena de conexi�n a la base de datos en `appsettings.json`.
	- En `appsettings.json` se encuentra 3 cadenas de conexion
	- `Company`: Cadena de conexi�n para Organizaciones y Usuarios
	- `MainCatalog`: Es el cadena de conexi�n de Catalog por defecto donde est� la tabla `Products`.
	- `TenantCatalog`: Es la cadena de conexi�n que servir� para las migraciones din�micas de la base de datos de `Catalog`
5. Se debe aplicar las migraciones seleccionando el proyecto `Infraestructure`:
	- update-database -context CompanyDbContext
	- update-database -context CatalogDbContext
	- Con esto se crea la base de datos `Company` y `MAINCatalogDb`

## Uso

1. Al ejecutar la aplicacion se crea los siguientes datos por defecto:
	- Usuario: `user = root | password = User@123`
	- Organization: `MAIN`

2. Para loguearse, ir a la url `/api/Identity/authentication` y escribir las credenciales por defecto
3. El resultado ser� un JWT, el cual servir� para autenticarse a los demas endpoints
4. Al crear una organizaci�n tambi�n se crea una Base de datos con el prefijo de la propiedad `TenantIdentifier` de la tabla `Organzation`
	- Ejemplo: `TENANTCatalogDb`
5. Al crear un usuario y asociarlo a una organizaci�n, �ste usuario solo podr� acceder a los registros de la base de datos `Catalog` que le corrresponde a su organizacion

