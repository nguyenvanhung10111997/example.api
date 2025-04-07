using MassTransit;

namespace example.consumer.Consumers
{
    public class HeartBeatConsumer : IConsumer<HeartBeat>
    {
        private readonly ILogger<HeartBeatConsumer> _logger;

        public HeartBeatConsumer(ILogger<HeartBeatConsumer> logger)
        {
            this._logger = logger;
        }

        public Task Consume(ConsumeContext<HeartBeat> context)
        {
            _logger.LogInformation("Received heartbeat with ID {Id} and timestamp {Timestamp}", context.Message.Identifier, context.Message.Timestamp);
            return Task.CompletedTask;
        }
    }

    public sealed record HeartBeat
    {
        public DateTimeOffset Timestamp { get; init; } = DateTimeOffset.UtcNow;
        public Guid Identifier { get; init; } = Guid.NewGuid();
    }
}
