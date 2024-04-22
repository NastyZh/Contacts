using System.Windows;
using Сontacts.Data;
using Сontacts.Models;
using System.Linq;

namespace Сontacts
{
    public partial class SearchNumber : Window
    {
        private readonly DatabaseHelper _db;

        public SearchNumber()
        {
            InitializeComponent();
            _db = new DatabaseHelper(); // Создаем экземпляр DatabaseHelper при инициализации окна
        }

        private void SelectPhoneNumberTypeClick(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtNumber.Text, out int abonentNumber))
            {
                var abonents = _db.SearchAbonentsByPhoneNumber(abonentNumber.ToString()); // Вызываем метод поиска абонентов по номеру телефона
                dataGrid.ItemsSource = abonents; // Отображаем результаты в DataGrid
            }
        }
    }
}