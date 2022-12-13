using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rynek.Publisher.BankCentralny;

namespace Rynek.Subscriber
{
    public class KupujacyInflacjaSubscriber : IObserver<BankCentralnyData>
    {
        public BankCentralnyData Data { get; set; }
        private IDisposable _unsubscriber;
        private Kupujacy _kupujacy;

        public KupujacyInflacjaSubscriber() { }
        public KupujacyInflacjaSubscriber(IObservable<BankCentralnyData> provider)
        {
            _unsubscriber = provider.Subscribe(this);
        }
        public void Subscribe(IObservable<BankCentralnyData> provider, Kupujacy kp)
        {
            if (provider != null)
            {
                _unsubscriber = provider.Subscribe(this);
            }
            _kupujacy = kp;
        }
        public void Unsubscribe()
        {
            _unsubscriber.Dispose();
        }
        public void OnCompleted()
        {
            this.Unsubscribe();
        }
        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }
        public virtual void OnNext(BankCentralnyData value)
        {
            this.Data = value;
            if (_kupujacy != null)
            {
                _kupujacy.Obserwuj();
            }
        }
    }
}
