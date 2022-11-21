using System;
using System.Linq;
using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;
using OrderDbContext;

namespace Billing
{

    public class OrderPlacedHandler :
        IHandleMessages<OrderPlaced>
    {
        static readonly ILog log = LogManager.GetLogger<OrderPlacedHandler>();
        private readonly OrderContext context;

        public OrderPlacedHandler(OrderContext orderContext)
        {
            this.context = orderContext;
        }

        public Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            var order = this.context.Orders.SingleOrDefault(x => x.OrderKey.ToString() == message.OrderId);
            
            if (order == null)
            {
                throw new Exception("order null");
            }

            order.Status = "Checked out";
            this.context.SaveChanges();

            log.Info($"Billing has received OrderPlaced, OrderId = {message.OrderId}");
            return Task.CompletedTask;
        }
    }
}