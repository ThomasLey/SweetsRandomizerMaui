using System.Net;

namespace MockServer.Routes
{
    public class PiRoute
    {

        public static string HandleContext(HttpListenerContext context)
        {
            return "<html><head><title>Here i am!</title></head><body><p>Here i am!</p></body></html>";
        }

    }
}
