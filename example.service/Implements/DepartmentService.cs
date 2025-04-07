using example.domain.Entities;
using example.domain.Interfaces;
using example.service.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Department> GetById(int id)
        {
            //Lazy Loading - 2 database roundtrips
            var department1 = (await _departmentRepository.GetAsync(x => x.Id == id)).FirstOrDefault();
            var users1 = department1.Users;

            ////Eager Loading - 1 database roundtrip
            var department2 = (await _departmentRepository.GetAsync(x => x.Id == id)).Include(x => x.Users).FirstOrDefault();
            var users2 = department2.Users;

            //Explicit Loading
            var department3 = (await _departmentRepository.GetAsync(x => x.Id == id)).FirstOrDefault();
            UnitOfWork.GetContext().Entry(department3).Collection(x => x.Users).Load();
            //var users3 = department3.Users;

            return department3;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
