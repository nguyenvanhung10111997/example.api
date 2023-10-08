using example.domain.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace example.domain.Entities
{
    [Table("Salary", Schema = "USR")]
    public class Salary : AuditEntity<int>
    {
        public int UserId { get; set; }

        public float CoeffficientSalary { get; set; }

        public float WorkDays { get; set; }

        public float TotalSalary { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }

    public static class SalaryModelBuilder
    {
        public static void CreateSalaryBuilder(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Salary>(entity =>
            {
                entity.Property(p => p.Id)
                    .ValueGeneratedOnAdd();
            });
        }
    }
}
