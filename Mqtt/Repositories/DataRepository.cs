using System;
using Mqtt.Models;
namespace Mqtt.Repositories {
    public class DataRepository {
        public DataRepository() { }

        public List<DataModel> GetDataModels() {
            using var context = new MqttContext();
            return context.Datas.ToList();
        }

        public DataModel PostDataModel(DataModel data) {
            using var context = new MqttContext();
            context.Add(data);
            context.SaveChanges();
            return data;
        }

        public DataModel? PutDataModel(int id, DataModel data) {
            using var context = new MqttContext();
            var d = context.Datas.Where(d => d.Id == id).FirstOrDefault();
            if (d == null) { return null; }
            d.Voltage = data.Voltage;
            d.Current = data.Current;
            context.SaveChanges();
            return d;
        }

        public bool DeleteDataModel(int id) {
            using var context = new MqttContext();
            var d = context.Datas.Where(d => d.Id == id).FirstOrDefault();
            if (d == null) { return false; }
            context.Remove(d);
            context.SaveChanges();
            return true;
        }
    }
}

