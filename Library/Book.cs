namespace Library
{
    public class Book // Класс Книга параметрами (Автор, Имя, Издательство, Год, Владелец, Счетчик дней использования)
    {
        public string Author;
        public string Name;
        public string Publisher;
        public int Year;
        public string Owner;
        public int NumOfDays;


        public Book(string author,string name,string publisher, int year, string owner, int numOfdays)
        {
            this.Author = author;
            this.Name = name;
            this.Publisher = publisher;
            this.Year = year;
            this.Owner = owner;
            this.NumOfDays  = numOfdays;
        }
        public Book()
        {

        }
    }
}
