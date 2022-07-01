using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace MockServer.Routes
{
    public class BatlightsRoute
    {

        private static readonly Random RANDOM = new Random();

        public static string HandleContext(HttpListenerContext context)
        {
            string[] segments = context.Request.Url.Segments;
            if (segments.Length <= 2) // (/ or /leinwand)
            {
                // Generate a possible status request
                StringBuilder sb = new StringBuilder("{\r\n");
                sb.Append("  \"Animation\": \"single\",\r\n");
                sb.Append("  \"Speed\": \"").Append(RANDOM.Next(0, 1000)).Append("\",\r\n");
                sb.Append("  \"Color\": \"").Append(RANDOM.Next(0x000000, 0xFFFFFF)).Append("\"\r\n");
                sb.Append("}");
                return sb.ToString();
            }
            else
            { // (/batlights/.../)
                string output;
                switch (Utilities.GetPathSegment(segments[2]))
                {
                    case "color":
                    case "move":
                        output = Handle1IntArg(context);
                        break;
                    case "colorCode":
                        output = HandleColorCode(context);
                        break;
                    case "animation":
                        output = HandleAnimation(context);
                        break;
                    case "speedCode":
                        output = HandleSpeedCode(context);
                        break;
                    default:
                        output = null;
                        break;
                }

                return output;
            }
        }

        private static string HandleSpeedCode(HttpListenerContext context)
        { // Next UrlSegment Index = 3
            string[] segments = context.Request.Url.Segments;
            if (segments.Length < 4)
            {
                Console.WriteLine("Missing parameters");
                return null;
            }

            string code = segments[3];
            switch (code)
            {
                case "turtle":
                case "veryslow":
                case "slow":
                case "medium":
                case "fast":
                case "veryfast":
                case "max":
                    break;

                default:
                    Console.WriteLine("Invalid parameter (code - Expected: {turtle, veryslow, slow, medium, fast, veryfast, max}): " + segments[3]);
                    return null;
            }

            return "<html><head><title>Success!</title></head><body><p>Success!</p></body></html>";
        }

        private static string HandleAnimation(HttpListenerContext context)
        { // Next UrlSegment Index = 3
            string[] segments = context.Request.Url.Segments;
            if (segments.Length < 4)
            {
                Console.WriteLine("Missing parameters");
                return null;
            }

            string code = segments[3];
            switch (code)
            {
                case "single":
                case "kitt":
                case "standby":
                case "invert":
                case "all":
                    break;

                default:
                    Console.WriteLine("Invalid parameter (animation - Expected: {single, kitt, standby, invert, all}): " + segments[3]);
                    return null;
            }

            return "<html><head><title>Success!</title></head><body><p>Success!</p></body></html>";
        }

        private static string HandleColorCode(HttpListenerContext context)
        { // Next UrlSegment Index = 3
            string[] segments = context.Request.Url.Segments;
            if (segments.Length < 4)
            {
                Console.WriteLine("Missing parameters");
                return null;
            }

            string code = segments[3];
            switch(code)
            {
                case "red":
                case "green":
                case "blue":
                case "yellow":
                case "cyan":
                case "magenta":
                case "black":
                case "white":
                    break;

                default:
                    Console.WriteLine("Invalid parameter (code - Expected: {red, green, blue, yellow, cyan, magenta, black, white}): " + segments[3]);
                    return null;
            }

            return "<html><head><title>Success!</title></head><body><p>Success!</p></body></html>";
        }

        private static string Handle1IntArg(HttpListenerContext context)
        { // Next UrlSegment Index = 3
            string[] segments = context.Request.Url.Segments;
            if (segments.Length < 4)
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
                Console.WriteLine("Invalid parameter (color - Expected: int): " + segments[3]);
                return null;
            }

            return "<html><head><title>Success!</title></head><body><p>Success!</p></body></html>";
        }

    }
}
