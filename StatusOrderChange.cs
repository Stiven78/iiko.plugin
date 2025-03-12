using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using Resto.Front.Api.Attributes;
using Resto.Front.Api.Attributes.JetBrains;

namespace Resto.Front.Api.StatusOrderChange
{
    /// <summary>
    /// Входной класс в плагин приложения
    /// </summary>
    [UsedImplicitly]
    [PluginLicenseModuleId(21005108)]
    public sealed class StatusOrderChange : IFrontPlugin
    {
        private readonly Stack<IDisposable> subscriptions = new Stack<IDisposable>();

        public StatusOrderChange()
        {
            PluginContext.Log.Info("Initializing StatusOrderChange");

            subscriptions.Push(new ButtonsTester());

            PluginContext.Log.Info("StatusOrderChange started");
        }

        public void Dispose()
        {
            while (subscriptions.Any())
            {
                var subscription = subscriptions.Pop();
                try
                {
                    subscription.Dispose();
                }
                catch (RemotingException)
                {
                    // nothing to do with the lost connection
                }
            }

            PluginContext.Log.Info("StatusOrderChange stopped");
        }
    }
}