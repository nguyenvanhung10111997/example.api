﻿using example.domain.Entities;
using example.service.Interfaces;
using MediatR;

namespace example.service.Features
{
    internal class UpdateUserHandler : IRequestHandler<UpdateUser, bool>
    {
        private readonly IUserService _userService;

        public UpdateUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(UpdateUser request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Id = request.Id,
                UserName = request.UserName,
                EmailAddress = request.EmailAddress,
                DepartmentId = request.DepartmentId
            };

            return await _userService.UpdateAsync(user);
        }
    }

    public class UpdateUser : IRequest<bool>
    {
        public required int Id { get; set; }

        public required string UserName { get; set; }

        public string? EmailAddress { get; set; }

        public int DepartmentId { get; set; }
    }
}
