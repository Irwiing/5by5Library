using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryFive
{
    class Book
    {
        private static FileHandler BookFile = new FileHandler { FileName = "LIVRO.csv" };
        public long TumbleNumber { get; private set; }
        public String ISBN { get; set; }
        public String Title { get; set; }
        public String Genre { get; set; }
        public DateTime PublishDate { get; set; }
        public String Author { get; set; }

        public Book() => TumbleNumber = NewID();                      
        private long NewID()
        {
            string lastBook = BookFile.GetLastLine();
            string[] bookAtributes = lastBook.Split(';');

            if(long.TryParse(bookAtributes[0], out long newID))
            {
                return newID+1;
            }
            return 1;
        }
        public String ToCSV()
        {
            return $"{TumbleNumber};{ISBN};{Title};{Genre};{PublishDate.ToString("dd/MM/yyyy")};{Author}";
        }
        static private List<Book> GetBookList()
        {
            List<Book> bookList = new List<Book>();
            BookFile.CheckFile();
            String[] bookContent = BookFile.ReadFile();
            Boolean header = true;
            foreach (var book in bookContent)
            {
                if (!header)
                {
                    String[] bookAtributes = book.Split(';');
                    bookList.Add(new Book
                    {
                        TumbleNumber = long.Parse(bookAtributes[0]),
                        ISBN = bookAtributes[1],
                        Title = bookAtributes[2],
                        Genre = bookAtributes[3],
                        PublishDate = DateTime.ParseExact(bookAtributes[4], "d", new CultureInfo(name: "pt-BT")),
                        Author = bookAtributes[5]
                    });
                }
                header = false;
            }
            return bookList;
        }
        static private String[] SetBookList(List<Book> bookList)
        {
            StringBuilder bookSB = new StringBuilder();
            bookSB.AppendLine("NumeroTombo;ISBN;Titulo;Genero;DataPublicacao;Autor");
            bookList.ForEach(book =>
            {
                bookSB.AppendLine(book.ToCSV());
            });

            return bookSB.ToString().Split('\n');
        }
        static public Boolean CheckISBN(String isbn)
        {
            List<Book> bookList = GetBookList();
            if (bookList.Exists(book => book.ISBN.Equals(isbn)))
                return true;
            return false;
        }
        static public Book GetBook(long tumbleNumber)
        {
            List<Book> bookList = GetBookList();
            return bookList.Find(book => book.TumbleNumber.Equals(tumbleNumber));
        }
        static public void CreateBook(Book newBook)
        {
            List<Book> bookList = GetBookList();
            bookList.Add(newBook);
            BookFile.CheckFile();
            BookFile.WriteFile(SetBookList(bookList));
        }
    }
}
