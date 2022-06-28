namespace MockServer
{
    internal class Utilities
    {
        
        /// <summary>
        /// Fix segment path.
        /// URL path-elements sometimes consist of trailing '/'.
        /// This method removes them if they exist.
        /// </summary>
        /// <param name="path">The path-element to fix</param>
        /// <returns>A fully cleaned path-element with no trailing '/'</returns>
        public static string GetPathSegment(string path)
        {
            if (path.EndsWith("/"))
                path = path.Substring(0, path.Length - 1);
            return path;
        }

    }
}
