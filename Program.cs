using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
//ВАРИАНТ №42
namespace KursProj
{
    public class Program
    {
        static List<Car> car_list = new List<Car>();// Объявите 3 списка
        static List<Condition> condition_list = new List<Condition>();
        static List<Credentials> credentials_list = new List<Credentials>();
        static int[] m = { 6, 4, 7 }; // Количество полей в классе для каждого класса
        static StreamReader sr;

        [STAThread]
        static void Main(string[] args)
        {
            var fDialog = new OpenFileDialog()
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
            };
            int k = 0;

            for (int i = 0; i < 3; i++)
            {
                if (i == 0)
                {
                    Console.WriteLine("Нажмите любую кнопку для открытия файла с машинами...");
                }
                if (i == 1)
                {
                    Console.WriteLine("Нажмите любую кнопку для открытия файла с состояниями машин...");
                }
                if (i == 2)
                {
                    Console.WriteLine("Нажмите любую кнопку для открытия файла с учетными данными...");
                }
                Console.ReadKey();
                if (fDialog.ShowDialog() != DialogResult.OK)
                {
                    Console.WriteLine("Файл не был выбран!");
                    Console.ReadLine();
                    continue;
                }
                sr = new StreamReader(fDialog.FileName, Encoding.Default);
                File_Load(sr, i, m[i]);
                Console.Clear();
            }
            int n = -1; // Позиция в switch
            while (true)
            {
                Frame();// Вывод списка для выбора запроса
                if (!int.TryParse(Console.ReadLine(), out n))
                {
                    Console.WriteLine("Это не цифра, нажмите на Enter");
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                }
                switch (n)
                {
                    case 1:
                        Console.Clear();
                        ShowNotes();
                        Console.WriteLine("Запрос окончен, нажмите любую клавишу");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    case 2:
                        Console.Clear();
                        Console.Write("Введите адресс по которому хотите узнать список машин\nНапример: ул. Чкаловская: ");
                        string addres = Console.ReadLine();
                        SearchCarByAddres(addres);
                        Console.WriteLine("Запрос окончен, нажмите любую клавишу");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    case 3:
                        Console.Clear();
                        StolenCars();
                        Console.WriteLine("Запрос окончен, нажмите любую клавишу");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    case 4: //Владелец самоого дорогого автомобиля
                        Console.Clear();
                        OwnerMostExpensiveCar();
                        Console.WriteLine("Запрос окончен, нажмите любую клавишу");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    case 5://Какие автомобили чаще всего ремонтируются
                        Console.Clear();
                        MostRepairedCars();
                        Console.WriteLine("Запрос окончен, нажмите любую клавишу");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    case 6://соотношение между автомобилями в личной собственности и по найму
                        Console.Clear();
                        OperationsStatistic();
                        Console.WriteLine("Запрос окончен, нажмите любую клавишу");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    case 7: //Расход бензина для заданной машины и владельца
                        Console.Clear();
                        Console.WriteLine("Введите фамилию владельца: ");
                        string surname = Console.ReadLine();
                        Console.WriteLine("Введите модель автомобиля");
                        string model = Console.ReadLine();
                        PetrolConsumption(surname, model);
                        Console.WriteLine("Запрос окончен, нажмите любую клавишу");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    case 8: //Добавление автомобиля
                        Console.Clear();
                        while (true)
                        { //заносим машину в список
                            var auto = car_list.Last();
                            Console.WriteLine("Введите данные об автомобиле в формате: «Ключ;Фирма;Модель;Тип кузова;Расход бензина;Цена»");
                            Console.WriteLine($"Не используйте лишних пробелов, во избежания некорректности данных. Введите данные с ключом {auto.carKey + 1}.");
                            Console.WriteLine("Пример ввода: '1;Porshe;maccan;Хэтчбек;120;50000'.");
                            var line_with_car = Console.ReadLine();
                            string[] ms = new string[6];
                            ms = line_with_car.Split(';');
                            try
                            {
                                if (auto.carKey + 1 != Convert.ToInt32(ms[0]))
                                {
                                    Console.WriteLine("некорректный ключ!");
                                    Console.ReadKey();
                                    Console.Clear();
                                    continue;
                                }
                                car_list.Add(new Car()
                                {
                                    carKey = Convert.ToInt32(ms[0]),
                                    Firm = ms[1],
                                    Model = ms[2],
                                    Type = ms[3],
                                    Consumption = Convert.ToInt32(ms[4]),
                                    Cost = Convert.ToInt32(ms[5])
                                });
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("некорректный ввод!");
                                Console.ReadKey();
                                Console.Clear();
                                continue;
                            }
                            break;
                        }
                        while (true) //заносим учетные данные для машины
                        {
                            Console.WriteLine();
                            Console.WriteLine();
                            var credent = credentials_list.Last();
                            Console.WriteLine("Введите учетные данные автомобиля в формате: «Ключ;Владелец (фамилия);Гос. Номерной знак;Адрес;Наличие гаража;Год постановки на учет;Пробег».");
                            Console.WriteLine($"Не используйте лишних пробелов, во избежания некорректности данных. Введите данные с ключом {credent.creKey + 1}.");
                            Console.WriteLine("Пример ввода: '1;Иванов;А205УЕ;ул. Чкаловская;1;2015;300000'.");
                            var line_with_credent = Console.ReadLine();
                            string[] ms = new string[6];
                            ms = line_with_credent.Split(';');
                            try
                            {
                                if (credent.creKey + 1 != Convert.ToInt32(ms[0]))
                                {
                                    Console.WriteLine("некорректный ключ!");
                                    Console.ReadKey();
                                    Console.Clear();
                                    continue;
                                }
                                credentials_list.Add(new Credentials()
                                {
                                    creKey = Convert.ToInt32(ms[0]),
                                    Surname = ms[1],
                                    Number = ms[2],
                                    Address = ms[3],
                                    IsGarage = Convert.ToBoolean(Convert.ToInt32(ms[4])),
                                    YearRegistration = Convert.ToInt32(ms[5]),
                                    Mileage = Convert.ToInt32(ms[6])
                                });
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("некорректный ввод!");
                                Console.ReadKey();
                                Console.Clear();
                                continue;
                            }
                            break;
                        }
                        while (true) //заносим состояние машины
                        {
                            Console.WriteLine();
                            Console.WriteLine();
                            var condition = condition_list.Last();
                            Console.WriteLine("Введите состояние автомобиля в формате: «Ключ;Статус;Дата техосмотра;Владелец (владение);».");
                            Console.WriteLine($"Не используйте лишних пробелов, Свойство «Владелец (владение)» предусматривает личное пользование или по найму. Введите данные с ключом {condition.conditionKey + 1}.");
                            Console.WriteLine("Пример ввода: '5;в угоне;17.03.2022;личное'.");
                            var line_with_condition = Console.ReadLine();
                            string[] ms = new string[6];
                            ms = line_with_condition.Split(';');
                            try
                            {
                                if (condition.conditionKey + 1 != Convert.ToInt32(ms[0]))
                                {
                                    Console.WriteLine("некорректный ключ!");
                                    Console.ReadKey();
                                    Console.Clear();
                                    continue;
                                }
                                condition_list.Add(new Condition()
                                {
                                    conditionKey = Convert.ToInt32(ms[0]),
                                    State = ms[1],
                                    Date = Convert.ToDateTime(ms[2]),
                                    owner = ms[3]
                                });
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("некорректный ввод!");
                                Console.ReadKey();
                                Console.Clear();
                                continue;
                            }
                            break;
                        }
                        Console.WriteLine("Запрос окончен, данные успешно добавлены, нажмите любую клавишу.");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    case 9: //Удаление автомобиля
                        Console.Clear();
                        Console.Write("Введите ключ машины, которую хотите удалить: ");
                        int result;
                        string key = Console.ReadLine();
                        try
                        {
                            result = Int32.Parse(key);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine($"Некорректное значение '{key}'");
                            Console.ReadKey();
                            Console.Clear();
                            continue;
                        }
                        if (result > car_list.Count() || result <= 0)
                        {
                            Console.WriteLine($"Машины с таким индексом нету");
                            Console.ReadKey();
                            Console.Clear();
                            continue;
                        }
                        var car = car_list.Where(x => x.carKey == result).FirstOrDefault(); //получаем объект который нужно удалить
                        car_list.Remove(car);
                        var con = condition_list.Where(x => x.conditionKey == result).FirstOrDefault();
                        condition_list.Remove(con);
                        var credential = credentials_list.Where(x => x.creKey == result).FirstOrDefault();
                        credentials_list.Remove(credential);
                        Console.WriteLine("Автомобиль и все данные о нём успешно удалены.");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    case 10:
                        Console.WriteLine("Выход из программы - да, нет - любая клавиша ");
                        string st = Console.ReadLine();
                        if (st == "да") Environment.Exit(0);
                        Console.Clear();
                        continue;
                    default:
                        Console.Clear();
                        Console.WriteLine("Введенный № отсутствует, нажмите любую клавишу");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                }
            }
        }
        static void File_Load(StreamReader sr, int n, int m)
        {
            string str;
            switch (n)
            {
                case 0:
                    while ((str = sr.ReadLine()) != null) // Пока не конец файла
                    {
                        string[] ms = new string[m];        // m - количество полей в классе
                        ms = str.Split(';');                    // Расщепление на массив строк
                        car_list.Add(new Car()
                        {
                            carKey = Convert.ToInt32(ms[0]),
                            Firm = ms[1],
                            Model = ms[2],
                            Type = ms[3],
                            Consumption = Convert.ToInt32(ms[4]),
                            Cost = Convert.ToInt32(ms[5])
                        });
                    }
                    break;
                case 1:
                    while ((str = sr.ReadLine()) != null) // Пока не конец файла
                    {
                        string[] ms = new string[m];        // m - количество полей в классе
                        ms = str.Split(';');                    // Расщепление на массив строк
                        condition_list.Add(new Condition()
                        {
                            conditionKey = Convert.ToInt32(ms[0]),
                            State = ms[1],
                            Date = Convert.ToDateTime(ms[2]),
                            owner = ms[3]
                        });
                    }
                    break;
                case 2:
                    while ((str = sr.ReadLine()) != null) // Пока не конец файла
                    {
                        string[] ms = new string[m];        // m - количество полей в классе
                        ms = str.Split(';');                    // Расщепление на массив строк
                        credentials_list.Add(new Credentials()
                        {
                            creKey = Convert.ToInt32(ms[0]),
                            Surname = ms[1],
                            Number = ms[2],
                            Address = ms[3],
                            IsGarage = Convert.ToBoolean(Convert.ToInt32(ms[4])),
                            YearRegistration = Convert.ToInt32(ms[5]),
                            Mileage = Convert.ToInt32(ms[6])
                        });
                    }
                    break;
            }
        }
        // Метод для выбора списка запросов
        static void Frame()
        {
            Console.WriteLine("\tВыберите № задачи\n");
            Console.WriteLine("1 - Вывести список машин, их данных, и состояний\n");
            Console.WriteLine("2 - Какие автомобили зарегистрированы по указанному адресу\n");
            Console.WriteLine("3 - Сколько автомобилей, и каких моделей числятся в угоне.\n");
            Console.WriteLine("4 - Владелец самого дорогого автомобиля\n");
            Console.WriteLine("5 - Какие автомобили чаще всего ремонтируются\n");
            Console.WriteLine("6 - Cоотношение между автомобилями в личной собственности и эксплуатируемых по найму\n");
            Console.WriteLine("7 - Расход бензина для заданной модели и владельца\n");
            Console.WriteLine("8 - Добавление автомобиля\n");
            Console.WriteLine("9 - Удаление автомобиля\n");
            //Console.WriteLine("\t Другие запросы");
            Console.WriteLine("\n10 - Выход из программы");
        }

