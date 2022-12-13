using NUnit.Framework;
using Rynek.Publisher.Sprzedawca;
using Rynek.Data;
using Rynek.Subscriber;
using System;
using System.Collections.Generic;

namespace RynekTest
{
    [TestFixture]
    public class KupujacySubscriberTest
    {
        private SprzedajacyCenyProvider provider;
        [SetUp]
        public void Setup()
        {
            provider = new SprzedajacyCenyProvider();
        }

        [Test]
        public void CheckIfTestWorks()
        {
            Assert.Pass();
        }
        [Test]
        public void CheckIfChange()
        {
            KupujacySubscriber subscriber = new KupujacySubscriber(provider);
            List<CartItem> items = new List<CartItem>();
            items.Add(new Book("978-83-8222-309-5", 0.15, 22.54, 1));
            provider.SetMeasurments(items);
            Assert.That(subscriber.Data.Produkt[0].KosztWytworzenia, Is.EqualTo(22.54).Within(0.001));
        }
        [Test]
        public void CheckIfChange2()
        {
            KupujacySubscriber subscriber = new KupujacySubscriber(provider);
            List<CartItem> items = new List<CartItem>();
            items.Add(new Fruit("Tangerine", 0.25, 4.0, 3));
            provider.SetMeasurments(items);
            items.Add(new Fruit("Pomelo", 0.25, 6.0, 2));
            provider.SetMeasurments(items);
            Assert.That(subscriber.Data.Produkt[1].KosztWytworzenia, Is.EqualTo(6.0).Within(0.001));
        }
    }
}