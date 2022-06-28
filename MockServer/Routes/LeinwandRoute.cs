using System;
using System.Net;
using System.Text;

namespace MockServer.Routes
{
    public class LeinwandRoute
    {

        private static readonly Random RANDOM = new Random();

        public static string HandleContext(HttpListenerContext context)
        {
            string[] segments = context.Request.Url.Segments;
            if (segments.Length <= 2) // (/ or /leinwand)
            {
                // Generate a possible status request
                StringBuilder sb = new StringBuilder("{\r\n");
                for (int i = 0; i < 100; i++)
                {
                    int color = RANDOM.Next(0x000000, 0xFFFFFF);
                    sb.Append("  \"").Append(i).Append("\": ").Append(color).Append(",\r\n");
                }

                sb.Append("  \"mdns\": \"").Append(Program.Host).Append("\",\r\n");
                sb.Append("  \"speed\": \"").Append(RANDOM.Next(0, 100)).Append("\",\r\n");
                sb.Append("  \"background\": \"").Append(RANDOM.Next(0x000000, 0xFFFFFF)).Append("\",\r\n");
                sb.Append("  \"foreground\": \"").Append(RANDOM.Next(0x000000, 0xFFFFFF)).Append("\",\r\n");
                sb.Append("  \"direction\": \"").Append(RANDOM.Next(0, 2) == 1 ? 1 : -1).Append("\",\r\n");
                sb.Append("  \"pps\": \"").Append(RANDOM.Next(10, 20)).Append("\",\r\n");
                sb.Append("  \"nos\": \"").Append(RANDOM.Next(10, 20)).Append("\",\r\n");
                sb.Append("  \"animation\": \"").Append("spin").Append("\"\r\n");
                sb.Append("}");
                return sb.ToString();
            }
            else
            { // (/leinwand/.../)
                string output;
                switch (Utilities.GetPathSegment(segments[2]))
                {
                    case "speed":
                    case "highlight":
                    case "on":
                    case "setBackground":
                    case "setForeground":
                    case "setPps":
                    case "setNos":
                    case "setDirection":
                        // Output and parsing is the same for every request
                        output = Handle1IntArgument(context);
                        break;
                    case "section":
                        output = HandleSection(context);
                        break;
                    case "off":
                        output = HandleOff(context);
                        break;
                    default:
                        output = null;
                        break;
                }

                return output;
            }
        }

        private static string HandleSection(HttpListenerContext context)
        { // Next UrlSegment Index = 3
            string[] segments = context.Request.Url.Segments;
            if (segments.Length < 5)
            {
                Console.WriteLine("Missing parameters");
                return null;
            }

            try
            {
                // Check if start argument is an integer
                int.Parse(Utilities.GetPathSegment(segments[3]));
            }
            catch
            {
                Console.WriteLine("Invalid parameter (start - Expected: int): " + segments[3]);
                return null;
            }

            try
            {
                // Check if end argument is an integer
                int.Parse(Utilities.GetPathSegment(segments[4]));
            }
            catch
            {
                Console.WriteLine("Invalid parameter (end - Expected: int): " + segments[3]);
                return null;
            }

            return "<html><head><title>Success!</title></head><body><p>Success!</p></body></html>";
        }

        private static string HandleOff(HttpListenerContext context)
        { // Next UrlSegment Index = 3
            return "<html><head><title>Success!</title></head><body><p>Success!</p></body></html>";
        }

        private static string Handle1IntArgument(HttpListenerContext context)
        { // Next UrlSegment Index = 3
            string[] segments = context.Request.Url.Segments;
            if(segments.Length < 4)
            {
                Console.WriteLine("Missing parameters");
                return null;
            }

            try
            {
                // Check if input argument is an integer
                int.Parse(Utilities.GetPathSegment(segments[3]));
            }
            catch
            {
                Console.WriteLine("Invalid parameter (arg0 - Expected: int): " + segments[3]);
                return null;
            }

            return "<html><head><title>Success!</title></head><body><p>Success!</p></body></html>";
        }

    }
}
