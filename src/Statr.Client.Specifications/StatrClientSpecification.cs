using Machine.Fakes;
using Machine.Specifications;
using Statr.Client.Transports;

namespace Statr.Client.Specifications
{
    public class StatrClientSpecification
    {
        [Subject(typeof(StatrClient))]
        public class in_general
        {
            Establish context = () =>
                client = (StatrClient)StatrClient.Build("somehost");

            It should_use_udp_transport = () =>
                client.Transport.ShouldBeOfType<UdpTransport>();

            static StatrClient client;
        }

        [Subject(typeof(StatrClient))]
        public class when_counting : with_client
        {
            Because of = () =>
                client.Count("metric.name");

            It should_send_metric_to_transport = () =>
                transport.WasToldTo(t => t.Send("metric.name:1|c"));
        }

        [Subject(typeof(StatrClient))]
        public class when_counting_with_amount : with_client
        {
            Because of = () =>
                client.Count("metric.name", 10);

            It should_send_metric_to_transport = () =>
                transport.WasToldTo(t => t.Send("metric.name:10|c"));
        }

        [Subject(typeof(StatrClient))]
        public class when_setting_gauge : with_client
        {
            Because of = () =>
                client.Gauge("metric.gauge", 50);

            It should_send_metric_to_transport = () =>
                transport.WasToldTo(t => t.Send("metric.gauge:50|g"));
        }

        public class with_client : WithFakes
        {
            Establish context = () =>
            {
                transport = An<IClientTransport>();
                client = StatrClient.Build(transport);
            };

            protected static IClientTransport transport;

            protected static IStatrClient client;
        }
    }
}
