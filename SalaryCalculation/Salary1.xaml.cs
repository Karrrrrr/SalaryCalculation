using Microsoft.Office.Interop.Excel;
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
    /// Логика взаимодействия для Salary1.xaml
    /// </summary>
    public partial class Salary1 : System.Windows.Window
    {
        public Salary1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SalaryChange sc = new SalaryChange();
            sc.ShowDialog();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            SalaryGrid.Children.Clear();
            SalaryGrid.ColumnDefinitions.Clear();
            SalaryGrid.RowDefinitions.Clear();
            for (int i = 1; i < 6; i++)
            {
                SalaryGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            SalaryGrid.RowDefinitions.Add(new RowDefinition());
            TextBlock workerTitle = new TextBlock() { Text = "Работник", FontSize = 20, FontWeight = FontWeights.Bold, TextAlignment = TextAlignment.Center };
            SalaryGrid.Children.Add(workerTitle);
            Grid.SetColumn(workerTitle, 0);
            Grid.SetRow(workerTitle, 0);
            TextBlock monthYearTitle = new TextBlock() { Text = "Месяц и год", FontSize = 20, FontWeight = FontWeights.Bold, TextAlignment = TextAlignment.Center };
            SalaryGrid.Children.Add(monthYearTitle);
            Grid.SetColumn(monthYearTitle, 1);
            Grid.SetRow(monthYearTitle, 0);
            TextBlock accuredTitle = new TextBlock() { Text = "Начислено", FontSize = 20, FontWeight = FontWeights.Bold, TextAlignment = TextAlignment.Center };
            SalaryGrid.Children.Add(accuredTitle);
            Grid.SetColumn(accuredTitle, 2);
            Grid.SetRow(accuredTitle, 0);
            TextBlock withheldTitle = new TextBlock() { Text = "Удержано", FontSize = 20, FontWeight = FontWeights.Bold, TextAlignment = TextAlignment.Center };
            SalaryGrid.Children.Add(withheldTitle);
            Grid.SetColumn(withheldTitle, 3);
            Grid.SetRow(withheldTitle, 0);
            TextBlock paidTitle = new TextBlock() { Text = "Выплачено", FontSize = 20, FontWeight = FontWeights.Bold, TextAlignment = TextAlignment.Center };
            SalaryGrid.Children.Add(paidTitle);
            Grid.SetColumn(paidTitle, 4);
            Grid.SetRow(paidTitle, 0);
            using (SalaryCalculationEntities db = new SalaryCalculationEntities())
            {
                foreach (Salary salary in db.Salary)
                {
                    SalaryGrid.RowDefinitions.Add(new RowDefinition());
                    TextBlock worker = new TextBlock() { Text = db.Worker.Find(salary.WorkerID).FullName, FontSize = 20, TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap, };
                    SalaryGrid.Children.Add(worker);
                    Grid.SetColumn(worker, 0);
                    Grid.SetRow(worker, SalaryGrid.RowDefinitions.Count - 1);
                    TextBlock monthYear = new TextBlock() { Text = salary.MonthYear, FontSize = 20, TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap, };
                    SalaryGrid.Children.Add(monthYear);
                    Grid.SetColumn(monthYear, 1);
                    Grid.SetRow(monthYear, SalaryGrid.RowDefinitions.Count - 1);
                    TextBlock accured = new TextBlock() { Text = decimal.Round(salary.Accured, 2).ToString() + "₽", FontSize = 20, TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap, };
                    SalaryGrid.Children.Add(accured);
                    Grid.SetColumn(accured, 2);
                    Grid.SetRow(accured, SalaryGrid.RowDefinitions.Count - 1);
                    TextBlock withheld = new TextBlock() { Text = decimal.Round(salary.Withheld, 2).ToString() + "₽", FontSize = 20, TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap, };
                    SalaryGrid.Children.Add(withheld);
                    Grid.SetColumn(withheld, 3);
                    Grid.SetRow(withheld, SalaryGrid.RowDefinitions.Count - 1);
                    TextBlock paid = new TextBlock() { Text = decimal.Round(salary.Paid, 2).ToString() + "₽", FontSize = 20, TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap, };
                    SalaryGrid.Children.Add(paid);
                    Grid.SetColumn(paid, 4);
                    Grid.SetRow(paid, SalaryGrid.RowDefinitions.Count - 1);
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            List<Salary> salaries = new List<Salary>();
            List<string> workers = new List<string>();
            using (SalaryCalculationEntities db = new SalaryCalculationEntities())
            {
                foreach (Salary salary in db.Salary)
                {
                    salaries.Add(salary);
                    workers.Add(salary.Worker.FullName);
                }
            }
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.SheetsInNewWorkbook = 1;
            Workbook workbook = app.Workbooks.Add(Type.Missing);
            Worksheet worksheet = app.Worksheets.Item[1];
            worksheet.Cells[1][1] = "Работник";
            worksheet.Cells[2][1] = "Месяц и год";
            worksheet.Cells[3][1] = "Начислено";
            worksheet.Cells[4][1] = "Удержано";
            worksheet.Cells[5][1] = "Выплачено";
            for (int i = 0; i < salaries.Count; i++)
            {
                worksheet.Cells[1][i + 2] = workers[i];
                worksheet.Cells[2][i + 2] = salaries[i].MonthYear;
                worksheet.Cells[3][i + 2] = salaries[i].Accured;
                worksheet.Cells[4][i + 2] = salaries[i].Withheld;
                worksheet.Cells[5][i + 2] = salaries[i].Paid;
            }
            Range borders = worksheet.Range[worksheet.Cells[1][1], worksheet.Cells[5][salaries.Count + 1]];
            borders.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = borders.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = borders.Borders[XlBordersIndex.xlEdgeTop].LineStyle = borders.Borders[XlBordersIndex.xlEdgeRight].LineStyle = borders.Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = borders.Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlContinuous;
            worksheet.Columns.AutoFit();
            app.Visible = true;
            app.ActiveWorkbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, @"D:\Salary.pdf");
        }
    }
}
