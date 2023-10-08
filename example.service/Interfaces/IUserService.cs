using example.domain.Entities;

namespace example.service.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<bool> CreateAsync(User userModel);
        Task<bool> UpdateAsync(User userModel);
        Task<User?> GetByIdAsync(int userId);
    }
}
