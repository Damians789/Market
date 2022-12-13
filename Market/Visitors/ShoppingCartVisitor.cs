using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rynek.Data;

namespace Rynek.Visitors
{
    public interface ShoppingCartVisitor
    {
        double Visit(Book book);
        double Visit(Fruit fruit);
        double Visit(Toy toy);
    }
}
