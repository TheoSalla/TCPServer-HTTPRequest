using System.Net.Security;
using System.Net.Sockets;
using System.Text;

namespace TcpClientExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Connect to the server
            TcpClient client = new TcpClient("dummyjson.com", 443);
            System.Console.WriteLine(client.Connected);

            // Get the network stream
            NetworkStream stream = client.GetStream();
            System.Console.WriteLine(client.Connected);
            // Create the SSL stream
            SslStream sslStream = new SslStream(stream);

            // Authenticate the client
            sslStream.AuthenticateAsClient("dummyjson.com");

            // Send the HTTP request
            string request = "GET /products/1 HTTP/1.1\r\nHost: dummyjson.com\r\n\r\n";
            byte[] requestBytes = Encoding.ASCII.GetBytes(request);
            sslStream.Write(requestBytes, 0, requestBytes.Length);

            // Read the HTTP response
            byte[] responseBytes = new byte[1024];
            int bytesRead = sslStream.Read(responseBytes, 0, responseBytes.Length);
            string response = Encoding.ASCII.GetString(responseBytes, 0, bytesRead);

            // Print the response
            Console.WriteLine(response);

            // Close the connection
            sslStream.Close();
            stream.Close();
            client.Close();
        }
    }
}
