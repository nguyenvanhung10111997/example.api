using Amazon.SimpleNotificationService;
using Amazon.SQS;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using example.consumer.Configurations;
using example.consumer.Consumers;
using example.consumer.Features;
using example.consumer.Senders;
using example.infrastructure.Configurations;
using example.infrastructure.ContainerManager;
using example.service.Configurations;
using MassTransit;

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
            services.AddControllers();
            ConfigureAmazonSqs(services);

            services.AddHostedService<UserConsumerService>();
            services.AddHostedService<HeartBeatSender>();
        }

        //this method is called by the runtime. when use register use AutofacServiceProviderFactory in function startup in program.cs
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterServiceDependeny();
        }

        public void Configure(IApplicationBuilder app)
        {
            Engine.ContainerManager = new ContainerManager(app.ApplicationServices.GetAutofacRoot());
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(x => x.MapControllers());
        }

        private void ConfigureAmazonSqs(IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<HeartBeatConsumer>();

                x.UsingAmazonSqs((context, cfg) =>
                {
                    cfg.Host(new Uri(ApiConfig.Providers.AmazonSQS.Host), h => {
                        h.AccessKey(ApiConfig.Providers.AmazonSQS.AccessKey);
                        h.SecretKey(ApiConfig.Providers.AmazonSQS.SecretKey);
                        h.Config(new AmazonSimpleNotificationServiceConfig { ServiceURL = ApiConfig.Providers.AmazonSQS.ServiceURL });
                        h.Config(new Amazon.SQS.AmazonSQSConfig { ServiceURL = ApiConfig.Providers.AmazonSQS.ServiceURL });
                    });

                    cfg.ReceiveEndpoint("my-queue", e =>
                    {
                        //e.ConfigureConsumeTopology = false;

                        //e.Subscribe("event-topic", s =>
                        //{
                        //    s.TopicAttributes["DisplayName"] = "Public Event Topic";
                        //    s.TopicSubscriptionAttributes["some-subscription-attribute"] = "some-attribute-value";
                        //    s.TopicTags.Add("environment", "development");
                        //});
                        e.ConfigureConsumer<HeartBeatConsumer>(context);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });
        }
    }
}