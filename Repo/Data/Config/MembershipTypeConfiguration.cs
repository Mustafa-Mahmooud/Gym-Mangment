using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entites;

namespace Repo.Data.Config
{
    public class MembershipTypeConfiguration : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
          

            builder.Property(mt => mt.DurationInMonths)
                   .IsRequired();

            builder.Property(mt => mt.Price)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            // Relationships
            builder.HasMany(mt => mt.Members)
                   .WithOne(m => m.MembershipType)
                   .HasForeignKey(m => m.MembershipId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
