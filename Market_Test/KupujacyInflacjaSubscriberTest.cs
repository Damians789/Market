using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rynek.Publisher.BankCentralny;
using Rynek.Subscriber;
using NUnit.Framework;

namespace RynekTest
{
    [TestFixture]
    internal class KupujacyInflacjaSubscriberTest
    {
        private RynekInflacjaProvider provider;
        [SetUp]
        public void SetUp()
        {
            provider = new RynekInflacjaProvider();
        }
        [Test]
        public void CheckIfTestWorks()
        {
            Assert.Pass();
        }
        [Test]
        public void CheckIfChange()
        {
            KupujacyInflacjaSubscriber subscriber = new KupujacyInflacjaSubscriber(provider);
            provider.SetMeasurments(1.37);
            Assert.That(subscriber.Data.Inflacja, Is.EqualTo(1.37).Within(0.001));
        }
        [Test]
        public void CheckIfChange2()
        {
            KupujacyInflacjaSubscriber subscriber = new KupujacyInflacjaSubscriber(provider);
            provider.SetMeasurments(1.37);
            provider.SetMeasurments(8.6);
            Assert.That(subscriber.Data.Inflacja, Is.EqualTo(8.6).Within(0.001));
        }
    }
}
