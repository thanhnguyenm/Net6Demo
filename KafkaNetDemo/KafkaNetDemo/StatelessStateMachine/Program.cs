// See https://aka.ms/new-console-template for more information
var car = new Stateless.StateMachine<Car.States, Car.Actions>(Car.States.Stopped);

car.Configure(Car.States.Stopped)
    .Permit(Car.Actions.Start, Car.States.Started);

car.Configure(Car.States.Started)
    .Permit(Car.Actions.Accelerate, Car.States.Running)
    .PermitReentry(Car.Actions.Start)
    .Permit(Car.Actions.Stop, Car.States.Stopped)
    .OnEntry(s => Console.WriteLine($"Entry: {s.Source}, {s.Destination}"))
    .OnExit(s => Console.WriteLine($"Entry: {s.Source}, {s.Destination}"));

car.Configure(Car.States.Running)
    .Permit(Car.Actions.Stop, Car.States.Stopped);

Console.WriteLine(car.State);

car.Fire(Car.Actions.Start);
Console.WriteLine(car.State);

car.Fire(Car.Actions.Start);
Console.WriteLine(car.State);

car.Fire(Car.Actions.Accelerate);
Console.WriteLine(car.State);

car.Fire(Car.Actions.Stop);
Console.WriteLine(car.State);


class Car
{
    public enum States
    {
        Stopped,
        Started,
        Running
    }

    public enum Actions
    {
        Stop,
        Start,
        Accelerate
    }
}