using example.domain.Entities;
using example.service.Features;
using example.service.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace example.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(UserCreateReq obj)
        {
            var userCreateModel = new CreateUser
            {
                UserName = obj.UserName,
                EmailAddress = obj.EmailAddress,
                DepartmentId = obj.DepartmentId
            };
            var result = await _mediator.Send(userCreateModel);
            return Ok(result);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<IActionResult> Update(UserUpdateReq obj)
        {
            var userUpdateModel = new UpdateUser
            {
                Id = obj.Id,
                UserName = obj.UserName,
                EmailAddress = obj.EmailAddress,
                DepartmentId = obj.DepartmentId
            };
            var result = await _mediator.Send(userUpdateModel);
            return Ok(result);
        }

        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetUserById { Id = id });
            return Ok(result.UserName);
        }
    }
}
