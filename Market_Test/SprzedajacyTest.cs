using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Rynek;
using Rynek.Data;

namespace RynekTest
{
    [TestFixture]
    internal class SprzedajacyTest
    {
        private Sprzedajacy sprzedawca;
        private BankCentralny bank;
        private List<CartItem> Produkty = new List<CartItem>();
        [SetUp]
        public void SetUp()
        {
            bank = new BankCentralny(1.37, 140000);
            sprzedawca = new Sprzedajacy(bank);
        }
        [Test]
        public void CheckIfTestWorks()
        {
            Assert.Pass();
        }
        [Test]
        public void CheckIfCanCreate()
        {
            Assert.That(sprzedawca, Is.Not.Null);
        }
        [Test]
        public void CheckIfIsbnCorrect_Exception()
        {
            Assert.Throws<Exception>(() => Produkty.Add(new Book("978-83xd5", 0.15, 22.54, 1)));
        }
        [Test]
        public void CheckIfAmountChanges()
        {
            double prev = sprzedawca.Produkty[0].DostepnaIlosc;
            sprzedawca.Zaopatruj();
            double ilosc = sprzedawca.Produkty[0].DostepnaIlosc;
            Assert.That(prev, Is.LessThan(ilosc));
        }
        [Test]
        public void CheckIfMarzaBelowZero_Exception()
        {
            Assert.Throws<Exception>(() => Produkty.Add(new Book("978-83xd5", -0.15, 22.54, 1)));
        }
        [Test]
        public void CheckIfPricesUpdate()
        {
            double prev = sprzedawca.Produkty[0].KosztWytworzenia;
            bank.OkresOpodatkowania();
            sprzedawca.OkresSprzedazowy();
            double koszt = sprzedawca.Produkty[0].KosztWytworzenia;
            Assert.That(koszt, Is.EqualTo(99.40).Within(0.005));
        }
        [Test]
        public void CheckIfWytworzenieBelowZero_Exception()
        {
            Assert.Throws<Exception>(() => Produkty.Add(new Book("978-83xd5", 0.15, -22.54, 1)));
        }
        [Test]
        public void CheckIfCanBuy()
        {
            double prev = sprzedawca.Produkty[0].DostepnaIlosc;
            sprzedawca.Kupuj(sprzedawca.Produkty[0].GetType(), 3);
            double now = sprzedawca.Produkty[0].DostepnaIlosc;
            Assert.That(now, Is.EqualTo(prev - 3).Within(0.001));
        }
    }
}
