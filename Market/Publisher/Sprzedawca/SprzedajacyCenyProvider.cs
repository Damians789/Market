using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rynek.Data;

namespace Rynek.Publisher.Sprzedawca
{
    public class SprzedajacyCenyProvider : IObservable<SprzedajacyData>
    {
        List<IObserver<SprzedajacyData>> observers;

        public SprzedajacyCenyProvider()
        {
            observers = new List<IObserver<SprzedajacyData>>();
        }
        public IDisposable Subscribe(IObserver<SprzedajacyData> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
            return new SprzedajacyUnSubscriber(observers, observer);
        }
        private void MeasurmentsChanged(List<CartItem> produkt)
        {
            foreach (var obs in observers)
            {
                obs.OnNext(new SprzedajacyData(produkt));
            }
        }
        public void SetMeasurments(List<CartItem> produkt)
        {
            MeasurmentsChanged(produkt);
        }
        private void MeasurmentsChanged(SprzedajacyData? loc)
        {
            foreach (var obs in observers)
            {
                if (!loc.HasValue)
                    obs.OnError(new SprzedajacyDataUnknownException());
                else
                    obs.OnNext(loc.Value);
            }
        }
        public void SetMeasurments(SprzedajacyData? loc)
        {
            MeasurmentsChanged(loc);
        }

        public void EndTransmission()
        {
            foreach (var obs in observers.ToArray())
                if (observers.Contains(obs))
                    obs.OnCompleted();

            observers.Clear();
        }
    }
    public class SprzedajacyDataUnknownException : Exception
    {
        internal SprzedajacyDataUnknownException()
        { }
    }
}
