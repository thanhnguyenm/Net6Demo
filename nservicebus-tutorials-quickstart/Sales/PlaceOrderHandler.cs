using System;
using System.Linq;
using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;
using OrderDbContext;

namespace Sales
{    
    public class PlaceOrderHandler :
        IHandleMessages<PlaceOrder>
    {
        static readonly ILog log = LogManager.GetLogger<PlaceOrderHandler>();
        static readonly Random random = new Random();
        private readonly OrderContext context;
        private readonly IMessageSession messageSession;

        public PlaceOrderHandler(OrderContext context)
        {
            this.context = context;
        }

        public Task Handle(PlaceOrder message, IMessageHandlerContext context)
        {
            log.Info($"Received PlaceOrder, OrderId = {message.OrderId}");

            // This is normally where some business logic would occur

            #region ThrowTransientException
            // Uncomment to test throwing transient exceptions
            //if (random.Next(0, 5) == 0)
            //{
            //    throw new Exception("Oops");
            //}
            #endregion

            #region ThrowFatalException
            // Uncomment to test throwing fatal exceptions
            //throw new Exception("BOOM");
            #endregion

            var orderPlaced = new OrderPlaced
            {
                OrderId = message.OrderId
            };

            var order = this.context.Orders.SingleOrDefault(x => x.OrderKey.ToString() == message.OrderId);
            if (order == null)
            {
                order = new Entities.Order
                {
                    OrderKey = Guid.Parse(message.OrderId),
                    OrderName = message.OrderId
                };

                this.context.Orders.Add(order);
            }
            order.Status = "NEW";
            this.context.SaveChanges();

            //var task = Task.Delay(20000, default);
            //task.Wait();

            log.Info($"Publishing OrderPlaced, OrderId = {message.OrderId}");

            return context.Publish(orderPlaced);
            //return context.Send("thanhnm-74DE141E26BC4449BC9A47CC8CFFB416-Billing", orderPlaced);
        }
    }
}
