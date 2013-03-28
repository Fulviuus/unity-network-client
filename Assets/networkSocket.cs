using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;

 

public class networkSocket : MonoBehaviour
{
    public String host = "localhost";
    public Int32 port = 50000;

    internal Boolean socket_ready = false;
    internal String input_buffer = "";
    TcpClient tcp_socket;
    NetworkStream net_stream;

    StreamWriter socket_writer;
    StreamReader socket_reader;



    void Update()
    {
        string received_data = readSocket();
        string key_stroke = Input.inputString;

        // Collects keystrokes into a buffer
        if (key_stroke != ""){
            input_buffer += key_stroke;

            if (key_stroke == "\n"){
            	// Send the buffer, clean it
            	Debug.Log("Sending: " + input_buffer);
            	writeSocket(input_buffer);
            	input_buffer = "";
            }

        }


        if (received_data != "")
        {
        	// Do something with the received data,
        	// print it in the log for now
            Debug.Log(received_data);
        }
    }


    void Awake()
    {
        setupSocket();
    }

    void OnApplicationQuit()
    {
        closeSocket();
    }

    public void setupSocket()
    {
        try
        {
            tcp_socket = new TcpClient(host, port);

            net_stream = tcp_socket.GetStream();
            socket_writer = new StreamWriter(net_stream);
            socket_reader = new StreamReader(net_stream);

            socket_ready = true;
        }
        catch (Exception e)
        {
        	// Something went wrong
            Debug.Log("Socket error: " + e);
        }
    }

    public void writeSocket(string line)
    {
        if (!socket_ready)
            return;
            
        line = line + "\r\n";
        socket_writer.Write(line);
        socket_writer.Flush();
    }

    public String readSocket()
    {
        if (!socket_ready)
            return "";

        if (net_stream.DataAvailable)
            return socket_reader.ReadLine();

        return "";
    }

    public void closeSocket()
    {
        if (!socket_ready)
            return;

        socket_writer.Close();
        socket_reader.Close();
        tcp_socket.Close();
        socket_ready = false;
    }

}