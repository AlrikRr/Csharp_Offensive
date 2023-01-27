using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerSocket
{
    internal class Program
    {
        static void Main(string[] args)
        {

            /*
           IPHostEntry hostInfo = Dns.GetHostEntry("localhost");

           IPAddress[] ips = hostInfo.AddressList;

           foreach(IPAddress ip in ips)
           {
               Console.WriteLine(ip.ToString());
           }

           */
            int BUFFER_SIZE = 2048;
            // Create Port Endpoint 1337
            IPEndPoint ip_endp = new IPEndPoint(IPAddress.Any, 1337);
            // Create server socket on 0.0.0.0
            Socket server_socket = new Socket(IPAddress.Any.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Bind Socket with endpoint port 1337
            server_socket.Bind(ip_endp);

            // Listen mode, waiting connection from client
            server_socket.Listen(5);

            // Once connected, create client socket
            Socket client_socket = server_socket.Accept();
            Console.WriteLine("[+] Connection from {0}", client_socket.RemoteEndPoint);

            // Send message to client, msg needs to be byte array
            Console.Write("Enter msg: ");
            string input_msg = Console.ReadLine();
            // convert msg to byte array
            client_socket.Send(Encoding.ASCII.GetBytes(input_msg));

            while (input_msg != "exit")
            {
                //Receive client data
                byte[] cl_msg_byte = new byte[BUFFER_SIZE];
                Array.Clear(cl_msg_byte, 0, cl_msg_byte.Length);
                client_socket.Receive(cl_msg_byte);
                Console.WriteLine("From Client: {0}",Encoding.ASCII.GetString(cl_msg_byte).TrimEnd('\0'));

                //Send to client
                Console.Write("Enter msg: ");
                input_msg = Console.ReadLine();
                client_socket.Send(Encoding.ASCII.GetBytes(input_msg));

            }

            // close connections
            client_socket.Close();
            Console.WriteLine("[+] Client Socket -- Close");
            server_socket.Close();
            Console.WriteLine("[+] Server Socket -- Close");
        }
    }
}

