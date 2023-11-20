using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Intelogix.TimeTracker.Common.Security;
using Intelogix.TimeTracker.Data.Models;
namespace Intelogix.TimeTracker.Data.Configs
{
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        private readonly IConfiguration _configuration;
        public UserConfig(IConfiguration configuration) {
        _configuration = configuration;
        }
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.Property(x => x.UserName).IsRequired();
            builder.Property(x => x.UserName).HasMaxLength(20);
            builder.HasIndex(x => x.UserName)
                .IsUnique();
            builder.Property(x => x.Password).IsRequired();
            builder.HasData(new User { 
            Id = 1,
            Name = "Admin",
            UserName ="Admin",
            Password = AesCrypto.Encrypt("P4ssw0rd", _configuration["Security:Key"]),
            });

        }
    }
}
