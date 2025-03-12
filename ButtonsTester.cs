using Resto.Front.Api.StatusOrderChange.Infrastructure.NalunshService;
using Resto.Front.Api.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace Resto.Front.Api.StatusOrderChange
{
    using static PluginContext;
    /// <summary>
    /// Класс для добавления кнопок
    /// </summary>
    internal sealed class ButtonsTester : IDisposable
    {
        private readonly CompositeDisposable subscriptions;
        private IDisposable authUserWithoutPinLicenseSubscription;

        public ButtonsTester()
        {
            subscriptions = new CompositeDisposable
            {
                Operations.AddButtonToPluginsMenu("Узнать статус заказа", ShowPoupop),               
            };
        }

        public void Dispose()
        {
            subscriptions.Dispose();
            authUserWithoutPinLicenseSubscription?.Dispose();
        }

        private void ShowPoupop((IViewManager vm, IReceiptPrinter printer) callback) 
        {
            var statusOrder = new NalunshService().GetStatus();
            callback.vm.ShowOkPopup("Проверка статуса заказа", $@"Заказ номер {statusOrder.Result.OrderId} в состоянии {statusOrder.Result.Description}");
        }
    }
}
