using Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Data.Config
{
    public class TrainerConfig : IEntityTypeConfiguration<Trainers>
    {
        public void Configure(EntityTypeBuilder<Trainers> builder)
        {
            builder.Property(T => T.FullName).IsRequired();
            builder.Property(T => T.Email).IsRequired();
            builder.Property(T => T.Salary).IsRequired();
            builder.Property(T => T.Specialty).IsRequired();

            builder.HasMany(t => t.Members)
                    .WithOne(m => m.Trainer)
                    .HasForeignKey(m => m.TrainerId)
                    .OnDelete(DeleteBehavior.Cascade);



        }
    }
}
