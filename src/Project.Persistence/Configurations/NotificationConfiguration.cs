using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Entities;

namespace Project.Persistence.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder
            .HasOne(entity => entity.ReceiverUser)
            .WithMany()
            .HasForeignKey(entity => entity.ReceiverUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}