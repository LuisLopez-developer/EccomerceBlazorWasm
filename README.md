## Cómo empezar
Esta plantilla está diseñada para ayudarte a configurar rápidamente un proyecto Blazor con Flowbite. Si deseas conocer más sobre el proceso y entender los pasos en mayor profundidad, puedes consultar este enlace: https://flowbite.com/docs/getting-started/blazor/

Para obtener la plantilla, solo sigue estas simples instrucciones:

1. Clona el repositorio en una carpeta local en tu máquina.
1. Abre el proyecto en un editor de texto.
1. En una ventana de terminal, ejecuta `npm install`, esto descargará los módulos de Node necesarios para la plantilla.
1. Cuando termine, en la ventana de terminal, ejecuta `npx tailwindcss -i wwwroot/css/app.css -o wwwroot/css/app.min.css --watch`

> [!important]
> El último paso se utiliza para iniciar el compilador de **Tailwind CSS**. Si no has cambiado nada en la estructura del proyecto, no te preocupes por esta nota. Pero en caso de que lo hayas hecho, asegúrate de especificar la ruta correcta para el archivo CSS de entrada y enlazar el archivo CSS de salida generado después de ejecutar este comando. Los cambios deben realizarse dentro de **index.html**, que se encuentra en **/wwwroot**.

## Creditos especiales
A: [**Rasheed K Mozaffar**](https://github.com/rasheed-k-mozaffar/FlowbiteBlazorWasmStarter) 

Por: La base del proyecto, con las modificaciones de Blazor para utilizar [**Tailwind CSS**](https://tailwindcss.com/) junto con [**Flowbite**](https://flowbite.com/).
