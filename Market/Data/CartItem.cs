using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rynek.Visitors;

namespace Rynek.Data
{
    public abstract class CartItem
    {
        public double DostepnaIlosc;
        public double SprzedanaIlosc;
        private double _marza;
        public double Marza { 
            get {
                return this._marza;
            }
            set{
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value");
                else
                    this._marza = value;
            }
        }
        private double _koszt;
        public double KosztWytworzenia {
            get
            {
                return this._koszt;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value");
                else
                    this._koszt = value;
            }
        }
        public abstract double Accept(ShoppingCartVisitor visitor);
    }
}
