Gestor de Productos AT

Programa de escritorio para administrar una tienda de abarrotes: inventario, ventas, proveedores, usuarios y reportes. Hecho por TIKITIKI TEAM.

COMO INICIAR

- Copia la carpeta completa del programa a un lugar como C:\GestorProductosAT (no en Program Files).
- Comprueba que GestorProductosAT.exe y GestorProductosAT.db estén en la misma carpeta.
- Abre GestorProductosAT.exe.
- Si Windows muestra "Windows protegió su PC", pulsa Más información y luego Ejecutar de todos modos.

USUARIOS INICIALES

- admin / 1234 (administrador)
- cajero1 / 1234 (cajero)

Cambia las contraseñas desde la sección Usuarios.

SI NO FUNCIONA

- No abre: copia la carpeta completa, no solo el .exe.
- Dice que no encuentra la base de datos: copia GestorProductosAT.db junto al .exe.
- No guarda datos o marca error de escritura: mueve la carpeta fuera de Program Files y quita "Solo lectura" al archivo .db.
- Dice "database is locked": cierra otros programas que tengan abierto el .db, por ejemplo DB Browser.
- Olvidaste una contraseña: con el programa cerrado, abre el .db con DB Browser y ejecuta UPDATE Usuario SET contrasena = '1234' WHERE usuario = 'admin'; Al abrir el programa, la protege sola.

RESPALDO

Los datos están en el archivo GestorProductosAT.db. Copia ese archivo de vez en cuando, con el programa cerrado.

TECNOLOGIAS

C# con Windows Forms (.NET 10) y SQLite.
