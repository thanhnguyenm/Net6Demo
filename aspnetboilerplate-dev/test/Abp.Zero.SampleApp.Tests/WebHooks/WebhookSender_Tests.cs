using System;
using System.Threading.Tasks;
using Abp.Webhooks;
using Shouldly;
using Xunit;

namespace Abp.Zero.SampleApp.Tests.Webhooks
{
    public class WebhookSender_Tests : WebhookTestBase
    {
        [Fact]
        public async Task Should_Throw_Exception_Async()
        {
            var webhookSender = Resolve<IWebhookSender>();

            await webhookSender
                .SendWebhookAsync(new WebhookSenderArgs())
                .ShouldThrowAsync<ArgumentNullException>();

            var exception = await webhookSender
                .SendWebhookAsync(new WebhookSenderArgs()
                {
                    WebhookEventId = Guid.NewGuid(),
                    WebhookSubscriptionId = Guid.Empty
                })
                .ShouldThrowAsync<ArgumentNullException>();

            exception.Message.ShouldContain(nameof(WebhookSenderArgs.WebhookSubscriptionId));

            var exception2 = await webhookSender
                .SendWebhookAsync(new WebhookSenderArgs()
                {
                    WebhookEventId = Guid.Empty,
                    WebhookSubscriptionId = Guid.NewGuid()
                })
                .ShouldThrowAsync<ArgumentNullException>();

            exception2.Message.ShouldContain(nameof(WebhookSenderArgs.WebhookEventId));
        }

        [Fact]
        public void Should_Throw_Exception()
        {
            var webhookSender = Resolve<IWebhookSender>();

            Should.Throw<ArgumentNullException>(() =>
            {
                webhookSender.SendWebhook(new WebhookSenderArgs());
            });

            var exception = Should.Throw<ArgumentNullException>(() =>
            {
                webhookSender.SendWebhook(new WebhookSenderArgs()
                {
                    WebhookEventId = Guid.NewGuid(),
                    WebhookSubscriptionId = Guid.Empty
                });
            });

            exception.Message.ShouldContain(nameof(WebhookSenderArgs.WebhookSubscriptionId));

            var exception2 = Should.Throw<ArgumentNullException>(() =>
            {
                webhookSender.SendWebhook(new WebhookSenderArgs()
                {
                    WebhookEventId = Guid.Empty,
                    WebhookSubscriptionId = Guid.NewGuid()
                });
            });

            exception2.Message.ShouldContain(nameof(WebhookSenderArgs.WebhookEventId));
        }
    }
}
