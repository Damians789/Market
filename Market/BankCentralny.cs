using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rynek.Publisher.BankCentralny;

namespace Rynek
{
    public class BankCentralny
    {
        private double Inflacja;
        private double PoprzedniPrzychod;
        private double ObecnyPrzychod;
        public RynekInflacjaProvider provider { get; set; }
        private void CalculateInflation()
        {
            if (PoprzedniPrzychod > 150000)
                Inflacja = 3 + (0.33 * (1 - (ObecnyPrzychod / PoprzedniPrzychod)));
            else
                Inflacja = 2.1;
            provider.SetMeasurments(Inflacja);
        }
        public BankCentralny()
        {
            provider = new RynekInflacjaProvider();
        }
        public BankCentralny(double inflacja, double tax)
        {
            Inflacja = inflacja;
            PoprzedniPrzychod = tax;
            ObecnyPrzychod = 0;
            provider = new RynekInflacjaProvider();
            provider.SetMeasurments(inflacja);
        }
        public void OkresOpodatkowania()
        {
            CalculateInflation();
            PoprzedniPrzychod = ObecnyPrzychod;
            ObecnyPrzychod = 0;
        }
        public void Podatki(double tax)
        {
            ObecnyPrzychod += tax;
        }
        public double SprawdzPodatki()
        {
            return ObecnyPrzychod;
        }
    }
}
