using example.domain.Entities;
using example.service.Interfaces;
using MediatR;

namespace example.service.Features
{
    internal class CreateUserHandler : IRequestHandler<CreateUser, bool>
    {
        private readonly IUserService _userService;

        public CreateUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                UserName = request.UserName,
                EmailAddress = request.EmailAddress,
                DepartmentId = request.DepartmentId
            };

            return await _userService.CreateAsync(user);
        }
    }
}
