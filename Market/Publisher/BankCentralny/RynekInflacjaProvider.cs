using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rynek.Publisher.BankCentralny
{
    public class RynekInflacjaProvider : IObservable<BankCentralnyData>
    {
        private List<IObserver<BankCentralnyData>> observers;

        public RynekInflacjaProvider()
        {
            observers = new List<IObserver<BankCentralnyData>>();
        }
        public IDisposable Subscribe(IObserver<BankCentralnyData> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
            return new InflacjaUnSubscriber(observers, observer);
        }
        private void MeasurmentsChanged(double Inflacja)
        {
            foreach (var obs in observers)
            {
                obs.OnNext(new BankCentralnyData(Inflacja));
            }
        }
        public void MeasurmentsChanged(BankCentralnyData? loc)
        {
            foreach (var obs in observers)
            {
                if (!loc.HasValue)
                    obs.OnError(new BankCentralnyDataUnknownException());
                else
                    obs.OnNext(loc.Value);
            }
        }
        public void SetMeasurments(BankCentralnyData? loc)
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
        public void SetMeasurments(double Inflacja)
        {
            MeasurmentsChanged(Inflacja);
        }
    }
    public class BankCentralnyDataUnknownException : Exception
    {
        internal BankCentralnyDataUnknownException()
        { }
    }
}
