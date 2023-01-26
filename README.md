# EjercicioPOO
El problema:
Tenemos un método que genera un reporte en base a una colección de formas geométricas, procesando algunos datos para presentar información extra. La firma del método es:

public static string Imprimir(List<FormaGeometrica> formas, int idioma)
Al mismo tiempo, encontramos muy díficil el poder agregar o bien una nueva forma geométrica, o imprimir el reporte en otro idioma. Nos gustaría poder dar soporte para que el usuario pueda agregar otros tipos de formas u obtener el reporte en otros idiomas, pero extender la funcionalidad del código es muy doloroso. ¿Nos podrías dar una mano a refactorear la clase FormaGeometrica? Dentro del código encontrarás un TODO con nuevos requerimientos a satisfacer una vez completada la refactorización.

Acompañando al proyecto encontrarás una serie de tests unitarios (librería NUnit) que describen el comportamiento del método Imprimir. Se puede modificar cualquier cosa del código y de los tests, con la única condición que los tests deben pasar correctamente al entregar la solución.

Se agradece también la inclusión de nuevos tests unitarios para validar el comportamiento de la nueva funcionalidad agregada.

Cómo funciona
Lo que te encontrás al levantar la .sln es una librería con el objeto de negocio FormaGeometrica, y un pequeño proyecto con test unitarios sobre el método de impresión de reporte.

La resolución es libre y cómo encarar el problema queda en el criterio de quien lo resuelva!
