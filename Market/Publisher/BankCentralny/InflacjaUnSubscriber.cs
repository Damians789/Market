using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rynek.Publisher.BankCentralny
{
    public class InflacjaUnSubscriber : IDisposable
    {
        private List<IObserver<BankCentralnyData>> lstObservers;
        private IObserver<BankCentralnyData> observer;

        public InflacjaUnSubscriber(List<IObserver<BankCentralnyData>> observersCollection, IObserver<BankCentralnyData> observer)
        {
            this.lstObservers = observersCollection;
            this.observer = observer;
        }
        public void Dispose()
        {
            if (this.observer != null && lstObservers.Contains(observer))
            {
                lstObservers.Remove(this.observer);
            }
        }
    }
}
