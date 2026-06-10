using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zd3_biblioteka
{
    //базовый класс оператор мобильной связи
    public class Operator
    {
        //название оператора
        public string Name { get; set; }

        //стоимость одной минуты разговора
        public double PriceMinute { get; set; }

        //площадь покрытия
        public double Coverage { get; set; }

        //доп. поля:
        //1 кол-во абонентов
        public int Subs { get; set; }

        //2 год основания
        public int FoundationYear { get; set; }


        //констуктор по умолчанию
        public Operator()
        {
            Name = "";
            PriceMinute = 1;
            Coverage = 0;
            Subs = 0;
            FoundationYear = 2000;
        }

        //конструктор с параметрами
        public Operator(string name, double price, double coverage, int subs, int foundYear)
        {
            Name = name;
            PriceMinute = price;
            Coverage = coverage;
            Subs = subs;
            FoundationYear = foundYear;
        }

        //функция для качества q = 100 * площадь покрытия / стоимость 1 минуты

        public double Quality()
        {
            //защита от деления на ноль
            if (PriceMinute == 0)
            {
                return 0;
            }
            else
            {
                return 100 * Coverage / PriceMinute;
            }
        }
        //вывод информации об объекте
        public string Info()
        {
            return $"Оператор: {Name}; Цена/мин: {PriceMinute}; " + $"Покрытие: {Coverage}; Абонентов: {Subs}; " +  $"Год: {FoundationYear}; Q = {Quality()}";
        }
        //чтобы listbox показывал текст через Info()
        public override string ToString()
        {
            return Info();
        }
    }
}
