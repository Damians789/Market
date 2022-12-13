using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Rynek;
using Rynek.Subscriber;

namespace RynekTest
{
    [TestFixture]
    internal class BankCentralnyTest
    {
        private BankCentralny bankCentralny;
        [SetUp]
        public void SetUp()
        {
            bankCentralny = new BankCentralny(1.37, 370000);
        }
        [Test]
        public void CheckIfTestWorks()
        {
            Assert.Pass();
        }
        [Test]
        public void CheckIfCanCreate()
        {
            Assert.That(bankCentralny, Is.Not.Null);
        }
        [Test]
        public void CheckIfCanPay()
        {
            bankCentralny.Podatki(1400);
            Assert.That(bankCentralny.SprawdzPodatki(), Is.EqualTo(1400).Within(0.001));
        }
        [Test]
        public void CheckIfInflationCorrect()
        {
            SprzedajacyInflacjaSubscriber provider = new SprzedajacyInflacjaSubscriber(bankCentralny.provider);
            bankCentralny.OkresOpodatkowania();
            Assert.That(provider.Data.Inflacja, Is.EqualTo(3.33).Within(0.001));
        }
        [Test]
        public void CheckIfOkresOpodatkowaniaWorks()
        {
            bankCentralny.OkresOpodatkowania();
            Assert.That(bankCentralny.SprawdzPodatki(), Is.EqualTo(0));
        }
    }
}
