Unity TCP Network Client
========================

This is a simple TCP client for the Unity 3D platform, written in C# using .NET sockets.

What is it for?
---------------

This is just a starter script for more complex networking applications.

How does it work?
-----------------

The package comes with a simple echo server, written in python.
The server runs on port 50000, it will echo back packets received and will close the connection when receiving a "quit" command.

Execute the server, load the "network" scene included in the Unity package and run it.

The program will start collecting keystrokes, and when enter is pressed it will send the string to the server.
When received, the server will reply accordingly.

All the string transactions can be monitored in the Unity console window, since no GUI is present.

If a "quit" command is submitted, the server will close the connection, and further packet requests will return an exception (the socket has been closed)
