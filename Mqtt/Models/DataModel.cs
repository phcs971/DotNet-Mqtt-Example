using System;
using System.ComponentModel.DataAnnotations;

namespace Mqtt.Models {
    public class DataModel {
        public DataModel() { }

        [Key]
        public int Id { get; set; }

        public long Voltage { get; set; }
        public long Current { get; set; }

        public long Power { get { return Voltage * Current; }
        }
    }
}

