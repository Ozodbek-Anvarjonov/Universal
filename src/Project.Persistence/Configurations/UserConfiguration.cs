using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Entities;
using Project.Domain.Enums;

namespace Project.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasData(new User
            {
                Id = 1,
                FirstName = "SYSTEM",
                LastName = "SYSTEM",
                Role = UserRole.Owner,
            });
    }
}