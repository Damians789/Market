using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rynek.Data;
using Rynek.Subscriber;
using Rynek.Visitors;

namespace Rynek
{
    public class Kupujacy
    {
        private KupujacyInflacjaSubscriber _inflacja = new KupujacyInflacjaSubscriber();
        private List<KupujacySubscriber> _kps = new List<KupujacySubscriber>();
        private Dictionary<int, Sprzedajacy> _sprzedawcy = new Dictionary<int, Sprzedajacy>();
        private Dictionary<Type, SprzedawcaCartItemData> _productsdescription = new Dictionary<Type, SprzedawcaCartItemData> ();

        public double CalculatePrice(CartItem item)
        {
            var visitor = new ShoppingCartVisitorImpl();
            return item.Accept(visitor);
        }

        public Kupujacy() { }
        public Kupujacy(BankCentralny bc, Sprzedajacy sp, Dictionary<Type, SprzedawcaCartItemData> produkty)
        {
            _inflacja.Subscribe(bc.provider, this);
            _sprzedawcy.Add(0, sp);
            _kps.Add(new KupujacySubscriber());
            _kps[0].Subscribe(_sprzedawcy[0].provider, this, 0);
            _productsdescription = produkty;
            Obserwuj();
        }

        public Kupujacy(BankCentralny bc, List<Sprzedajacy> sp, Dictionary<Type, SprzedawcaCartItemData> produkty)
        {
            _inflacja.Subscribe(bc.provider, this);
            for (int i=0; i<sp.Count(); i++)
                _sprzedawcy.Add(i, sp[i]);

            for (int i=0; i<_sprzedawcy.Count(); i++)
            {
                _kps.Add(new KupujacySubscriber());
                _kps[i].Subscribe(_sprzedawcy[i].provider, this, i);
            }
            _productsdescription = produkty;
            Obserwuj();
        }
        public void OkresZakupowy()
        {
            ZaaktualizujZapotrzebowanie();
            Obserwuj();
        }
        public void ZaaktualizujZapotrzebowanie()
        {
            Random rand = new Random();
            foreach (SprzedawcaCartItemData itm in _productsdescription.Values)
                itm.PotrzebnaIlosc += (rand.Next() %9) + 1;
        }

        public void Obserwuj()
        {
            foreach (Sprzedajacy sp in _sprzedawcy.Values)
            {
                foreach (CartItem itm in sp.Produkty)
                {
                    if (_productsdescription.Keys.Contains(itm.GetType()))
                    {
                        sp.Kupuj(itm.GetType(), (_productsdescription[itm.GetType()].PotrzebnaIlosc * 0.07
                            * itm.Accept(new ShoppingCartVisitorImpl()) / _productsdescription[itm.GetType()].CenaReferencyjna));
                    }
                }
            }
        }
        public void ObserwujSprzedawca(int ids)
        {
            Sprzedajacy sp = _sprzedawcy[ids];
            foreach (CartItem itm in sp.Produkty)
            {
                if (_productsdescription.Keys.Contains(itm.GetType()))
                {
                    sp.Kupuj(itm.GetType(), (_productsdescription[itm.GetType()].PotrzebnaIlosc * 0.07
                        * itm.Accept(new ShoppingCartVisitorImpl()) / _productsdescription[itm.GetType()].CenaReferencyjna));
                    sp.Kupuj(itm.GetType(), 2);
                }
            }
        }
        public double Zapotrzebowanie(Type type)
        {
            return _productsdescription[type].PotrzebnaIlosc;
        }
    }
}