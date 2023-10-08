using example.domain.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace example.domain.Entities
{
    [Table("User", Schema = "USR")]
    public class User : AuditEntity<int>
    {
        public required string UserName { get; set; }

        public string? EmailAddress { get; set; }

        public int DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public virtual Department Department { get; set; }
    }

    public static class UserModelBuilder
    {
        public static void CreateUserBuilder(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(p => p.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(p => p.UserName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });
        }
    }
}
