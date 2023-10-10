using example.domain.Entities;
using example.infrastructure.RabbitMQ;
using example.service.Interfaces;
using MediatR;

namespace example.service.Features
{
    internal class GetUserByIdHandler : IRequestHandler<GetUserById, User?>
    {
        private readonly IUserService _userService;
        private readonly IRabbitMQProducer _rabbitMQProducer;

        public GetUserByIdHandler(IUserService userService,
            IRabbitMQProducer rabbitMQProducer)
        {
            _userService = userService;
            _rabbitMQProducer = rabbitMQProducer;
        }

        public async Task<User?> Handle(GetUserById request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetByIdAsync(request.Id);
            _rabbitMQProducer.SendProductMessage(user.UserName);
            return user;
        }
    }

    public class GetUserById : IRequest<User?>
    {
        public required int Id { get; set; }
    }
}
