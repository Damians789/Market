using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rynek.Data;

namespace Rynek.Visitors
{
    internal class ShoppingCartVisitorImpl : ShoppingCartVisitor
    {
        public double Visit(Book book) => book.KosztWytworzenia * book.Marza;
        public double Visit(Fruit fruit) => fruit.KosztWytworzenia * fruit.Marza;
        public double Visit(Toy toy) => toy.KosztWytworzenia * toy.Marza;
    }
}
