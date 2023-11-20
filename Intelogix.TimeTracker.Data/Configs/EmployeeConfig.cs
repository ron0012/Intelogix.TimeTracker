using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Intelogix.TimeTracker.Data.Models;

namespace Intelogix.TimeTracker.Data.Configs
{
    internal class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x=>x.Id).IsRequired();
            builder.Property(x=>x.Name).IsRequired();
            builder.Property(x=>x.Name).HasMaxLength(100);
            builder.HasMany(x => x.TimeSheets)
                .WithOne(x => x.Employee)
                .HasForeignKey(x=>x.UserIdFk)               
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
