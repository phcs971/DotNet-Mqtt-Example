using System.Text;
using Mqtt.Repositories;
using Mqtt.Models;
using MQTTnet;
using MQTTnet.Server;
using MQTTnet.Protocol;
using Newtonsoft.Json.Linq;

namespace Mqtt.Services {
    public class MqttService {
        private MqttService() { }

        public static MqttService Instance = new();

        private DataRepository Repository = new();

        private IMqttServer? server { get; set; }

        public void Build() {
            if (server != null) { return; }
            var factory = new MqttFactory();
            server = factory.CreateMqttServer();
        }

        public async Task Start() {
            if (server == null) { return; }
            var options = new MqttServerOptionsBuilder()
                .WithDefaultEndpoint()
                .WithApplicationMessageInterceptor(OnNewMessage)
                .WithConnectionValidator(OnNewConnection)
                .Build();
            await server.StartAsync(options);
        }

        public async Task Stop() {
            if (server == null) { return; }
            await server.StopAsync();
            server.Dispose();
            server = null;
        }

        private void OnNewConnection(MqttConnectionValidatorContext context) {
            context.ReasonCode = MqttConnectReasonCode.Success;

        }

        private void OnNewMessage(MqttApplicationMessageInterceptorContext context) {
            var payload = Encoding.UTF8.GetString(
                context.ApplicationMessage.Payload,
                0,
                context.ApplicationMessage.Payload.Length
            );
            var json = JObject.Parse(payload);

            Console.WriteLine("voltage {0}, current {1}", json["voltage"], json["current"]);
            var data = new DataModel();
            data.Voltage = Convert.ToInt64(json["voltage"]);
            data.Current = Convert.ToInt64(json["current"]);
            Repository.PostDataModel(data);
        }
    }
}

