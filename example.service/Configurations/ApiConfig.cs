namespace example.service.Configurations
{
    public class ApiConfig
    {
        public static CommonConfig Common;
        public static ConnectionStrings Connection;
        public static URLConnectionConfig URLConnection;
    }

    public class CommonConfig
    {
    }

    public class ConnectionStrings
    {
        public string DefaultConnectionString { get; set; }
    }

    public class URLConnectionConfig
    {
        public string IDSUrl { get; set; }
    }
}
