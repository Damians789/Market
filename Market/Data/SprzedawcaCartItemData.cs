using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rynek.Data
{
    public class SprzedawcaCartItemData
    {
        public double CenaReferencyjna { get; set; }
        public double PotrzebnaIlosc { get; set; }
        public SprzedawcaCartItemData(double cref, double pot)
        {
            CenaReferencyjna = cref;
            PotrzebnaIlosc = pot;
        }
    }
}
