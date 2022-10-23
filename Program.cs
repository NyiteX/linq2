using System.Collections.Generic;
using System.Data.SqlTypes;

Kniga K = new();

char vvod;
do
{
    Console.Clear();
    Console.WriteLine("1.Добавление\n2.Поиск\n3.Добавление работников\n4.Вывод на экран всей базы");
    Console.WriteLine("Esc - Выход\n");
    vvod = Console.ReadKey().KeyChar;
    switch (vvod)
    {
        case '1':
            Console.Clear();
            K.Add();
            Console.WriteLine("Press any key to continue.\n");
            Console.ReadKey();
            break;
        case '2':
            if (K.GetCount() > 0)
            {
                char vvod2;
                do
                {
                    Console.Clear();
                    Console.WriteLine("1.Поиск по названию фирмы\n2.Поиск по профилю бизнеса\n3.Поиск по фамилии директора\n4.Поиск по кол-ву сотрудников");
                    Console.WriteLine("5.Поиск по адресу\n6.Поиск по дате основания.");
                    Console.WriteLine("Esc - Выход\n");
                    vvod2 = Console.ReadKey().KeyChar;
                    switch (vvod2)
                    {
                        case '1':
                            Console.Clear();
                            Console.WriteLine("Название фирмы: ");
                            string? str = Console.ReadLine();
                            K.PrintAll(K.SortBySlogan(str));
                            Console.WriteLine("Press any key to continue.\n");
                            Console.ReadKey();
                            break;
                        case '2':
                            Console.Clear();
                            Console.WriteLine("Профиль фирмы: ");
                            string? str2 = Console.ReadLine();
                            K.PrintAll(K.SortByProfile(str2));
                            Console.WriteLine("Press any key to continue.\n");
                            Console.ReadKey();
                            break;
                        case '3':
                            Console.Clear();
                            Console.WriteLine("Фамилия директора: ");
                            string? str3 = Console.ReadLine();
                            K.PrintAll(K.SortBySecondName(str3));
                            Console.WriteLine("Press any key to continue.\n");
                            Console.ReadKey();
                            break;
                        case '4':
                            Console.Clear();
                            Console.WriteLine("Кол-во сотрудников(диапазон): ");
                            string? str4 = "";
                            string? str4_2 = "";
                            while (!Prover(str4))
                            {
                                Console.Write("от: ");
                                str4 = Console.ReadLine();
                                if (!Prover(str4)) Console.WriteLine("Кривой ввод");
                            }
                            while (!Prover(str4_2))
                            {
                                Console.Write("до: ");
                                str4_2 = Console.ReadLine();
                                if (!Prover(str4_2)) Console.WriteLine("Кривой ввод");
                            }
                            K.PrintAll(K.SortByKolSotr(str4, str4_2));
                            Console.WriteLine("Press any key to continue.\n");
                            Console.ReadKey();
                            break;
                        case '5':
                            Console.Clear();
                            Console.WriteLine("Адрес фирмы: ");
                            string? str5 = Console.ReadLine();
                            K.PrintAll(K.SortByAdres(str5));
                            Console.WriteLine("Press any key to continue.\n");
                            Console.ReadKey();
                            break;
                    }
                } while (vvod2 != 27);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Список пуст.");
                Console.WriteLine("Press any key to continue.\n");
                Console.ReadKey();
            }
            break;
        case '3':
            if (K.GetCount() > 0)
            {
                Console.Clear();
                Console.WriteLine("Название фирмы: ");
                string? str = Console.ReadLine();
                K.AddWorkers(K.SortBySlogan(str));
                Console.WriteLine("Press any key to continue.\n");
                Console.ReadKey();
                break;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Список пуст.");
                Console.WriteLine("Press any key to continue.\n");
                Console.ReadKey();
            }
            break;
        case '4':
            Console.Clear();
            K.PrintAll(K.Sort());
            Console.WriteLine("Press any key to continue.\n");
            Console.ReadKey();
            break;

    }
} while (vvod != 27);

