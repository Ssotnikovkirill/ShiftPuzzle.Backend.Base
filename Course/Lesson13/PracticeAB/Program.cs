﻿namespace PracticeAB;
using System.Collections.Generic; // библиотека для работы с List и Dictionary
using System.Text.Json; // библиотека для работы с JSON

// объявление сериализуемого класса People
[System.Serializable] public class People
{
    // поля класса
    public int id { get; set; }
    public string Name { get; set; }
    public string email { get; set; }
    public bool isVerified { get; set; }

    public People() { } // Пустой конструктор для десериализации

    // конструктор класса
    public People(string a, int b, string c, bool d)
    {
        this.id=b;
        this.Name = a;
        this.email=c;
        this.isVerified=d;
    }
}
[System.Serializable] public class Check
{
    // поля класса
    public string orderId { get; set; }
    public string customerName { get; set; }
    public int totalPrice { get; set; }
    public List<string> items { get; set; }

    public Check() { } // Пустой конструктор для десериализации
    public Check(string a, string b, int c, List<string> d)
    {
        this.orderId=a;
        this.customerName = b;
        this.totalPrice=c;
        this.items=d;
    }
}
class Program
{
    static void Main(string[] args)
    {
        const string path = "1.json";
        string jsonFromFile = File.ReadAllText(path);
        People read_people = JsonSerializer.Deserialize<People>(jsonFromFile);
        if (read_people != null)
        {
            Console.WriteLine($"Айди: {read_people.id}\nПочта: {read_people.email}");
        }
        Console.WriteLine("-----------------");
        List<string> it = new List<string>{"Ноутбук", "Мышь", "салфетки для мониторв"};
        Check n = new Check("ORD10245", "Анна Петрова", 5600+1550, it); // создание объекта
        // Сериализация в JSON
        string json2 = JsonSerializer.Serialize(n);
        const string path2 = "2.json";
        string jsonFromFile2 = File.ReadAllText(path2);
        Check read_Check = JsonSerializer.Deserialize<Check>(jsonFromFile2);
        if (read_Check != null)
        {
            Console.WriteLine($"Скидка: {read_Check.totalPrice/10*9}");

            // вывод содержимого списка ссылок
            Console.WriteLine("Список покупок:");
            foreach (string el in read_Check.items)
            {
                Console.Write(el+", ");
            }
        }
        Console.WriteLine("\n-----------------");
        List<string> it4 = new List<string>{"Ноутбук", "Мышь", "салфетки для мониторв"};
        Check n4 = new Check("ORD10245", "Анна Петрова", 5600+1550, it4); // создание объекта
        // Сериализация в JSON
        string json4 = JsonSerializer.Serialize(n4);
        const string path4 = "4.json";
        File.WriteAllText(path4, json4); // запись объекта в JSON файл
        string jsonFromFile4 = File.ReadAllText(path4);
        Check read_Check4 = JsonSerializer.Deserialize<Check>(jsonFromFile4);
        if (read_Check4 != null)
        {
            Console.WriteLine($"Скидка: {(read_Check4.totalPrice/10*9)*98/100}");

            // вывод содержимого списка ссылок
            Console.WriteLine("Список покупок:");
            foreach (string el in read_Check4.items)
            {
                Console.Write(el+", ");
            }
        }
        Console.WriteLine("\n-----------------");
    }
}
