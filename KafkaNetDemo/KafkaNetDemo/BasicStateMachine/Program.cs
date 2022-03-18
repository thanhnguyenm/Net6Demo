// See https://aka.ms/new-console-template for more information
Car car = new();
Console.WriteLine(car.CurrentState);

car.TakeAction(Car.Action.Start);
Console.WriteLine(car.CurrentState);

car.TakeAction(Car.Action.Accelerate);
Console.WriteLine(car.CurrentState);

car.TakeAction(Car.Action.Stop);
Console.WriteLine(car.CurrentState);

class Car
{
    public enum State
    {
        Stopped,
        Started,
        Running
    }

    public enum Action
    {
        Stop,
        Start, 
        Accelerate
    }

    private State state = State.Stopped;

    public State CurrentState { get { return state; } }

    public void TakeAction(Action action)
    {
        state = (state, action) switch
        {
            (State.Stopped, Action.Start) => State.Started,
            (State.Started, Action.Accelerate) => State.Running,
            (State.Started, Action.Stop) => State.Stopped,
            (State.Running, Action.Stop) => State.Stopped,
            _ => state
        };
    }
}