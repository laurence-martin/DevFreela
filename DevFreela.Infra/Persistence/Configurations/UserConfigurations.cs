using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infra.Persistense.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .ToTable("Users")
                .HasKey(t => t.Id);
            builder
                .HasMany(x => x.Skills)
                .WithOne()
                .HasForeignKey(p => p.IdSkill)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
