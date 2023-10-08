using MediatR;

namespace example.service.Features
{
    public class CreateUser : IRequest<bool>
    {
        public required string UserName { get; set; }

        public string? EmailAddress { get; set; }

        public int DepartmentId { get; set; }
    }
}
