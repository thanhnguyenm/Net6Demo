// See https://aka.ms/new-console-template for more information
using MediatorPattern;

IMediator mediator = new Mediator();

IntRequest intRequest = new();
StringRequest stringRequest = new();

IHandler<IntRequest> intHandler = new IntHandler();
IHandler<StringRequest> stringHandler = new StringHandler();

mediator.AddHandler(intHandler);
mediator.AddHandler(stringHandler);

Console.ReadLine();