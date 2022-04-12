using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorPattern;

internal interface INotificationSender
{
    void Send();
    void AcceptVisitor(IVisitor visitor);
}

internal class InvoiceNotificationSender : INotificationSender
{
    public void AcceptVisitor(IVisitor visitor)
    {
        visitor.Visit(this);
    }

    public void Send()
    {
        Console.WriteLine($"{nameof(InvoiceNotificationSender)}");
    }
}

internal class MarketingNotificationSender : INotificationSender
{
    public void AcceptVisitor(IVisitor visitor)
    {
        visitor.Visit(this);
    }

    public void Send()
    {
        Console.WriteLine($"{nameof(MarketingNotificationSender)}");
    }
}
