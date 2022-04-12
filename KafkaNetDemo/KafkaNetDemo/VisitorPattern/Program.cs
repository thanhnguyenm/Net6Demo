// See https://aka.ms/new-console-template for more information
using VisitorPattern;

EmailVisitor emailVisitor = new();
TextVisitor textVisitor = new();

InvoiceNotificationSender invoiceNotificationSender = new();
invoiceNotificationSender.AcceptVisitor(emailVisitor);
invoiceNotificationSender.AcceptVisitor(textVisitor);
invoiceNotificationSender.Send();

MarketingNotificationSender marketingNotificationSender = new();
marketingNotificationSender.AcceptVisitor(emailVisitor);
marketingNotificationSender.AcceptVisitor(textVisitor);
marketingNotificationSender.Send();

Console.ReadLine();