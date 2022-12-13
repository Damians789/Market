using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rynek.Publisher.Sprzedawca;

namespace Rynek.Subscriber
{
    public class KupujacySubscriber : IObserver<SprzedajacyData>
    {
        public SprzedajacyData Data { get; set; }
        private IDisposable _unsubscriber;
        private Kupujacy _kupujacy;
        private int _IDSprzedajacego;

        public KupujacySubscriber() { }
        public KupujacySubscriber(IObservable<SprzedajacyData> provider)
        {
            _unsubscriber = provider.Subscribe(this);
        }
        public void Subscribe(IObservable<SprzedajacyData> provider, Kupujacy kp, int ids)
        {
            if (provider == null)
            {
                _unsubscriber = provider.Subscribe(this);
            }
            _kupujacy = kp;
            _IDSprzedajacego = ids;
        }
        public void Unsubscribe()
        {
            _unsubscriber.Dispose();
        }
        public void OnCompleted()
        {
            _unsubscriber.Dispose();
        }
        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }
        public void OnNext(SprzedajacyData value)
        {
            this.Data = value;
            if (_kupujacy != null)
            {
                _kupujacy.ObserwujSprzedawca(_IDSprzedajacego);
            }
        }
    }
}
