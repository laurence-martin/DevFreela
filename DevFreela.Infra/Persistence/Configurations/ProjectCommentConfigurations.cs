using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace DevFreela.Infra.Persistense.Configurations
{
    public class ProjectCommentConfigurations : IEntityTypeConfiguration<ProjectComment>
    {
        public void Configure(EntityTypeBuilder<ProjectComment> builder)
        {
            builder
                .ToTable("ProjectComments")
                .HasKey(t => t.Id);
            builder
                .HasOne(t => t.User)
                .WithMany(t => t.Comments)
                .HasForeignKey(t => t.IdUser)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .HasOne(t => t.Project)
                .WithMany(t => t.Comments)
                .HasForeignKey(t => t.IdProject)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