bool Prover(string str)
{
    if (str.Length > 0)
    {
        for (int i = 0; i < str.Length; i++)
            if (!Char.IsDigit(str[i]))
                return false;
        return true;
    }
    else return false;
}
static class ExtensionCompany
{
    public static bool Prover(string str)
    {
        if (str.Length > 0)
        {
            for (int i = 0; i < str.Length; i++)
                if (!Char.IsDigit(str[i]))
                    return false;
            return true;
        }
        else return false;
    }
    public static void AddTelef(this Company p, string name)
    {
        foreach (var p2 in p.Workers)
        {
            Console.Write("Телефон: ");
            string? telef = Console.ReadLine();
            p2.telef.Add(telef);
            break;
        }
    }
    public static void AddWorkers(this Kniga k, IOrderedEnumerable<Company> pe)
    {
        int money = 0;
        string? tmp = "";
        foreach (var p in pe)
        {
            p.Workers[p.Workers.Length - 1] = new();
            Console.Write("Имя сотрудника: ");
            p.Workers[p.Workers.Length - 1].Name = Console.ReadLine();
            Console.Write("Фамилия сотрудника: ");
            p.Workers[p.Workers.Length - 1].Secondname = Console.ReadLine();
            Console.Write("Отчество сотрудника: ");
            p.Workers[p.Workers.Length - 1].Otchestvo = Console.ReadLine();
            while (!Prover(tmp))
            {
                Console.Write("Зарплата: ");
                tmp = Console.ReadLine();
                if (!Prover(tmp)) Console.WriteLine("Кривой ввод.");
                else money = Convert.ToInt32(tmp);
            }
            Console.Write("Email: ");
            p.Workers[p.Workers.Length - 1].Email = Console.ReadLine();
            Console.Write("Должность: ");
            p.Workers[p.Workers.Length - 1].Dolzhnostb = Console.ReadLine();
            p.Workers[p.Workers.Length - 1].Money = money;
            Console.Write("Телефон: ");
            p.Workers[p.Workers.Length - 1].telef.Add(Console.ReadLine());

            Array.Resize(ref p.Workers, p.Workers.Length + 1);
        }
    }
}
class Kniga
{
    Company[] Companys = new Company[0];
    public Kniga()
    {
        Companys = new Company[0];
    }
    public bool Prover(string str)
    {
        if (str.Length > 0)
        {
            for (int i = 0; i < str.Length; i++)
                if (!Char.IsDigit(str[i]))
                    return false;
            return true;
        }
        else return false;
    }
    public Company[] getCompanys() { return Companys; }
    public void Add()
    {
        int date = 0, kol_sotrudnikov = 0;
        string? name = "", bussines = "", adres = "";

        Console.Write("Имя фирмы: ");
        name = Console.ReadLine();
        Console.Write("Профиль бизнесса: ");
        bussines = Console.ReadLine();
        string? tmp = "";
        while (!Prover(tmp))
        {
            Console.Write("Год основания: ");
            tmp = Console.ReadLine();
            if (!Prover(tmp)) Console.WriteLine("Кривой ввод.");
            else date = Convert.ToInt32(tmp);
        }
        tmp = "";
        while (!Prover(tmp))
        {
            Console.Write("Кол-во сотрудников: ");
            tmp = Console.ReadLine();
            if (!Prover(tmp)) Console.WriteLine("Кривой ввод.");
            else kol_sotrudnikov = Convert.ToInt32(tmp);
        }
        Console.Write("Адрес фирмы: ");
        adres = Console.ReadLine();

        Array.Resize(ref Companys, Companys.Length + 1);
        Companys[Companys.Length - 1] = new();
        Console.Write("Имя директора: ");
        Companys[Companys.Length - 1].I = Console.ReadLine();
        Console.Write("Фамилия директора: ");
        Companys[Companys.Length - 1].F = Console.ReadLine();
        Console.Write("Отчество директора: ");
        Companys[Companys.Length - 1].O = Console.ReadLine();
        Companys[Companys.Length - 1].Name = name;
        Companys[Companys.Length - 1].Bussines = bussines;
        Companys[Companys.Length - 1].Year = date;
        Companys[Companys.Length - 1].Kol_workers = kol_sotrudnikov;
        Companys[Companys.Length - 1].Adres = adres;
        Companys[Companys.Length - 1].Workers = new Person[kol_sotrudnikov];
    }
    void Print(Company p)
    {
        Console.WriteLine("Имя фирмы: " + p.Name + "\nГод основания: " + p.Year + "\nПрофиль бизнеса: " + p.Bussines + "\nКол-во сотрудников: " + p.Kol_workers + "\nАдрес: " + p.Adres);
        Console.WriteLine("ФИО директора фирмы.\nИмя: " + p.I + "\nФамилия: " + p.F + "\nОтчество: " + p.O);
        //foreach (var p2 in p.Workers)
        //    Console.Write(p2.telef + " ");
        Console.WriteLine('\n');
    }
    public IOrderedEnumerable<Company> Sort()
    {
        var sortedPeople1 = from p in Companys
                            orderby p.Name
                            select p;
        return sortedPeople1;
    }
    public int GetCount() { return Companys.Length; }
    public IOrderedEnumerable<Company> SortByProfile(string str)
    {
        str = str.ToUpper();
        var sortedPeople1 = from p in Companys
                            where p.Bussines.ToUpper().Contains(str)
                            orderby p.Name
                            select p;
        return sortedPeople1;
    }
    public IOrderedEnumerable<Company> SortBySecondName(string str)
    {
        str = str.ToUpper();
        var sortedPeople1 = from p in Companys
                            where p.F.ToUpper() == str
                            orderby p.Name
                            select p;
        return sortedPeople1;
    }
    public IOrderedEnumerable<Company> SortByAdres(string str)
    {
        var sortedPeople1 = from p in Companys
                            where p.Adres.Contains(str)
                            orderby p.Name
                            select p;
        return sortedPeople1;
    }
    public IOrderedEnumerable<Company> SortByKolSotr(string str, string str2)
    {
        if (int.Parse(str) > int.Parse(str2))
        {
            string tmp = str2;
            str2 = str;
            str = tmp;
        }
        var sortedPeople1 = from p in Companys
                            where p.Kol_workers >= int.Parse(str) && p.Kol_workers <= int.Parse(str2)
                            orderby p.Name
                            select p;
        return sortedPeople1;
    }
    public IOrderedEnumerable<Company> SortBySlogan(string str)
    {
        str = str.ToUpper();
        var sortedPeople1 = from p in Companys
                            where p.Name.ToUpper().Contains(str)
                            orderby p.Name
                            select p;
        return sortedPeople1;
    }
    public void PrintAll(IOrderedEnumerable<Company> sortedpeople)
    {
        if (sortedpeople.Count() > 0)
            foreach (var p in sortedpeople)
                Print(p);
        else
            Console.WriteLine("Список пуст\n");
    }
}

class Company
{
    public Person[] Workers;
    public string? Name { get; set; }
    public string? Bussines { get; set; }
    public string? F { get; set; }
    public string? I { get; set; }
    public string? O { get; set; }
    public int Kol_workers { get; set; }
    public int Year { get; set; }
    public string? Adres { get; set; }
}
class Person
{
    public List<string?> telef;
    public string? Name { get; set; }
    public string? Secondname { get; set; }
    public string? Otchestvo { get; set; }
    public string? Dolzhnostb { get; set; }
    public string? Email { get; set; }
    public double Money { get; set; }
}