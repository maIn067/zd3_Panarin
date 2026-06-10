using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zd3_biblioteka
{
    //класс потомок: оператор с платой за соединение
    public class OperatorPlus : Operator
    {
        //дополнительное поле p: наличие платы за соединение (t/f)
        public bool HasConnectionFee { get; set; }

        //размер платы за соединение
        public double ConnectionFeeAmount { get; set; }

        //наличие безлимитного интернета
        public bool HasUnlimitedInternet { get; set; }


        //конструктор по умолчанию
        public OperatorPlus() : base()
        {
            HasConnectionFee = false;
            ConnectionFeeAmount = 0;
            HasUnlimitedInternet = false;
        }

        //конструктор с параметрами вызывает конструктор базового класса
        public OperatorPlus(string name, double priceMinute, double coverage, int subs, int foundationYear, bool hasConnectionFee, double connectionFeeAmount, bool hasUnlimitedInternet) : base(name, priceMinute, coverage, subs, foundationYear)
        {
            HasConnectionFee = hasConnectionFee;
            ConnectionFeeAmount = connectionFeeAmount;
            HasUnlimitedInternet = hasUnlimitedInternet;
        }

        //перекрытие: Qp = 0,7*Q если P истина, иначе Qp = 1,5*Q
        public new double Quality()
        {
            //Q из базового класса
            double q = base.Quality();
            //если есть плата за соединение
            if (HasConnectionFee == true)
            {
                return 0.7 * q;
            }
            else
            {
                return 1.5 * q;
            }
        }
        //перекрытие вывода информации через new
        public new string Info()
        {
            //текст про плату за соединение
            string feeText = "нет";
            if (HasConnectionFee == true)
            {
                feeText = "да";
            }
            else
            {
                feeText = "нет";
            }

            //текст про безлимитный интернет
            string netText = "нет";
            if (HasUnlimitedInternet == true)
            {
                netText = "да";
            }
            else
            {
                netText = "нет";
            }
            return $"Оператор+: {Name}; Цена/мин: {PriceMinute}; " + $"Покрытие: {Coverage}; Абонентов: {Subs}; " + $"Год: {FoundationYear}; Плата за соединение: {feeText}; " + $"Размер платы: {ConnectionFeeAmount}; Безлимит: {netText}; " + $"Qp = {Quality()}";
        }
        //переопределение tostring, чтобы LB показывал новый Info()
        public override string ToString()
        {
            return Info();
        }
    }
}
