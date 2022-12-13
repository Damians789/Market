using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rynek.Subscriber;
using Rynek.Publisher.BankCentralny;
using NUnit.Framework;

namespace RynekTest
{
    [TestFixture]
    internal class SprzedajacyInflacjaSubscriberTest
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
            SprzedajacyInflacjaSubscriber subscriber = new SprzedajacyInflacjaSubscriber(provider);
            provider.SetMeasurments(1.37);
            Assert.That(subscriber.Data.Inflacja, Is.EqualTo(1.37).Within(0.001));
        }
        [Test]
        public void CheckIfChange2()
        {
            SprzedajacyInflacjaSubscriber subscriber = new SprzedajacyInflacjaSubscriber(provider);
            
            provider.SetMeasurments(1.37);
            /*provider.SetMeasurments(8.6);*/
            provider.SetMeasurments(new BankCentralnyData(8.6));
            Assert.That(subscriber.Data.Inflacja, Is.EqualTo(8.6).Within(0.001));
        }
    }
}
