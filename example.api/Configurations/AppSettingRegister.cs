using example.infrastructure.Configurations;
using example.service.Configurations;

namespace example.api.Configurations
{
    public static class AppSettingRegister
    {
        public static void Binding(IConfiguration configuration)
        {
            ApiConfig.Common = new CommonConfig();
            configuration.Bind("CommonConfig", ApiConfig.Common);

            ApiConfig.Connection = new ConnectionStrings();
            configuration.Bind("ConnectionStrings", ApiConfig.Connection);

            ApiConfig.URLConnection = new URLConnectionConfig();
            configuration.Bind("URLConnectionConfig", ApiConfig.URLConnection);

            DbConnectionConfig.DefaultConnectionString = ApiConfig.Connection.DefaultConnectionString;
        }
    }
}
