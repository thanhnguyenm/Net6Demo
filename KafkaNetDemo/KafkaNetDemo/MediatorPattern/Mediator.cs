using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorPattern;

internal interface IMediator
{
    void AddHandler<T>(IHandler<T> handler) where T : IRequest;

    void Notify(IRequest request);
}

internal class Mediator : IMediator
{
    private Dictionary<string, IHandler<IRequest>> _handlers = new Dictionary<string, IHandler<IRequest>>();

    public void AddHandler<T>(IHandler<T> handler) where T : IRequest
    {
        _handlers.Add(handler.GetType().FullName, (IHandler<IRequest>)handler);
    }

    public void Notify(IRequest request)
    {
        if (_handlers.TryGetValue(request.GetType().Name, out var handler))
        {
            handler.Handle(request);
        }
    }
}