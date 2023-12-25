using BootcampResult.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BootcampResult.Persistence.EntityConfigurations;

public class UserCredentialsConfiguration : IEntityTypeConfiguration<UserCredentials>
{
    public void Configure(EntityTypeBuilder<UserCredentials> builder)
    {
        builder
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<UserCredentials>(user=> user.UserId);
    }
}