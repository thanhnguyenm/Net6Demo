using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorPattern;

internal interface IRequest { }

internal interface IHandler<IRequest>
{
    void Handle(IRequest data);
}

public class IntRequest : IRequest
{
    public int data { get; set; }
}

public class StringRequest : IRequest
{
    public string? data { get; set; }
}

internal class IntHandler : IHandler<IntRequest>
{
    public void Handle(IntRequest data)
    {
        Console.WriteLine(data.data);
    }
}

internal class StringHandler : IHandler<StringRequest>
{
    public void Handle(StringRequest data)
    {
        Console.WriteLine(data.data);
    }
}