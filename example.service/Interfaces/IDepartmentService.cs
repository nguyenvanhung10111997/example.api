using example.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace example.service.Interfaces
{
    public interface IDepartmentService : IDisposable
    {
        Task<bool> CreateAsync();
        Task<Department> GetById(int id);
    }
}
