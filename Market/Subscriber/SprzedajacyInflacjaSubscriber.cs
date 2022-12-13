using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rynek.Publisher.BankCentralny;

namespace Rynek.Subscriber
{
    public class SprzedajacyInflacjaSubscriber : IObserver<BankCentralnyData>
    {
        public BankCentralnyData Data { get; set; }
        private IDisposable _unsubscriber;
        private Sprzedajacy _sprzedawca;

        public SprzedajacyInflacjaSubscriber() { }
        public SprzedajacyInflacjaSubscriber(IObservable<BankCentralnyData> provider)
        {
            _unsubscriber = provider.Subscribe(this);
        }
        public void Subscribe(IObservable<BankCentralnyData> provider, Sprzedajacy sp)
        {
            if (provider != null)
            {
                _unsubscriber = provider.Subscribe(this);
            }
            _sprzedawca = sp;
        }
        public void Unsubscribe()
        {
            _unsubscriber.Dispose();
        }
        public void OnCompleted()
        {

        }
        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }
        public virtual void OnNext(BankCentralnyData value)
        {
            this.Data = value;
            if (_sprzedawca != null)
            {
                _sprzedawca.ZaaktualizujCenyInflacja();
            }
        }
    }
}