        static void ShowNotes()
        {
            Console.Clear();
            Console.WriteLine("\t\tВСЕ МАШИНЫ:");
            Console.WriteLine($"{"Ключ:",-9}{"Фирма:",-13}{"Модель:",-11}{"Тип кузова:",-16}{"Расход топлива:",-18}{"Цена:",-10}");
            foreach (var item in car_list)
            {
                Console.WriteLine($"{item.carKey,-9}{item.Firm,-13}{item.Model,-11}{item.Type,-16}{item.Consumption,-18}{item.Cost,-10}");
            }

            Console.WriteLine();
            Console.WriteLine("\t\tУЧЕТНЫЕ ДАННЫЕ");
            Console.WriteLine($"{"Ключ:",-7}{"Владелец (Фамилия):",-20}{"Номерной знак:",-16}{"Адресс:",-19}{"Наличие гаража:",-17}{"Год регистрации:",-19}{"Пробег:",-5}");
            foreach (var item in credentials_list)
            {
                Console.WriteLine($"{item.creKey,-7}{item.Surname,-20}{item.Number,-16}{item.Address,-19}{item.IsGarage,-17}{item.YearRegistration,-19}{item.Mileage,-5}");
            }

            Console.WriteLine();
            Console.WriteLine("\t\tСОСТОЯНИЕ МАШИНЫ");
            Console.WriteLine($"{"Ключ:",-7}{"Статус:",-15}{"Дата техосмотра:",-19}{"Владелец (владение):",-6}");
            foreach (var item in condition_list)
            {
                Console.WriteLine($"{item.conditionKey,-7}{item.State,-15}{item.Date.ToShortDateString(),-19}{item.owner,-6}");
            }
        }

