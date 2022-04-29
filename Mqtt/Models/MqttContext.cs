using System;
using Microsoft.EntityFrameworkCore;
namespace Mqtt.Models {
    public class MqttContext: DbContext {
        public DbSet<DataModel> Datas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer("Server=localhost;Database=mqtt;User=sa;Password=Mqtt@123");

            // optionsBuilder.UseSqlServer("Server=.\;Database=Mqtt;Trusted_Connection=True");
        }
    }
}

