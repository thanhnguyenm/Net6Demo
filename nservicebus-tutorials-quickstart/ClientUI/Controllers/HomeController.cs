using System;
using System.Dynamic;
using System.Threading;
using System.Threading.Tasks;
using Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NServiceBus;

namespace ClientUI.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        static int messagesSent;
        private readonly ILogger<HomeController> _log;
        private readonly IMessageSession _messageSession;

        public HomeController(
            IMessageSession messageSession, 
            ILogger<HomeController> logger)
        {
            _messageSession = messageSession;
            _log = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> PlaceOrder()
        {
            string orderId = Guid.NewGuid().ToString();

            var command = new PlaceOrder { OrderId = orderId };

            // Send the command
            //await _messageSession.Send(command).ConfigureAwait(false);
            await _messageSession.Send("thanhnm-74DE141E26BC4449BC9A47CC8CFFB416-Sales", command).ConfigureAwait(false);

            _log.LogInformation($"Sending PlaceOrder, OrderId = {orderId}");

            dynamic model = new ExpandoObject();
            model.OrderId = orderId;
            model.MessagesSent = Interlocked.Increment(ref messagesSent);

            return View(model);
        }
    }
}
