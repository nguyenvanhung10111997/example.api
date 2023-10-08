using example.domain.Entities;
using example.domain.Interfaces;
using example.service.Interfaces;

namespace example.service.Implements
{
    internal class DepartmentService : BaseService, IDepartmentService
    {
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Salary> _salaryRepository;

        public DepartmentService(Lazy<IUnitOfWork> unitOfWork) : base(unitOfWork)
        {
            _departmentRepository = unitOfWork.Value.GetRepository<Department>();
            _userRepository = unitOfWork.Value.GetRepository<User>();
            _salaryRepository = unitOfWork.Value.GetRepository<Salary>();
        }

        public async Task<bool> CreateAsync()
        {
            try
            {
                var deparmentModel = new Department { DepartmentName = $"Department_{Guid.NewGuid()}" };
                var department = await _departmentRepository.AddAsync(deparmentModel);

                var userModel = new User
                {
                    UserName = $"User_{Guid.NewGuid()}",
                    EmailAddress = $"{Guid.NewGuid()}@gmail.com",
                    Department = department,
                    DepartmentId = department.Id
                };
                var user = await _userRepository.AddAsync(userModel);

                var salaryModel = new Salary
                {
                    CoeffficientSalary = new Random().Next(1, 100),
                    WorkDays = 10,
                    User = user,
                    UserId = user.Id
                };
                var salary = await _salaryRepository.AddAsync(salaryModel);

                var executeResult = await UnitOfWork.SaveChangesAsync();

                return executeResult > 0;
            }
            catch (Exception ex)
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