        static void SearchCarByAddres(string addres)
        {
            Console.Clear();
            var lst_of_keys = credentials_list.Where(x => x.Address == addres).ToList();
            if (lst_of_keys.Count == 0)
            {
                Console.WriteLine("По данному адресу не зарегистрировано машин.");
                return;
            }
            var lst_of_cars = car_list.Join(lst_of_keys,
                                                 x => x.carKey,
                                                 y => y.creKey,
                                                 (x, y) => new
                                                 {
                                                     Firm = x.Firm,
                                                     Model = x.Model
                                                 });
            Console.WriteLine($"По адресу '{addres}' зарегистрованна(ны): ");
            foreach (var item in lst_of_cars)
            {
                Console.WriteLine($"Машинa - {item.Model} фирмы - {item.Firm};");
            }
        }
        //Filter
        static void StolenCars()
        {
            Console.Clear();

            //ФИЛЬТРАЦИЯ
            var lst_of_keys = condition_list.Where(x => x.State == "в угоне");

            Console.WriteLine($"В угоне {lst_of_keys.Count()} машин(ы)(а)");
            Console.WriteLine();
            if (lst_of_keys.Count() == 0)
                return;
            //НА основе имеющихся ключей, составляю список с моделями машин
            var lst_of_cars = car_list.Join(lst_of_keys,
                                                 x => x.carKey,
                                                 y => y.conditionKey,
                                                 (x, y) => new
                                                 {
                                                     Model = x.Model,
                                                     Condition = y.State  
                                                 });
            foreach (var item in lst_of_cars)
            {
                Console.WriteLine($"Модель - {item.Model}");
                Console.WriteLine($"Состояние - {item.Condition}");
                Console.WriteLine();
            }
        }
        //
        static void OwnerMostExpensiveCar()
        {
            Console.Clear();
            if (car_list.Count() == 0)
            {
                Console.WriteLine("Нет ни одной машины.");
                return;
            }

            var key = car_list.Find(c => c.Cost == car_list.Max(x => x.Cost)).carKey;
            var owner = credentials_list.Where(x => x.creKey == key).First().Surname; //берем.First() чтобы возвращало объект класса а не список(все равно он будет 1)
            Console.WriteLine("Владелец самой дорогой машины: " + owner);
        }

