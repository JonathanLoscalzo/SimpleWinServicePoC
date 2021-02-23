# Introduction 
Simple windows service using Topshelf for installing

# Getting Started

## Develop
- Colocar en vs el proyecto de inicio Winservice
- Colocar el breakpoint y va a detenerse donde querramos (no es necesario attachear)

## Install
- Buildear el proyecto con la configuración necesaria (App.config + appsettings.json)
- Ir a la carpeta del build: /bin/<Debug ó Release>
- Ejecutar en una Developer Command Prompt en administrador el siguiente comando

```
Winservice.exe install --autostart
```

En caso de querer modificar parámetros especiales al servicio, por ejemplo: 
```
Winservice.exe install --autostart -servicename Winservice -displayname Winservice
```

Para ver toda la ayuda y  parámetros disponibles:
```
Winservice.exe help
```

Luego para darle arrancar el servicio: 
```
sc start Winservice #ó el nombre que hayan colocado, según App.config será AnotherCoolService 
```

Revisar el eventviewer en Application y ver si se ejecutan los servicios. 

### Debugging Remoto
Para el debugging remoto tenemos 2 opciones:

- Instalar el servicio y attachear al proceso
- La 2da opción puede ser descomentar el código del Program.Main()
```
// TODO: use args to force attach a process to continue...
//while (!Debugger.IsAttached)
//{
//    Thread.Sleep(1000);
//}
```

Este chunk de código espera a que el attach del proceso se realice para continuar, luego pueden poner un breakpoint en la siguiente linea. 

NOTA: la primera opción se detiene donde se encuentra la ejecución y quizás la segunda es más interesante para debuggear.  
NOTA2: el TODO aclara que podrían pasar por parámetro una variable "need_debug_attached:True" o similar  y evaluarla con un condicional para ejecutar el while. 

## Uninstall
Para desinstalar:
```
Winservice.exe uninstall 
```

En caso que lo hayan instalado con otro nombre: 
```
Winservice.exe uninstall -servicename Winservice -displayname Winservice

```

Y la otra opción es usando directamente el comando ```sc```
```
sc stop Winservice # o el nombre del servicio que hayan colocado, puede ser AnotherCoolService
sc delete Winservice # o el nombre del servicio que hayan colocado, puede ser AnotherCoolService
```
