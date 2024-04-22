using System.IO;
using System.Text;
using System.Windows;
using Contacts;
using Сontacts.Data;
using Сontacts.Models;

namespace Сontacts;

public partial class MainWindow : Window
{
    private DatabaseHelper _dbHelper=new DatabaseHelper();

    public MainWindow()
    {
        InitializeComponent();

        LoadAbonent();
    }

    private void OpenModalSearchNumberClick(object sender, RoutedEventArgs e)
    {
        SearchNumber searchNumber = new SearchNumber();
        searchNumber.ShowDialog();
    }

    private void OpenModalWindowClick(object sender, RoutedEventArgs e)
    {
        SearchWindow searchWindow = new SearchWindow();
        searchWindow.ShowDialog();
            //LoadAbonent();
    }

    private void OpenModalWindowStreets(object sender, RoutedEventArgs e)
    {
        Streets streets = new Streets();
        streets.ShowDialog();
    }

    private void ExportToCSVClick(object sender, RoutedEventArgs e)
    {
        var abonents = LoadAbonents();

        // Создаем строку заголовков CSV файла
        string header = "ФИО,Улица,Номер дома" + Environment.NewLine;

        // Создаем строки данных для CSV файла
        StringBuilder csvData = new StringBuilder();
        foreach (var abonent in abonents)
        {
            csvData.AppendLine(
                $"{abonent.FullName},{abonent.Address.Street},{abonent.Address.NumberHouse}{abonent.PhoneNumber.Number},{abonent.PhoneNumber.Type}");
        }

        // Сохраняем данные в файл CSV
        string filePath = "abonents.csv";
        File.WriteAllText(filePath, header + csvData.ToString());

        MessageBox.Show("Данные успешно выгружены в CSV файл: " + filePath);
    }

    private List<Abonent> LoadAbonents()
    {
          List<Abonent> abonents = _dbHelper.GetAbonents();

        return abonents;
    }

    private void LoadAbonent()
    {
        List<Abonent> abonents = _dbHelper.GetAbonents();
        dataGrid.ItemsSource = abonents;
    }
}