        static void MostRepairedCars()
        {
            //список ключей с машинами в ремонте
            var lst_of_keys = condition_list.Where(x => x.State == "в ремонте").ToList();
            if (lst_of_keys.Count() == 0)
            {
                Console.WriteLine("Нет машин в ремонте");
                return;
            }
            var lst = car_list.Join(lst_of_keys,
                                    x => x.carKey,
                                    y => y.conditionKey,
                                    (a, b) => new
                                    {
                                        Model = a.Model,
                                        Firm = a.Firm,
                                    });
            //(from v in lst select v.Firm)
            var result = lst.Select(x=>x.Firm).GroupBy(g => g).OrderByDescending(o => o.Count()).FirstOrDefault().Key;
            Console.WriteLine($"Машина которая чаще всего ремонтируется - {result}");
        }
        //Соотношение между автомобилями в личной собственности и по найму
        static void OperationsStatistic()
        {
            var count_hiring = condition_list.Count(x => x.owner == "по найму");
            var count_personal = condition_list.Count(x => x.owner == "личное");
            var sum = count_hiring + count_personal;

            Console.WriteLine($"отношение владения 'по найму' к 'личное' относится как : {count_hiring * 100 / sum}% к {count_personal * 100 / sum}% соответсвенно");
        }

        //Расход бензина для заданной машины и владельца
        static void PetrolConsumption(string surname, string model)
        {
            Console.Clear();
            var owner = credentials_list.Where(x => x.Surname == surname);// список объектов с нужной фамилией
            if (owner.Count() == 0)
            {
                Console.WriteLine($"Владельца с фамилией '{surname}' не содержится!");
                return;
            }
            var car = car_list.Where(x => x.Model == model);//список объектов с нужной моделью
            if (car.Count() == 0)
            {
                Console.WriteLine($"Машин с моделью '{model}' не содержится!");
                return;
            }

            var lst_of_petrol_consumption = car.Join(owner,
                                                     x => x.carKey,
                                                     y => y.creKey,
                                                     (a, c) => new
                                                     {
                                                         Surname = c.Surname,
                                                         Model = a.Model,
                                                         Consumption = a.Consumption
                                                     });
            foreach (var item in lst_of_petrol_consumption)
            {
                Console.WriteLine($"Расход топлива для владельца {item.Surname} c моделью машины {item.Model} равен {item.Consumption} литр/км.");
            }
        }
    }
}