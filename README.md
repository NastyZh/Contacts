  Contacts
  
Проект предназначен для хранения и поиска информации абонентов.

  Что имеется:

1)Основное окно - вывод информации об абонентах в формате таблицы и набор кнопок для прочих возможностей. 
Предоставленный набор кнопок:

- "Поиск" - вызов модального окна "Поиск по номеру" 
- "Выгрузить CSV" - для запуска механизма выгрузки CSV 
- "Улицы" - для вызова модального окна "Улицы"
  
Имеющиеся колонки в отображаемой таблицы:

- ФИО абонента 
- Улица , Номер дома 
- Номер телефона (домашний, рабочий, мобильный)
Не предусмотрены механизмы фильтрации и сортировки отображаемой таблицы по всем колонкам.

2) Модальное окно "Поиск по номеру" - содержит текстовое поле для ввода номера.
   
При успехе поиска ожидается вывод в таблице только совпавших по критерию поиска абонентов, 
при отсутствии совпадений ожидается информативное окно с текстом "Нет абонентов, удовлетворяющих критерию поиска".

Имеющиеся номера в базе данных : 123456789 , 987654321 ,  3453564345 , 37754457 , 44493382 , 22849939

3) Модальное окно "Улицы" отображает информацию об обслуживаемых улицах и количестве абонентов на каждой из них в табличном формате.
   - не получилось это сделать, нужно доработать.
     
4) Кнопка "Выгрузить CSV" запускает механизм формирования файла report_{текущая дата и время}.csv, в котором содержится информация из таблицы основного окна с учётом фильтров и сортировки.
