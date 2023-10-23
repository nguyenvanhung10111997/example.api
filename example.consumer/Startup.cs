using Autofac;
using example.consumer.Configurations;
using example.consumer.Features;
using example.infrastructure.ContainerManager;
using example.service.Configurations;

namespace example.consumer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AppSettingRegister.Binding(configuration);
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabase();
            services.AddRepositories();
            services.AddMediator();
            services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddHostedService<UserConsumerService>();
        }
        //this method is called by the runtime. when use register use AutofacServiceProviderFactory in function startup in program.cs
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterServiceDependeny();

            var containerManager = Engine.ContainerManager;
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(x => x.MapControllers());
        }
    }
}
