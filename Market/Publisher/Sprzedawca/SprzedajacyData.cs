using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rynek.Data;


namespace Rynek.Publisher.Sprzedawca
{
    public struct SprzedajacyData
    {
        public List<CartItem> Produkt = new List<CartItem>();
        public SprzedajacyData(List<CartItem> produkt)
        {
            this.Produkt = produkt;
        }
    }
}
