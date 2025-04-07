using example.domain.Entities;
using example.infrastructure.RabbitMQ;
using MediatR;

namespace example.service.Features
{
    internal class CreateUserHandler : IRequestHandler<CreateUser, bool>
    {
        private readonly IRabbitMQProducer _rabbitMQProducer;

        public CreateUserHandler(IRabbitMQProducer rabbitMQProducer)
        {
            _rabbitMQProducer = rabbitMQProducer;
        }

        public async Task<bool> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                UserName = request.UserName,
                EmailAddress = request.EmailAddress,
                DepartmentId = request.DepartmentId
            };

            _rabbitMQProducer.SendMessage(user, nameof(user));

            return await Task.FromResult(true);
        }
    }

    public class CreateUser : IRequest<bool>
    {
        public required string UserName { get; set; }

        public string? EmailAddress { get; set; }

        public int DepartmentId { get; set; }
    }
}
