using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rynek.Publisher.Sprzedawca
{
    public class SprzedajacyUnSubscriber : IDisposable
    {
        private IList<IObserver<SprzedajacyData>> lstObservers;
        private IObserver<SprzedajacyData> observer;

        public SprzedajacyUnSubscriber(IList<IObserver<SprzedajacyData>> lstObservers, IObserver<SprzedajacyData> observer)
        {
            this.lstObservers = lstObservers;
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
