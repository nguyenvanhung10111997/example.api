using example.domain.Entities;
using example.domain.Interfaces;
using example.service.Interfaces;

namespace example.service.Implements
{
    internal class UserService : BaseService, IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(Lazy<IUnitOfWork> unitOfWork) : base(unitOfWork)
        {
            _userRepository = unitOfWork.Value.GetRepository<User>();
        }

        public async Task<bool> CreateAsync(User userModel)
        {
            try
            {
                var user = await _userRepository.AddAsync(userModel);
                var executeResult = await UnitOfWork.SaveChangesAsync();

                return executeResult > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateAsync(User userModel)
        {
            try
            {
                var user = await _userRepository.UpdateAsync(userModel);
                var executeResult = await UnitOfWork.SaveChangesAsync();

                return executeResult > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User?> GetByIdAsync(int userId)
        {
            try
            {
                var userQuery = await _userRepository.GetAsync(x => x.Id == userId);
                var userResult = userQuery.FirstOrDefault();

                return userResult;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
