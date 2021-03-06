using System;
using System.Threading.Tasks;
using Abp.Auditing;
using Abp.Dependency;
using Abp.RealTime;
using Abp.Runtime.Session;
using Castle.Core.Logging;

namespace Abp.Web.SignalR.Hubs
{
    /// <summary>
    /// Common Hub of ABP.
    /// </summary>
    public class AbpCommonHub : AbpHubBase, ITransientDependency
    {
        private readonly IOnlineClientManager _onlineClientManager;
        private readonly IClientInfoProvider _clientInfoProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbpCommonHub"/> class.
        /// </summary>
        public AbpCommonHub(
            IOnlineClientManager onlineClientManager, 
            IClientInfoProvider clientInfoProvider)
        {
            _onlineClientManager = onlineClientManager;
            _clientInfoProvider = clientInfoProvider;

            Logger = NullLogger.Instance;
            AbpSession = NullAbpSession.Instance;
        }

        public void Register()
        {
            Logger.Debug("A client is registered: " + Context.ConnectionId);
        }

        public override async Task OnConnected()
        {
            await base.OnConnected();

            var client = CreateClientForCurrentConnection();

            Logger.Debug("A client is connected: " + client);
            
            _onlineClientManager.Add(client);
        }

        public override async Task OnReconnected()
        {
            await base.OnReconnected();

            var client = _onlineClientManager.GetByConnectionIdOrNull(Context.ConnectionId);
            if (client == null)
            {
                client = CreateClientForCurrentConnection();
                _onlineClientManager.Add(client);
                Logger.Debug("A client is connected (on reconnected event): " + client);
            }
            else
            {
                Logger.Debug("A client is reconnected: " + client);
            }
        }

        public override async Task OnDisconnected(bool stopCalled)
        {
            await base.OnDisconnected(stopCalled);

            Logger.Debug("A client is disconnected: " + Context.ConnectionId);

            try
            {
                _onlineClientManager.Remove(Context.ConnectionId);
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.ToString(), ex);
            }
        }

        private IOnlineClient CreateClientForCurrentConnection()
        {
            return new OnlineClient(
                Context.ConnectionId,
                GetIpAddressOfClient(),
                AbpSession.TenantId,
                AbpSession.UserId
            );
        }

        protected virtual string GetIpAddressOfClient()
        {
            try
            {
                return _clientInfoProvider.ClientIpAddress;
            }
            catch (Exception ex)
            {
                Logger.Error("Can not find IP address of the client! connectionId: " + Context.ConnectionId);
                Logger.Error(ex.Message, ex);
                return "";
            }
        }
    }
}
