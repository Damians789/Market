using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rynek.Visitors;

namespace Rynek.Data
{
    public class Fruit : CartItem
    {
        private string _nazwa;
        public string Name {
            get
            {
                return this._nazwa;
            }
            set
            {
                if (!check2(value))
                {
                    throw new Exception("To nie jest poprawna nazwa");
                }
                this._nazwa = value;
            }
        }
        public Fruit() { }

        public Fruit(string name, double marza, double wytworzenie, int ilosc)
        {
            if (!check2(name))
                throw new Exception("Niepoprawna nazwa");
            Name = name;
            Marza = marza;
            KosztWytworzenia = wytworzenie;
            DostepnaIlosc = ilosc;
        }

        public override double Accept(ShoppingCartVisitor visitor) => visitor.Visit(this);

        private bool check1(string value)
        {
            foreach (char c in value)
            {

                if (!char.IsLetter(c))
                    if (c.Equals(" "))
                        continue;
                    else
                        return false;
            }

            return true;
        }
        private bool check2(object value)
        {
            string str = value as string;
            return str != null && check1(str);
        }
    }
}
