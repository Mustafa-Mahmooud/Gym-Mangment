using Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Data.Config
{
    public class MemberConfig : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {

            builder.Property(T => T.FullName).IsRequired();
            builder.Property(T => T.Email).IsRequired();

            builder
                .HasOne(m => m.Trainer)
                .WithMany(t => t.Members)
                .HasForeignKey(m => m.TrainerId);

            builder
                .HasOne(m => m.MembershipType)
                .WithMany(mt => mt.Members)
                .HasForeignKey(m => m.MembershipId);

        }
    }
}
