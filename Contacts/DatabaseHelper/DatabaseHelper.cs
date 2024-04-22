using System.Data.SQLite;
using Dapper;
using Сontacts.Models;

namespace Сontacts.Data
{
    public class DatabaseHelper
    {
        private SQLiteConnection _connection;

        public DatabaseHelper()
        {
            _connection = new SQLiteConnection("Data Source=identifier.sqlite");
            _connection.Open();

            CreateTables();
            //SeedData();
        }

        private void CreateTables()
        {
            _connection.Execute(@"
                CREATE TABLE IF NOT EXISTS Abonent (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    FullName TEXT NOT NULL,
                    StreetId INTEGER,
                    PhoneNumberId INTEGER,
                    FOREIGN KEY (StreetId) REFERENCES Address(Id),
                    FOREIGN KEY (PhoneNumberId) REFERENCES PhoneNumber(Id)
                )");

            _connection.Execute(@"
                CREATE TABLE IF NOT EXISTS Address (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Street TEXT NOT NULL,
                    NumberHouse INTEGER NOT NULL
                )");

            _connection.Execute(@"
                CREATE TABLE IF NOT EXISTS PhoneNumber (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Number TEXT NOT NULL,
                    Type TEXT NOT NULL 
                )");
        }

        private void SeedData()
        {
          _connection.Execute(@"
              INSERT INTO Address (Street, NumberHouse) VALUES 
                  ('Мясникова', '1'), 
                  ('Грибоедова', '2'), 
                  ('Алишерова', '3')");
//
          _connection.Execute(@"
              INSERT INTO PhoneNumber (Number, Type) VALUES 
                  ('37754457', 'Mobile'), 
                  ('44493382', 'Home'),
                  ('22849939', 'Work')");
//
          _connection.Execute(@"
              INSERT INTO Abonent (FullName, StreetId, PhoneNumberId) VALUES 
                  ('Александров Иван Петрович', 1, 1),
                  ('Петров Петр Алексеевич', 2, 2),
                  ('Андреев Сидор Сидорович', 3, 3)");
        }

        public List<Abonent> GetAbonents()
        {
            var query = @"
                SELECT Abonent.Id, Abonent.FullName, 
                       Address.Street as Address, 
                       Address.NumberHouse as NumberHouse,
                       PhoneNumber.Number as PhoneNumber, 
                       PhoneNumber.Type as PhoneType
                FROM Abonent
                LEFT JOIN Address ON Abonent.StreetId = Address.Id
                LEFT JOIN PhoneNumber ON Abonent.PhoneNumberId = PhoneNumber.Id";

            var abonents = _connection.Query<Abonent, Address, PhoneNumber, Abonent>(
                query,
                (abonent, address, phoneNumber) =>
                {
                    abonent.Address = address;
                    abonent.PhoneNumber = phoneNumber;
                    return abonent;
                },
                splitOn: "Address, PhoneNumber"
            ).AsList();

            return abonents;
        }

        public Abonent GetAbonent(int abonentId)
        {
            var query = @"
                SELECT Abonent.Id, Abonent.FullName, 
                       Address.Street as Address, 
                       Address.NumberHouse as NumberHouse, 
                       PhoneNumber.Number as PhoneNumber, 
                       PhoneNumber.Type as PhoneType
                FROM Abonent
                LEFT JOIN Address ON Abonent.StreetId = Address.Id
                LEFT JOIN PhoneNumber ON Abonent.PhoneNumberId = PhoneNumber.Id
                WHERE Abonent.Id = @Id";

            var abonent = _connection.Query<Abonent, Address, PhoneNumber, Abonent>(
                query,
                (ab, address, phoneNumber) =>
                {
                    ab.Address = address;
                    ab.PhoneNumber = phoneNumber;
                    return ab;
                },
                param: new { Id = abonentId },
                splitOn: "Address, PhoneNumber"
            ).FirstOrDefault();

            return abonent;
        }

        public List<Abonent> SearchAbonentsByPhoneNumber(string phoneNumber)
        {
            var query = @"
        SELECT Abonent.Id, Abonent.FullName, 
               Address.Street as Street, 
               Address.NumberHouse as NumberHouse, 
               PhoneNumber.Number as Number, 
               PhoneNumber.Type as Type
        FROM Abonent
        LEFT JOIN Address ON Abonent.StreetId = Address.Id
        LEFT JOIN PhoneNumber ON Abonent.PhoneNumberId = PhoneNumber.Id
        WHERE PhoneNumber.Number = @PhoneNumber";

            var abonents = _connection.Query<Abonent, Address, PhoneNumber, Abonent>(
                query,
                (abonent, address, phoneNumberObj) =>
                {
                    abonent.Address = new Address
                        { Street = address.Street, NumberHouse = address.NumberHouse }; // Создаем новый объект Address
                    abonent.PhoneNumber = new PhoneNumber
                    {
                        Number = phoneNumberObj.Number, Type = phoneNumberObj.Type
                    }; // Создаем новый объект PhoneNumber
                    return abonent;
                },
                param: new { PhoneNumber = phoneNumber },
                splitOn: "Street, NumberHouse, Number, Type" // Исправляем разделитель
            ).AsList();

            return abonents;
        }


        public SQLiteConnection GetConnection()
        {
            return _connection;
        }

        public void CloseConnection()
        {
            if (_connection != null)
            {
                _connection.Close();
            }
        }
    }
}