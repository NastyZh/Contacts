using System.Windows;
using Сontacts.Models;
using System.Collections.Generic;
using System.Data.SQLite;
using Dapper;
using Сontacts.Data;

namespace Сontacts;

public partial class SearchWindow : Window
{
    private SQLiteConnection _connection;
    private DatabaseHelper _databaseHelper;

    public SearchWindow()
    {
        InitializeComponent();
        _databaseHelper = new DatabaseHelper();
    }

    // private void GetAbonentClick(object sender, RoutedEventArgs e)
    // {
    //     if (int.TryParse(txtId.Text, out int abonentId))
    //     {
    //         Abonent abonent = _databaseHelper.GetAbonent(abonentId);
    //         MessageBox.Show($"Имя абонента: {abonent.FullName}\nУлица: {abonent.Street.StreetName}\nНомер телефона: {abonent.PhoneNumber.Number}");

    //         // Здесь вы можете использовать полученного абонента для отображения информации в вашем приложении
    //     }
    //     else
    //     {
    //         MessageBox.Show("Введите корректный ID абонента");
    //     }

    //    
    // }
    private void GetAbonentClick(object sender, RoutedEventArgs e)
    {
        if (int.TryParse(txtId.Text, out int abonentId))
        {
            Abonent abonent = _databaseHelper.GetAbonent(abonentId);
            if (abonent != null)
            {
                MessageBox.Show($"Имя абонента: {abonent.FullName}\nУлица: {abonent.Address}\nНомер телефона: {abonent.PhoneNumber}");
            }
            else
            {
                MessageBox.Show("Абонент с указанным ID не найден.");
            }
        }
        else
        {
            MessageBox.Show("Введите корректный ID абонента");
        }
    }

}