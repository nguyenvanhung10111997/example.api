namespace example.service.Features
{
    public class UserUpdateReq
    {
        public required int Id { get; set; }

        public required string UserName { get; set; }

        public string? EmailAddress { get; set; }

        public int DepartmentId { get; set; }
    }
}
