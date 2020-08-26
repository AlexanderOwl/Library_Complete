using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace Library
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.Unicode;
            List<Book> books;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome to Console Library V.1 \nInput help to list of command \n");
            Console.ResetColor();

            books = !File.Exists("lib.xml") ? new List<Book>() : Menu.DeserializeFromXML(); // Если существует файл читает из него, если файла нет создает новый список книг.
            Menu menu = new Menu(); //Экземпляр класса меню.
            if (!File.Exists("lib.xml"))
            {
                menu.Add5(books);
            }
            menu.Open(books);// Вызов метода Open класса меню.
        }
    }
}