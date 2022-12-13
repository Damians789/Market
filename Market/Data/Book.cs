using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rynek.Visitors;

namespace Rynek.Data
{
    public class Book : CartItem
    {
        public String IsbnNumber { get; set; }
        public Book() { }

        public Book(string isbnNumber, double marza, double wytworzenie, int ilosc)
        {
            if (!CheckISBN13(isbnNumber))
                throw new Exception("Niepoprawny isbn");
            IsbnNumber = isbnNumber;
            Marza = marza;
            KosztWytworzenia = wytworzenie;
            DostepnaIlosc = ilosc;
        }

        public override double Accept(ShoppingCartVisitor visitor) => visitor.Visit(this);

        static bool CheckISBN13(string code)
        {
            code = code.Replace("-", "").Replace(" ", "");
            if (code.Length != 13) return false;
            int sum = 0;
            foreach (var (index, digit) in code.Select((digit, index) => (index, digit)))
            {
                if (char.IsDigit(digit)) sum += (digit - '0') * (index % 2 == 0 ? 1 : 3);
                else return false;
            }
            return sum % 10 == 0;
        }
    }
}
