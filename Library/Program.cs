using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace Library
{
    class Program
    {
        static List<Book> _books;
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.Unicode;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome to Console Library V.1 \nInput help to list of command \n");
            Console.ResetColor();

            _books = !File.Exists("lib.xml") ? new List<Book>() : Menu.DeserializeFromXML(); // Если существует файл читает из него, если файла нет создает новый список книг.
            Menu menu = new Menu(); //Экземпляр класса меню.
            if (!File.Exists("lib.xml"))
            {
                menu.Add5(_books);
            }
            menu.Open(_books);// Вызов метода Open класса меню.
        }
    }
}