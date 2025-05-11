using Core.Entites;
using Repo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Repo
{
    public class GymContextSeed
    {
        public static async Task SeedAsync(GymContext gymContext)
        {

            if (!gymContext.Trainers.Any())
            {
                var TrainersData = File.ReadAllText("../Repo/Data/DataSeed/trainers.json");
                var Trainers = JsonSerializer.Deserialize<List<Trainers>>(TrainersData);

                if (Trainers?.Count > 0)
                {
                    foreach (var Trainer in Trainers)
                    {
                        await gymContext.Set<Trainers>().AddAsync(Trainer);
                    }
                    await gymContext.SaveChangesAsync();
                }
            }
        }
    }
}
