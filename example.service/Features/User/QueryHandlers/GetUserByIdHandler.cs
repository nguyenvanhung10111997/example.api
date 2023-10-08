using example.domain.Entities;
using example.service.Interfaces;
using MediatR;

namespace example.service.Features
{
    internal class GetUserByIdHandler : IRequestHandler<GetUserById, User?>
    {
        private readonly IUserService _userService;

        public GetUserByIdHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<User?> Handle(GetUserById request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetByIdAsync(request.Id);

            return user;
        }
    }
}
