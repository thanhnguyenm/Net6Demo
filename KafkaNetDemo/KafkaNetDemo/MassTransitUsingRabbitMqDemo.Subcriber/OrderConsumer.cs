using MassTransit;
using MassTransit.Models;

namespace MassTransitUsingRabbitMqDemo.Subcriber
{
    public class OrderConsumer : IConsumer<Order>
    {
        public async Task Consume(ConsumeContext<Order> context)
        {
            await Console.Out.WriteLineAsync($"{context.Message.Id} - {context.Message.Name}");
        }
    }
}
