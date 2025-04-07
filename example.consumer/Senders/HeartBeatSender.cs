using example.consumer.Consumers;
using MassTransit;

namespace example.consumer.Senders
{
    public class HeartBeatSender : IHostedService
    {
        private readonly IBus _messageBus;
        private readonly ILogger<HeartBeatSender> _logger;

        public HeartBeatSender(IBus messageBus, ILogger<HeartBeatSender> logger)
        {
            this._messageBus = messageBus;
            this._logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var heartBeat = new HeartBeat();
                    await _messageBus.Publish(new HeartBeat(), cancellationToken);
                    _logger.LogInformation("Sent heartbeat {Timestamp}", heartBeat.Timestamp);
                    await Task.Delay(TimeSpan.FromSeconds(3), cancellationToken);
                }
                catch (TaskCanceledException)
                {
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
