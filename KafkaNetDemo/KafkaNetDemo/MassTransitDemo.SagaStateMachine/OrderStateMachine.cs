using Automatonymous;

namespace MassTransitDemo.SagaStateMachine
{
    public interface SubmitOrder
    {
        Guid OrderId { get; }
        DateTime OrderDate { get; }
    }

    public interface OrderAccepted
    {
        Guid OrderId { get; }
    }

    public class OrderStateMachine : MassTransitStateMachine<OrderState>
    {
        public OrderStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => SubmitOrder, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => OrderAccepted, x => x.CorrelateById(context => context.Message.OrderId));

            Initially(
                When(SubmitOrder)
                    .Then(x => x.Instance.OrderDate = x.Data.OrderDate)
                    .TransitionTo(Submitted),
                When(OrderAccepted)
                    .TransitionTo(Accepted));

            During(Submitted,
                When(OrderAccepted)
                    .TransitionTo(Accepted));

            During(Accepted,
                When(SubmitOrder)
                .Then(x => x.Instance.OrderDate = x.Data.OrderDate));
        }

        //define States
        public State Submitted { get; private set; }
        public State Accepted { get; private set; }

        //define Events
        public Event<SubmitOrder> SubmitOrder { get; private set; }
        public Event<OrderAccepted> OrderAccepted { get; private set; }
    }
}