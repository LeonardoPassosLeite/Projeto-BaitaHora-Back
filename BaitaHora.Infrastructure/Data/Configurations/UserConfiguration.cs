using BaitaHora.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaitaHora.Infrastructure.Persistence.Configurations
{
       public class UserConfiguration : IEntityTypeConfiguration<User>
       {
              public void Configure(EntityTypeBuilder<User> builder)
              {
                     builder.HasKey(u => u.Id);

                     builder.Property(u => u.Username)
                            .IsRequired()
                            .HasMaxLength(50);

                     builder.Property(u => u.PasswordHash)
                            .IsRequired();

                     builder.Property(u => u.Role)
                            .IsRequired()
                            .HasConversion<string>()
                            .HasMaxLength(20);

                     builder.Property(u => u.ProfileImageUrl)
                            .HasMaxLength(500);
              }
       }
}