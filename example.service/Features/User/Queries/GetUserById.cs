using example.domain.Entities;
using MediatR;

namespace example.service.Features
{
    public class GetUserById : IRequest<User?>
    {
        public required int Id { get; set; }
    }
}
