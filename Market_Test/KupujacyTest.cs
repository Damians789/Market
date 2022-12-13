using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rynek;
using Rynek.Data;
using NUnit.Framework;

namespace RynekTest
{
    [TestFixture]
    internal class KupujacyTest
    {
        private BankCentralny bank;
        private Sprzedajacy sprzedawca;
        private Kupujacy kupujacy;
        [SetUp]
        public void SetUp()
        {
            bank = new BankCentralny(1.37, 160000);
            sprzedawca = new Sprzedajacy(bank);
            Dictionary<Type, SprzedawcaCartItemData> ceny = new Dictionary<Type, SprzedawcaCartItemData>();
            ceny.Add(sprzedawca.Produkty[0].GetType(), new SprzedawcaCartItemData(5, 7));
            ceny.Add(sprzedawca.Produkty[1].GetType(), new SprzedawcaCartItemData(8, 13));
            ceny.Add(sprzedawca.Produkty[2].GetType(), new SprzedawcaCartItemData(72, 50));
            kupujacy = new Kupujacy(bank, sprzedawca, ceny);
        }
        [Test]
        public void CheckIfTestWorks()
        {
            Assert.Pass();
        }
        [Test]
        public void CheckIfCanCreate()
        {
            Assert.That(kupujacy, Is.Not.Null);
        }

        [Test]
        public void CheckIfNeedsAccurate()
        {
            double potrzeba = kupujacy.Zapotrzebowanie(new Book().GetType());
            kupujacy.ZaaktualizujZapotrzebowanie();
            Assert.That(potrzeba, Is.LessThan(kupujacy.Zapotrzebowanie(new Book().GetType())));
        }
        [Test]
        public void CheckIfAnalyseWorks()
        {
            bank.OkresOpodatkowania();
            double tax = bank.SprawdzPodatki();
            kupujacy.Obserwuj();
            Assert.That(bank.SprawdzPodatki(), Is.EqualTo(3.147).Within(0.001));
        }
        [Test]
        public void CheckIfAnalyseWorks2()
        {
            bank.OkresOpodatkowania();
            double tax = bank.SprawdzPodatki();
            sprzedawca.ZaaktualizujCenyInflacja();
            Assert.That(bank.SprawdzPodatki(), Is.EqualTo(tax).Within(0.001));
        }
        [Test]
        public void CheckIfAnalyseWorks3()
        {
            bank.OkresOpodatkowania();
            double tax = bank.SprawdzPodatki();
            kupujacy.ObserwujSprzedawca(0);
            Assert.That(bank.SprawdzPodatki(), Is.EqualTo(9.858).Within(0.7));
        }
    }
}
