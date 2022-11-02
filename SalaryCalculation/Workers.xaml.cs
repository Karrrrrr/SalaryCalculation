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
using System.Windows.Shapes;

namespace SalaryCalculation
{
    /// <summary>
    /// Логика взаимодействия для Workers.xaml
    /// </summary>
    public partial class Workers : Window
    {
        public Workers()
        {
            InitializeComponent();
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                using (SalaryCalculationEntities db = new SalaryCalculationEntities())
                {
                    foreach (Worker worker in db.Worker)
                    {
                        WorkersGrid.RowDefinitions.Add(new RowDefinition());
                        TextBlock fullName = new TextBlock() { Text = worker.FullName, FontSize = 20, TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap, };
                        WorkersGrid.Children.Add(fullName);
                        Grid.SetColumn(fullName, 0);
                        Grid.SetRow(fullName, WorkersGrid.RowDefinitions.Count - 1);
                        TextBlock position = new TextBlock() { Text = worker.Position, FontSize = 20, TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap, };
                        WorkersGrid.Children.Add(position);
                        Grid.SetColumn(position, 1);
                        Grid.SetRow(position, WorkersGrid.RowDefinitions.Count - 1);
                        TextBlock rank = new TextBlock() { Text = worker.Rank.ToString(), FontSize = 20, TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap, };
                        WorkersGrid.Children.Add(rank);
                        Grid.SetColumn(rank, 2);
                        Grid.SetRow(rank, WorkersGrid.RowDefinitions.Count - 1);
                        TextBlock union = new TextBlock() { FontSize = 20, TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap, };
                        if (worker.UnionMembership)
                        {
                            union.Text = "Есть";
                        }
                        else
                        {
                            union.Text = "Нет";
                        }
                        WorkersGrid.Children.Add(union);
                        Grid.SetColumn(union, 3);
                        Grid.SetRow(union, WorkersGrid.RowDefinitions.Count - 1);
                    }
                }
            }
        }
    }
}
