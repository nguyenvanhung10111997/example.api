using example.domain.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace example.domain.Entities
{
    [Table("Department", Schema = "USR")]
    public class Department : AuditEntity<int>
    {
        public Department()
        {
            Users = new HashSet<User>();
        }

        public required string DepartmentName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }

    public static class DepartmentModelBuilder
    {
        public static void CreateDepartmentBuilder(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(p => p.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(p => p.DepartmentName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });
        }
    }
}
