using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rynek.Data;
using Rynek.Publisher.Sprzedawca;
using Rynek.Subscriber;
using Rynek.Visitors;

namespace Rynek
{
    public class Sprzedajacy
    {
        public List<CartItem> Produkty = new List<CartItem>();
        private BankCentralny _bc;
        private SprzedajacyInflacjaSubscriber _inflacja = new SprzedajacyInflacjaSubscriber();
        public SprzedajacyCenyProvider provider = new SprzedajacyCenyProvider();

        public Sprzedajacy()
        {
            provider = new SprzedajacyCenyProvider();
        }
        public Sprzedajacy(BankCentralny bc)
        {
            _bc = bc;
            _inflacja.Subscribe(bc.provider, this);
            Produkty.Add(new Book("978-83-8222-309-5", 0.15, 22.54, 1));
            Produkty.Add(new Fruit("Tangerine", 0.25, 4.0, 3));
            Produkty.Add(new Toy("Miś", 22.30, 40.21, 52));
            Zaopatruj();
            provider.SetMeasurments(Produkty);
        }
        public void OkresSprzedazowy()
        {
            Zaopatruj();
            ZaaktualizujCeny();
        }
        public void Zaopatruj()
        {
            Random rand = new Random();
            foreach (CartItem itm in Produkty)
            {
                itm.DostepnaIlosc += (rand.Next() % 9) + 1;
            }
        }
        private void ZaaktualizujCeny()
        {
            foreach (CartItem itm in Produkty)
            {
                /*if (_inflacja.Data.Inflacja is not double.NaN and not 0 and not double.NegativeInfinity && _inflacja.Data.Inflacja != null)
                {

                    itm.KosztWytworzenia = itm.KosztWytworzenia * _inflacja.Data.Inflacja;
                }*/
                itm.KosztWytworzenia = itm.KosztWytworzenia * _inflacja.Data.Inflacja;
                if (itm.SprzedanaIlosc > 3)
                    itm.Marza *= (1 + ((itm.SprzedanaIlosc - 3) * 0.1));
                else
                    itm.Marza *= (1 - ((3 - itm.SprzedanaIlosc) * 0.1));
            }
            provider.SetMeasurments(Produkty);
        }
        public void ZaaktualizujCenyInflacja()
        {
            foreach (CartItem itm in Produkty)
            {
                itm.KosztWytworzenia *= _inflacja.Data.Inflacja;
            }
            provider.SetMeasurments(Produkty);
        }
        public void Kupuj(Type type, double ilosc)
        {
            foreach (CartItem itm in Produkty)
            {
                if (itm.GetType() == type)
                {
                    itm.DostepnaIlosc -= ilosc;
                    itm.SprzedanaIlosc += ilosc;
                    _bc.Podatki(0.23 * ilosc * itm.Accept(new ShoppingCartVisitorImpl()));
                }
            }
        }
    }
}
