namespace StrongBeaver.Showroom.View.WebContent
{
    public class LocalWebContentDescription : IWebContentDescription
    {
        private static string pathPrefix = string.Empty;

        public string Title { get; set; }

        public string PageName { get; set; }

        public string Path => pathPrefix + "/" + PageName;

        public static void SetStaticPathPrefix(string prefix)
        {
            pathPrefix = prefix;
        }
    }
}
