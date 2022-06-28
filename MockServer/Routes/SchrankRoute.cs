using System;
using System.Net;
using System.Text;

namespace MockServer.Routes
{
    internal class SchrankRoute
    {

        private static readonly Random RANDOM = new Random();

        public static string HandleContext(HttpListenerContext context)
        {
            string[] segments = context.Request.Url.Segments;
            if (segments.Length <= 2) // (/ or /schrank)
            {
                // Generate possible status request
                StringBuilder sb = new StringBuilder("{\r\n");
                for(int i = 0; i < 100; i++)
                {
                    int color = RANDOM.Next(0x000000, 0xFFFFFF);
                    sb.Append("  \"").Append(i).Append("\": ").Append(color).Append(",\r\n");
                }

                sb.Append("  \"mdns\": \"").Append(Program.Host).Append("\",\r\n");
                sb.Append("  \"backgroundColor\": \"").Append(RANDOM.Next(0x000000, 0xFFFFFF)).Append("\"\r\n");
                sb.Append("}");
                return sb.ToString();
            }
            else
            { // (/schrank/.../)
                string output;
                switch (Utilities.GetPathSegment(segments[2]))
                {
                    case "segment":
                    case "xsegment":
                        output = HandleSegment(context);
                        break;
                    case "clear":
                    case "off":
                        output = HandleClearOrOff(context);
                        break;
                    case "on":
                    case "setBackground":
                        output = HandleOnOrSetBackground(context);
                        break;
                    default:
                        output = null;
                        break;
                }

                return output;
            }
        }

        private static string HandleSegment(HttpListenerContext context)
        { // Next UrlSegment Index = 3
            string[] segments = context.Request.Url.Segments;
            if (segments.Length < 6)
            {
                Console.WriteLine("Missing parameters");
                return null;
            }
            else if (Utilities.GetPathSegment(segments[4]) != "color")
            {
                Console.WriteLine("Invalid parameter (color - Expected: existant): " + segments[4]);
                return null;
            }

            try
            {
                // Check if id argument is an integer
                int.Parse(Utilities.GetPathSegment(segments[3]));
            }
            catch
            {
                Console.WriteLine("Invalid parameter (id - Expected: int): " + segments[3]);
                return null;
            }

            try
            {
                // Check if color argument is an integer
                int.Parse(Utilities.GetPathSegment(segments[5]));
            }
            catch
            {
                Console.WriteLine("Invalid parameter (color - Expected: int): " + segments[5]);
                return null;
            }

            return "<html><head><title>Success!</title></head><body><p>Success!</p></body></html>";
        }

        private static string HandleClearOrOff(HttpListenerContext context)
        { // Next UrlSegment Index = 3
            return "<html><head><title>Success!</title></head><body><p>Success!</p></body></html>";
        }

        private static string HandleOnOrSetBackground(HttpListenerContext context)
        { // Next UrlSegment Index = 3
            string[] segments = context.Request.Url.Segments;
            if (segments.Length < 4)
            {
                Console.WriteLine("Missing parameters");
                return null;
            }

            try
            {
                // Check if color argument is an integer
                int.Parse(Utilities.GetPathSegment(segments[3]));
            }
            catch
            {
                Console.WriteLine("Invalid parameter (color - Expected: int): " + segments[3]);
                return null;
            }

            return "<html><head><title>Success!</title></head><body><p>Success!</p></body></html>";

        }

    }
}
