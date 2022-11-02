using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SalaryCalculation
{
    /// <summary>
    /// Логика взаимодействия для SalaryChange.xaml
    /// </summary>
    public partial class SalaryChange : Window
    {
        public SalaryChange()
        {
            InitializeComponent();
        }

        string[] months = { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (WorkerCreateCB.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите работника");
            }
            else if (MonthYearCreateTB.Text == "")
            {
                MessageBox.Show("Введите месяц и год");
            }
            else if ((MonthYearCreateTB.Text.Split(' ').Length != 2) || (MonthYearCreateTB.Text.Split(' ')[1].Length != 4) || (!int.TryParse(MonthYearCreateTB.Text.Split(' ')[1], out int n)) || (!months.Contains(MonthYearCreateTB.Text.Split()[0])))
            {
                MessageBox.Show("Неверный формат месяца и года");
            }
            else if (MinSalaryCreateTB.Text == "")
            {
                MessageBox.Show("Введите минимальную оплату труда");
            }
            else if (!decimal.TryParse(MinSalaryCreateTB.Text, out decimal m))
            {
                MessageBox.Show("Неверный формат минимальной оплаты труда");
            }
            else if (decimal.Parse(MinSalaryCreateTB.Text) <= 0)
            {
                MessageBox.Show("Слишком маленькая минимальная оплата труда");
            }
            else
            {
                Salary salary = new Salary() { MonthYear = MonthYearCreateTB.Text, Accured = decimal.Parse(AccuredCreateTB.Text), Withheld = decimal.Parse(WithheldCreateTB.Text), Paid = decimal.Parse(PaidCreateTB.Text) };
                using (SalaryCalculationEntities db = new SalaryCalculationEntities())
                {
                    foreach (Worker worker in db.Worker)
                    {
                        if (WorkerCreateCB.SelectedItem.ToString() == worker.FullName)
                        {
                            salary.Worker = worker;
                            salary.WorkerID = worker.ID;
                            break;
                        }
                    }
                    db.Salary.Add(salary);
                    db.SaveChanges();
                    MessageBox.Show("Запись успешно создана");
                }
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ChangeTC.SelectedIndex == 0)
            {
                WorkerCreateCB.Items.Clear();
                MonthYearCreateTB.Text = "";
                MinSalaryCreateTB.Text = "";
                AccuredCreateTB.Text = "";
                WithheldCreateTB.Text = "";
                PaidCreateTB.Text = "";
                using (SalaryCalculationEntities db = new SalaryCalculationEntities())
                {
                    foreach (Worker worker in db.Worker)
                    {
                        WorkerCreateCB.Items.Add(worker.FullName);
                    }
                }
            }
            else if (ChangeTC.SelectedIndex == 1)
            {
                SalaryChangeCB.Items.Clear();
                WorkerChangeCB.Items.Clear();
                WorkerChangeCB.IsEnabled = false;
                MonthYearChangeTB.Text = "";
                MinSalaryChangeTB.Text = "";
                AccuredChangeTB.Text = "";
                WithheldChangeTB.Text = "";
                PaidChangeTB.Text = "";
                using (SalaryCalculationEntities db = new SalaryCalculationEntities())
                {
                    foreach (Salary salary in db.Salary)
                    {
                        SalaryChangeCB.Items.Add(salary.ID);
                    }
                    foreach (Worker worker in db.Worker)
                    {
                        WorkerChangeCB.Items.Add(worker.FullName);
                    }
                }
            }
            else if (ChangeTC.SelectedIndex == 2)
            {
                SalaryDeleteCB.Items.Clear();
                using (SalaryCalculationEntities db = new SalaryCalculationEntities())
                {
                    foreach (Salary salary in db.Salary)
                    {
                        SalaryDeleteCB.Items.Add(salary.ID);
                    }
                }

            }
        }

        private void WorkerCreateCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
            MinSalaryCreateTB.Text = "30000";
        }

        private void SalaryChangeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
            if (SalaryChangeCB.Items.Count != 0)
            {
                WorkerChangeCB.IsEnabled = true;
                using (SalaryCalculationEntities db = new SalaryCalculationEntities())
                {
                    Salary salary = db.Salary.Find(int.Parse(SalaryChangeCB.SelectedItem.ToString()));
                    WorkerChangeCB.SelectedItem = salary.Worker.FullName;
                    MonthYearChangeTB.Text = salary.MonthYear;
                    MinSalaryChangeTB.Text = "";
                    AccuredChangeTB.Text = salary.Accured.ToString();
                    WithheldChangeTB.Text = salary.Withheld.ToString();
                    PaidChangeTB.Text = salary.Paid.ToString();
                }
            }
        }

        private void WorkerChangeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }

        private void SalaryDeleteCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }

        private void MinSalaryCreateTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (decimal.TryParse(MinSalaryCreateTB.Text, out decimal n))
            {
                using (SalaryCalculationEntities db = new SalaryCalculationEntities())
                {
                    foreach (Worker w in db.Worker)
                    {
                        if (WorkerCreateCB.SelectedItem.ToString() == w.FullName)
                        {
                            AccuredCreateTB.Text = (decimal.Parse(MinSalaryCreateTB.Text) * w.Rank1.Coefficient * 1.15m).ToString();
                            if (w.UnionMembership)
                            {
                                WithheldCreateTB.Text = (decimal.Parse(AccuredCreateTB.Text) * 0.15m).ToString();
                            }
                            else
                            {
                                WithheldCreateTB.Text = (decimal.Parse(AccuredCreateTB.Text) * 0.14m).ToString();
                            }
                            PaidCreateTB.Text = (decimal.Parse(AccuredCreateTB.Text) - decimal.Parse(WithheldCreateTB.Text)).ToString();
                        }
                    }
                }
            }
            else
            {
                AccuredCreateTB.Text = "";
                WithheldCreateTB.Text = "";
                PaidCreateTB.Text = "";
            }
        }

        private void MinSalaryChangeTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (decimal.TryParse(MinSalaryChangeTB.Text, out decimal n))
            {
                using (SalaryCalculationEntities db = new SalaryCalculationEntities())
                {
                    foreach (Worker w in db.Worker)
                    {
                        if (WorkerChangeCB.SelectedItem.ToString() == w.FullName)
                        {
                            AccuredChangeTB.Text = (decimal.Parse(MinSalaryChangeTB.Text) * w.Rank1.Coefficient * 1.15m).ToString();
                            if (w.UnionMembership)
                            {
                                WithheldChangeTB.Text = (decimal.Parse(AccuredChangeTB.Text) * 0.15m).ToString();
                            }
                            else
                            {
                                WithheldChangeTB.Text = (decimal.Parse(AccuredChangeTB.Text) * 0.14m).ToString();
                            }
                            PaidChangeTB.Text = (decimal.Parse(AccuredChangeTB.Text) - decimal.Parse(WithheldChangeTB.Text)).ToString();
                            break;
                        }
                    }
                }
            }
            else
            {
                AccuredChangeTB.Text = "";
                WithheldChangeTB.Text = "";
                PaidChangeTB.Text = "";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (SalaryChangeCB.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите запись");
            }
            else if (MonthYearChangeTB.Text == "")
            {
                MessageBox.Show("Введите месяц и год");
            }
            else if ((MonthYearChangeTB.Text.Split(' ').Length != 2) || (MonthYearChangeTB.Text.Split(' ')[1].Length != 4) || (!int.TryParse(MonthYearChangeTB.Text.Split(' ')[1], out int n)) || (!months.Contains(MonthYearChangeTB.Text.Split()[0])))
            {
                MessageBox.Show("Неверный формат месяца и года");
            }
            else if ((MinSalaryChangeTB.Text == "") && (AccuredChangeTB.Text == ""))
            {
                MessageBox.Show("Введите минимальную оплату труда");
            }
            else if ((AccuredChangeTB.Text == "") && (!decimal.TryParse(MinSalaryChangeTB.Text, out decimal m)))
            {
                MessageBox.Show("Неверный формат минимальной оплаты труда");
            }
            else if (decimal.Parse(AccuredChangeTB.Text) <= 0)
            {
                MessageBox.Show("Слишком маленькая минимальная оплата труда");
            }
            else
            {
                using (SalaryCalculationEntities db = new SalaryCalculationEntities())
                {
                    foreach (Worker worker in db.Worker)
                    {
                        if (WorkerChangeCB.SelectedItem.ToString() == worker.FullName)
                        {
                            db.Salary.Find(int.Parse(SalaryChangeCB.SelectedItem.ToString())).Worker = worker;
                            db.Salary.Find(int.Parse(SalaryChangeCB.SelectedItem.ToString())).WorkerID = worker.ID;
                            break;
                        }
                    }
                    db.Salary.Find(int.Parse(SalaryChangeCB.SelectedItem.ToString())).MonthYear = MonthYearChangeTB.Text;
                    db.Salary.Find(int.Parse(SalaryChangeCB.SelectedItem.ToString())).Accured = decimal.Parse(AccuredChangeTB.Text);
                    db.Salary.Find(int.Parse(SalaryChangeCB.SelectedItem.ToString())).Withheld = decimal.Parse(WithheldChangeTB.Text);
                    db.Salary.Find(int.Parse(SalaryChangeCB.SelectedItem.ToString())).Paid = decimal.Parse(PaidChangeTB.Text);
                    db.SaveChanges();
                    MessageBox.Show("Изменения успешно применены");
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (SalaryDeleteCB.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите запись");
            }
            else
            {
                using (SalaryCalculationEntities db = new SalaryCalculationEntities())
                {
                    db.Salary.Remove(db.Salary.Find(int.Parse(SalaryDeleteCB.SelectedItem.ToString())));
                    db.SaveChanges();
                    SalaryDeleteCB.Items.Remove(SalaryDeleteCB.SelectedItem);
                    MessageBox.Show("Запись успешно удалена");
                }
            }
        }
    }
}
