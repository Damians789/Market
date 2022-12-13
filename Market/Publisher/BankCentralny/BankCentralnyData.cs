using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rynek.Publisher.BankCentralny
{
    public struct BankCentralnyData
    {
        public double Inflacja { get; set; }
        public BankCentralnyData(double infl)
        {
           this.Inflacja = infl;
        }
    }
}
