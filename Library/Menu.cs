using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Library
{
    class Menu // Меню и операции над книгами
    {
        public void Open(List<Book> books)
        {
            bool flag = true;
            while (flag)
            {
                switch (Console.ReadLine())
                {
                    case "help":
                        Console.WriteLine(" help - command list" +
                            "\n add - add new book" +
                            "\n print [order] [field] - print list of book by [ascending/descending] [name/author/publisher/year]" +
                            "\n delete - delete book" +
                            "\n save - save book" +
                            "\n stat - statistic about book" +
                            "\n load - load book from lib.xml" +
                            "\n take - take book for library" +
                            "\n return - return book to library");
                        break;

                    case "add":
                        Add(books);
                        break;


                    case "exit":
                        flag = false;
                        SerializeToXML(books);
                        break;

                    case "stat":
                        Stat(books);
                        break;

                    case "print":
                        Print(books);
                        break;

                    case "print descending name":
                        Print(books, "descending", "name");
                        break;

                    case "print ascending name":
                        Print(books, "ascending", "name");
                        break;
                    case "print descending author":
                        Print(books, "descending", "author");
                        break;

                    case "print ascending author":
                        Print(books, "ascending", "author");
                        break;

                    case "print descending publisher":
                        Print(books, "descending", "publisher");
                        break;

                    case "print ascending publisher":
                        Print(books, "ascending", "publisher");
                        break;
                    case "print descending year":
                        Print(books, "descending", "year");
                        break;

                    case "print ascending year":
                        Print(books, "ascending", "year");
                        break;

                    case "search":
                        Search(books);
                        break;

                    case "delete":
                        Console.WriteLine("Input name of book to delete");
                        Delete(Console.ReadLine(), books);
                        break;

                    case "save":
                        SerializeToXML(books);
                        break;

                    case "load":
                        books = DeserializeFromXML();
                        break;

                    case "take":
                        TakeBook(books);
                        break;

                    case "return":
                        ReturnBook(books);
                        break;

                    case "clear":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Welcome to console Library V.1 \nInput help to command list\n");
                        Console.ResetColor();
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Command does not exist");
                        Console.ResetColor();
                        break;
                }
            }
        }//Метод считывает команду пользователя и вызывает один из методов
        static void Delete(string name, List<Book> books)
        {
            bool flag = false;
            foreach (Book book in books.Where(b => b.Name == name))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Deleting...");
                Console.ResetColor();

                Console.WriteLine("Name: {0}, Author: {1}, Publisher: {2}, Year: {3}", book.Name, book.Author, book.Publisher, book.Year);

                if (books.Remove(book))
                {
                    flag = true;
                    Console.WriteLine("Success!");
                }
                break;
            }
            if (!flag)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There is no such book!");
                Console.ResetColor();
            }
        }// Удаляет книгу из библиотеки
        static void Print(List<Book> books)
        {
            int id = 1;
            foreach (Book book in books)
            {
                if (book.Owner == "Library")
                {
                    //Console.WriteLine("#id {0} Name: {1}, Author: {2}, Publisher: {3}, Year: {4}", id, book.Name, book.Author, book.Publisher, book.Year);
                    #region print                                      
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("#id ");
                    Console.ResetColor();
                    Console.Write(id + " ");

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("Name: ");
                    Console.ResetColor();
                    Console.Write(book.Name + " ");

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("Author: ");
                    Console.ResetColor();
                    Console.Write(book.Author + " ");

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("Publisher: ");
                    Console.ResetColor();
                    Console.Write(book.Publisher + " ");

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("Year: ");
                    Console.ResetColor();
                    Console.Write(book.Year + " ");
                    Console.WriteLine();
                    #endregion
                }
                id++;
            }
        }// Выводит список доступных книг
        static void Stat(List<Book> books) // Выводит статистику по книгам, подсвечивает серым книги которые недоступны
        {
            int id = 1;
            foreach (Book book in books)
            {
                if (book.Owner == "Library")
                {
                    Console.WriteLine("#id {0} Name: {1}, Author: {2}, Publisher: {3}, Available in library, Reads {4} " + (book.NumOfDays == 1 ? "day" : "days"), id, book.Name, book.Author, book.Publisher, book.NumOfDays);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("#id {0} Name: {1}, Author: {2}, Publisher: {3}, Reading: {4}, Reads {5} days", id, book.Name, book.Author, book.Publisher, book.Owner, book.NumOfDays);
                    Console.ResetColor();
                }
                id++;
            }
        }
        static void Print(List<Book> books, string orederBy, string field)
        {
            IOrderedEnumerable<Book> sortedBooks = null;
            if (orederBy == "ascending")
            {
                if (field == "name")
                {
                    sortedBooks = books.OrderBy(book => book.Name);
                }
                else if (field == "author")
                {
                    sortedBooks = books.OrderBy(book => book.Author);
                }
                else if (field == "publisher")
                {
                    sortedBooks = books.OrderBy(book => book.Publisher);
                }
                else if (field == "year")
                {
                    sortedBooks = books.OrderBy(book => book.Year);
                }
            }

            else if (orederBy == "descending")
            {
                if (field == "name")
                {
                    sortedBooks = books.OrderByDescending(book => book.Name);
                }
                else if (field == "author")
                {
                    sortedBooks = books.OrderByDescending(book => book.Author);
                }
                else if (field == "publisher")
                {
                    sortedBooks = books.OrderByDescending(book => book.Publisher);
                }
                else if (field == "year")
                {
                    sortedBooks = books.OrderByDescending(book => book.Year);
                }
            }
            int id = 1;
            foreach (Book book in sortedBooks)
            {
                if (book.Owner == "Library")
                {

                    //Console.WriteLine("Пустой поиск!");

                    //Console.Write("#id {0} Name: {1}, Author: {2}, Publisher: {3}", "Year: {4}", book.Name, book.Author, book.Publisher, book.Year, id);
                    #region print                                      
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("#id ");
                    Console.ResetColor();
                    Console.Write(id + " ");

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("Name: ");
                    Console.ResetColor();
                    Console.Write(book.Name + " ");

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("Author: ");
                    Console.ResetColor();
                    Console.Write(book.Author + " ");

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("Publisher: ");
                    Console.ResetColor();
                    Console.Write(book.Publisher + " ");

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("Year: ");
                    Console.ResetColor();
                    Console.Write(book.Year + " ");
                    Console.WriteLine();
                    #endregion
                }
                id++;
            }

        }//Печть в порядке спадания или возростания конкретного поля (можно выбрать)
        static void Search(List<Book> books)
        {
            bool find = false;
            Console.Write("Search: ");
            string readLine = Console.ReadLine();
            if (readLine != null)
            {
                int id = 1;
                foreach (Book book in books)
                {
                    if (book.Author.Contains(readLine) || book.Name.Contains(readLine) || book.Publisher.Contains(readLine))
                    {
                        if (book.Owner == "Library")
                        {
                            Console.WriteLine("#id {0} Name: {1}, Author: {2}, Publisher: {3}, Year: {4}", id, book.Name, book.Author, book.Publisher, book.Year);
                            find = true;
                        }
                    }
                    id++;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Empty search!");
                Console.ResetColor();
            }

            if (!find)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nothing found!");
                Console.ResetColor();
            }

        }//Поиск совпадений введенного текста пользователем в полях Name Author Publisher
        public void Add5(List<Book> books) 
        {          
            books.Add(new Book("Harper Lee","To Kill a Mockingbird","AST",1960, "Library", 0));
            books.Add(new Book("J.R.R. Tolkien","The Lord of the Rings","Ranok",1954, "Library", 0));
            books.Add(new Book("Jane Austen","Pride and Prejudice","ABABAGALAGAMA",1797, "Library", 0));
            books.Add(new Book("Markus Zusak","The Book Thief","Pechat` na kolenkakh",2004, "Library", 0));
            books.Add(new Book("Louisa May Alcott","Little Women","Ne Publisher",1868, "Library", 0));
        }//Добавляет новую книгу


        static void Add(List<Book> books) 
        {
            Console.WriteLine("To add book input\n" +
                                          "Аuthor, Name, Publisher, Year");
            string readLine = Console.ReadLine();
            if (readLine != null)
            {
                string[] temp = readLine.Split(',');
                if (temp.Length != 4)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Incorrect input!");
                    Console.ResetColor();
                    return;
                }
                for (int i = 0; i < temp.Length; i++)
                {
                    temp[i] = temp[i].Trim();
                }

                bool tryParseYear = int.TryParse(temp[3], out int year);
                if (!tryParseYear || year < 1 || year > DateTime.Now.Year)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Incorrect year!");
                    Console.ResetColor();
                    return;
                }

                foreach (Book book in books)
                {
                    if (book.Author.Contains(temp[0])
                        && book.Name.Contains(temp[1])
                        && book.Publisher.Contains(temp[2]))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("The book already exists!");
                        Console.ResetColor();
                        return;
                    }
                }
                books.Add(new Book(temp[0], temp[1], temp[2], year, "Library", 0));
            }
        }//Добавляет новую книгу


        static void TakeBook(List<Book> books)
        {
            Console.WriteLine("Input num of book that u want to take: ");
            Print(books);

            if (int.TryParse(Console.ReadLine(), out int choise) && choise > 0 && choise <= books.Count)
            {
                Console.Write("Input your name and surname: ");
                string name = Console.ReadLine();
                Console.Write("Indicate the number of days for which you want to take the book (no more than 30):");
                int days;
                if (int.TryParse(Console.ReadLine(), out days))
                {
                    if (days < 1 && days > 30)
                    {
                        Console.WriteLine("You cannot borrow a book for this period");
                    }
                    else
                    {
                        books[choise - 1].Owner = name;
                        books[choise - 1].NumOfDays += days;
                    }
                }
                else if (!int.TryParse(Console.ReadLine(), out days))
                {
                    Console.WriteLine("Invalid input");
                }

            }
            else
            {
                Console.WriteLine("Invalid input");
            }
        }//Возможность взять книгу из списка доступных
        static void ReturnBook(List<Book> books)
        {
            bool find = false;
            Console.Write("Input your name and surname: ");
            string name = Console.ReadLine();
            if (name != null)
            {
                int id = 1;
                foreach (Book book in books)
                {
                    if (book.Owner.Contains(name))
                    {
                        Console.WriteLine("#id {0} Name: {1}, Author: {2}, Publisher: {3}, Year: {4}", id, book.Name, book.Author, book.Publisher, book.Year);
                        find = true;
                    }
                    id++;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Empty input!");
                Console.ResetColor();
            }


            if (!find)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You have no books! Or you entered the wrong name!");
                Console.ResetColor();
            }
            else if (find)
            {
                Console.WriteLine("Enter the id of the book you want to check in: ");
                if (int.TryParse(Console.ReadLine(), out int choise))
                {
                    if (books[choise - 1].Owner == name)
                        books[choise - 1].Owner = "Library";

                    else
                    {
                        Console.WriteLine("You dont have this book");
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect input");
                }
            }

        }//Возвращает книгу в библиотеку
        public static void SerializeToXML(List<Book> lib)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Book>));
            TextWriter textWriter = new StreamWriter(@"lib.xml");
            serializer.Serialize(textWriter, lib);
            textWriter.Close();
        }//Сохраняет в файл
        public static List<Book> DeserializeFromXML()
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(List<Book>));
            TextReader textReader = new StreamReader(@"lib.xml");
            List<Book> lib = (List<Book>)deserializer.Deserialize(textReader);
            textReader.Close();

            return lib;
        }//Загружает книги из файла
    }
}
