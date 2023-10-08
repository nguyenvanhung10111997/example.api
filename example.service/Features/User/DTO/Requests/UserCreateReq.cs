namespace example.service.Features
{
    public class UserCreateReq
    {
        public required string UserName { get; set; }

        public string? EmailAddress { get; set; }

        public int DepartmentId { get; set; }
    }
}
