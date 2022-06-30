using MockServer.Routes;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MockServer
{
    public class Program
    {

        /// <summary>
        /// The Host-Prefix of this running server
        /// </summary>
        public static string Host { get; private set; }

        /// <summary>
        /// Searches a handler for the incoming HttpRequest and tries to handle it.
        /// If successful the server returns the output-content of that handler.
        /// If the handler does not exist for the incoming request or fails to parse it, 
        /// the server closes the connection without sending any output data
        /// </summary>
        /// <param name="context">The HttpContext of the current connection</param>
        private static void HandleHttpRequest(HttpListenerContext context)
        {
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Received request: " + request.Url.ToString());

            string output = null;
            if(request.Url.Segments.Length > 1)
            { // If there is a handler in the segment header, search for it
                switch (Utilities.GetPathSegment(request.Url.Segments[1]))
                { // Decide which handler to use
                    case "router":
                        output = RouterRoute.HandleContext(context);
                        break;
                    case "pi":
                        output = PiRoute.HandleContext(context);
                        break;
                    case "schrank":
                        output = SchrankRoute.HandleContext(context);
                        break;
                    case "leinwand":
                        output = LeinwandRoute.HandleContext(context);
                        break;
                    case "batlights":
                        output = BatlightsRoute.HandleContext(context);
                        break;
                    default:
                        output = null;
                        break;
                }
            }

            try
            {
                if (output == null)
                { // If the handler was not found or fails, return status code 404
                    Console.WriteLine("Received unregistered path: " + request.Url.AbsolutePath);
                    response.StatusCode = 404;
                }
                else
                { // Otherwise respond with the output data of the handler
                    response.StatusCode = 200;
                    byte[] outputData = Encoding.UTF8.GetBytes(output);

                    Stream outputStream = response.OutputStream;
                    outputStream.Write(outputData, 0, outputData.Length);
                    outputStream.Flush();

                    Console.WriteLine($"Request accepted with response ({response.StatusCode}):\n{output}");
                }
            }
            finally
            {
                response.Close();
            }
        }

        static void Main(string[] args)
        {
            Host = args.Length > 0 ? args[0] : "http://127.0.0.1:8080/";
            Console.WriteLine($"Starting HttpListener on prefix: {Host}...");

            // Create and start the listener
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add(Host);
            listener.Start();

            try
            {
                Console.WriteLine("Waiting for connections...");

                while (true)
                {
                    // Get all incoming connections and handle them
                    HttpListenerContext context = listener.GetContext();
                    Task.Run(() => HandleHttpRequest(context));
                }
            }
            finally
            {
                Console.WriteLine("Stopping HttpListener...");
                listener.Close();
            }
        }

    }
}
