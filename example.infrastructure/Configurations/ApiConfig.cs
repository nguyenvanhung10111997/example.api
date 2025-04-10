﻿namespace example.infrastructure.Configurations
{
    public class ApiConfig
    {
        public static CommonConfig Common;
        public static ConnectionStrings Connection;
        public static URLConnectionConfig URLConnection;
        public static ProviderConfig Providers;
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

    public class ProviderConfig
    {
        public RabbitMQConfig RabbitMQ { get; set; }
        public AmazonSQSConfig AmazonSQS { get; set; }
    }

    public class RabbitMQConfig
    {
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class AmazonSQSConfig
    {
        public string Host { get; set; }
        public string ServiceURL { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
    }
}
