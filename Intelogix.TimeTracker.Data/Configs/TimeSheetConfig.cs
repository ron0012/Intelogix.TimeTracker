using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Intelogix.TimeTracker.Data.Models;

namespace Intelogix.TimeTracker.Data.Configs
{
    internal class TimeSheetConfig : IEntityTypeConfiguration<TimeSheet>
    {
        public void Configure(EntityTypeBuilder<TimeSheet> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x=>x.ClockIn).IsRequired();
            builder.HasOne(x => x.Employee)
                .WithMany(x => x.TimeSheets)
                .HasForeignKey(x => x.UserIdFk)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

        }
    }
}
