using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using zd3_biblioteka;

namespace zd3_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //хранилище операторов
        private Operatorlist storage = new Operatorlist();
        public MainWindow()
        {
            InitializeComponent();
        }

        //включение/выключение поля потомка
        private void IsPlusBox_Changed(object sender, RoutedEventArgs e)
        {
            //если панель ещё не готова (вызов при загрузке окна)
            if (PlusPanel == null)
            {
                return;
            }
            else
            {
                if (IsPlusBox.IsChecked == true)
                {
                    PlusPanel.IsEnabled = true;
                }
                else
                {
                    PlusPanel.IsEnabled = false;
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //числовые переменные
                double price = 0;
                double coverage = 0;
                int subs = 0;
                int year = 0;

                //проверяем имя
                if (NameBox.Text.Trim() == "")
                {
                    MessageBox.Show("Введите название.");
                    return;
                }

                //проверка стоимости минуты
                if (double.TryParse(PriceBox.Text, out price) == false)
                {
                    MessageBox.Show("Стоимость минуты должна быть числом.");
                    return;
                }
                else
                {
                    if (price <= 0)
                    {
                        MessageBox.Show("Стоимость минуты должна быть больше нуля.");
                        return;
                    }
                }

                //проверка покрытие
                if (double.TryParse(CoverageBox.Text, out coverage) == false)
                {
                    MessageBox.Show("Площадь покрытия должна быть числом.");
                    return;
                }
                else
                {
                    if (coverage < 0)
                    {
                        MessageBox.Show("Площадь покрытия не может быть отрицательной.");
                        return;
                    }
                }
                //проверка абонентов
                if (int.TryParse(SubsBox.Text, out subs) == false)
                {
                    MessageBox.Show("Кол-во абонентов должно быть целым числом.");
                    return;
                }
                else
                {
                    if (subs < 0)
                    {
                        MessageBox.Show("Кол-во абонентов не может быть отрицательным.");
                        return;
                    }
                }
                //проверка год
                if (int.TryParse(YearBox.Text, out year) == false)
                {
                    MessageBox.Show("Год должен быть целым числом.");
                    return;
                }
                //если выбран потомок
                if (IsPlusBox.IsChecked == true)
                {
                    double feeAmount = 0;
                    //проверка размера платы (если введён)
                    if (double.TryParse(FeeAmountBox.Text, out feeAmount) == false)
                    {
                        feeAmount = 0;
                    }

                    //определение флагов
                    bool hasFee = false;
                    if (FeeBox.IsChecked == true)
                    {
                        hasFee = true;
                    }
                    else
                    {
                        hasFee = false;
                    }

                    bool hasNet = false;
                    if (UnlimBox.IsChecked == true)
                    {
                        hasNet = true;
                    }
                    else
                    {
                        hasNet = false;
                    }

                    //объект потомка
                    OperatorPlus op = new OperatorPlus(NameBox.Text.Trim(), price, coverage, subs, year, hasFee, feeAmount, hasNet);

                    //перегрузка Add(Operator)
                    storage.Add(op);
                }
                else
                {
                    //перегрузка Add(параметры)
                    storage.Add(NameBox.Text.Trim(), price, coverage, subs, year);
                }

                RefreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ошибка: {ex.Message}");
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (OperatorsList.SelectedItem == null)
                {
                    MessageBox.Show("Выберите оператора в списке.");
                    return;
                }
                else
                {
                    Operator selected = OperatorsList.SelectedItem as Operator;

                    //перегрузка Remove(Operator)
                    bool removed = storage.Remove(selected);

                    if (removed == true)
                    {
                        RefreshList();
                    }
                    else
                    {
                        MessageBox.Show("Не удалось удалить оператора.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}");
            }
        }

        private void BestButton_Click(object sender, RoutedEventArgs e)
        {
            Operator best = storage.BestOperator();

            if (best == null)
            {
                MessageBox.Show("Список пуст.");
                return;
            }
            else
            {
                MessageBox.Show($"Лучший:\n{best.ToString()}");
            }
        }

        private void AvgButton_Click(object sender, RoutedEventArgs e)
        {
            double avg = storage.AverageQuality();
            MessageBox.Show($"Среднее качество: {avg}");
        }

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            //получение отсортированного списка
            List<Operator> sorted = storage.SortedByQuality();
            OperatorsList.Items.Clear();
            foreach (Operator op in sorted)
            {
                OperatorsList.Items.Add(op);
            }
        }

        //обновить список на экране
        private void RefreshList()
        {
            OperatorsList.Items.Clear();
            foreach (Operator op in storage.Operators)
            {
                //ListBox покажет op через ToString()
                OperatorsList.Items.Add(op);
            }
        }

    }
}
