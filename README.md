# ProductCatalogChallenge

Este proyecto es una solución para el desafío del catálogo de productos. Proporciona una API para gestionar productos, descuentos, precios finales y existencias de inventario.

## Características

- Gestión de productos: Crear, leer, actualizar y eliminar productos.
- Gestión de descuentos: Definir descuentos para productos. (TODO)
- Cálculo de precios finales: Calcular el precio final de un producto aplicando un descuento. (TODO)
- Gestión de inventario: Controlar el stock disponible de productos. (TODO)

## Tecnologías Utilizadas

- .Net 8: Framework para el desarrollo de la API REST.
- Entity Framework Core: ORM (Mapeo Objeto-Relacional) para interactuar con la base de datos.
- SQLite: Base de datos ligera utilizada para almacenar los datos.
- xUnit: Framework de pruebas unitarias para realizar pruebas automatizadas.

## Estructura del Proyecto

La solución se organiza en las siguientes carpetas:

- `ProductCatalogChallenge.Api`: Proyecto de API REST que contiene los controladores y la configuración de enrutamiento.
- `ProductCatalogChallenge.Application`: Capa de lógica de aplicación que gestiona los casos de uso y operaciones de negocio.
- `ProductCatalogChallenge.Domain`: Definición de modelos de dominio que representan los conceptos del negocio.
- `ProductCatalogChallenge.Infraestructure`: Implementaciones concretas de la infraestructura, como repositorios y contextos de base de datos.
- `ProductCatalogChallenge.Tests`: Pruebas unitarias para asegurar la calidad del código.

## Configuración

### Base de Datos

El proyecto utiliza SQLite como base de datos. La cadena de conexión a la base de datos se encuentra en el archivo `appsettings.json` del proyecto `ProductCatalogChallenge.Api`. Asegúrate de que la cadena de conexión sea válida para tu entorno de desarrollo.

### Migraciones de Base de Datos

Para aplicar las migraciones de base de datos, sigue estos pasos:

1. Abre una terminal en el directorio raíz del proyecto.
2. Ejecuta el siguiente comando para aplicar las migraciones:

```bash
dotnet ef database update --project ProductCatalogChallenge.Infraestructure --startup-project ProductCatalogChallenge.Api
