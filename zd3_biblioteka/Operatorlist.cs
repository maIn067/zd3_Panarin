using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zd3_biblioteka
{
    //класс, который хранит операторы, методы и пр.
    public class Operatorlist
    {
        //1 коллекция: список операторов 
        private List<Operator> operators = new List<Operator>();

        //2 коллекция словарь для поиска по названию (ключ - имя, значение - объект)
        private Dictionary<string, Operator> byName = new Dictionary<string, Operator>();

        //доступ к списку для пользователя
        public List<Operator> Operators 
        {
            get { return operators; }
        }

        //1 перегрузка - добавление готового объекта
        public void Add (Operator op)
        {
            if (op == null)
            {
                return;
            }
            else
            {
                operators.Add(op);

                //добавление в словарь (или обновление данных)
                if (byName.ContainsKey(op.Name))
                {
                    byName[op.Name] = op;
                }
                else
                {
                    byName.Add(op.Name, op);
                }
            }
        }

        //2 перегрузка добавление по параметрам
        public void Add (string name, double priceminute, double coverage, int subs, int foundyear)
        {
            //создание объекта и переиспользование первой перегрузки
            Operator op = new Operator(name, priceminute, coverage, subs, foundyear);
            Add(op);
        }

        //1 удаление по объекту
        public bool Remove(Operator op)
        {
            if (op == null)
            {
                return false;
            }
            else
            {
                //убирает из словаря если есть
                if (byName.ContainsKey(op.Name))
                {
                    byName.Remove(op.Name);
                }
                return operators.Remove(op);
            }
        }

        //2 перегрузка удаление по названию
        public bool Remove(string name)
        {
            //поиск оператора по названию
            Operator found = operators.FirstOrDefault(o => o.Name == name);
            if (found == null)
            {
                return false;
            }
            else
            {
                return Remove(found);
            }
        }

        //поиск по названию через словарь
        public Operator FindByName(string name)
        {
            if (byName.ContainsKey(name))
            {
                return byName[name];
            }
            else
            {
                return null;
            }
        }

        //среднее качество
        public double AverageQuality()
        {
            if (operators.Count == 0)
            {
                return 0;
            }
            else
            {
                return operators.Average(o => o.Quality());
            }
        }

        //лучший оператор по качеству
        public Operator BestOperator()
        {
            return operators.OrderByDescending(o => o.Quality()).FirstOrDefault();
        }

        //список, отсорт. по качеству
        public List<Operator> SortedByQuality()
        {
            return operators.OrderByDescending(o => o.Quality()).ToList();
        }

        //операторы с покрытием больше заданного
        public List<Operator> WithCoverageAbove(double value)
        {
            return operators.Where(o => o.Coverage > value).ToList();
        }

        //количество операторов
        public int Count()
        {
            return operators.Count;
        }
    }
}
