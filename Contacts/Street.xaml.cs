using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Сontacts.Data;
using Сontacts.Models;

namespace Сontacts;

public partial class Streets : Window
{
    public Streets()
    {
        InitializeComponent();
        GetStreets();
    }

    private void GetStreets()
    {
      
        DatabaseHelper db = new DatabaseHelper();
        List<Abonent> abonents = db.GetAbonents();
        var groupedByStreet = abonents.GroupBy(abonent => abonent.Address.Street);
        List<Street> streets = new List<Street>();
        foreach (var group in groupedByStreet)
        {
            streets.Add(new Street
            {
                StreetName=group.Key,
                AbonentCount = group.Count()
            });
        }
        dataGrid.ItemsSource = streets;
    }

   
}