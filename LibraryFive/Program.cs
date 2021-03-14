using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryFive
{
    class Program
    {
        private static CultureInfo BrDate = new CultureInfo(name: "pt-BR");
        static void Main(string[] args)
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Bem vindo(a) à Biblioteca da FIVE");
                Console.WriteLine("\n\t---------- MENU ----------");
                Console.WriteLine("\n1 - Cadastrar novo leitor" +
                    "\n2 - Cadastrar novo livro" +
                    "\n3 - Emprestimo de livro" +
                    "\n4 - Devolução de livro" +
                    "\n5 - Relatório de empréstimos e devoluções");
                Console.WriteLine("\n\t--------------------------");
                Console.Write("\nOpção: ");
                string op = Console.ReadLine();

                if (int.TryParse(op, out int iop))
                {
                    switch (iop)
                    {
                        case 1:
                            CreateCustomerHandler();
                            break;
                        case 2:
                            CreateBookHandler();
                            break;
                        case 3:
                            CreateLoanHandler();
                            break;
                        case 4:
                            CreateDevolutionHandler();
                            break;
                        case 5:
                            ReportHandler();
                            break;
                    }
                }
            }
        }

        static void CreateCustomerHandler()
        {
            Console.Clear();
            Console.Write("\tCADASTRO DE CLIENTE\n");
            Console.Write("Informe o CPF: ");
            string cpf = Console.ReadLine();

            if (Customer.CheckCPF(cpf))
            {
                Console.WriteLine("Cliente já cadastrado");
                Console.WriteLine("\nPressione qualquer tecla para retornar ao menu principal.");
                Console.ReadKey();
                return;
            }

            Console.Write("Informe o nome: ");
            string name = Console.ReadLine();

            Console.Write("Informe a data de nascimento (dd/MM/YYYY): ");
            string stringDateOfBirth = Console.ReadLine();
            if (!DateTime.TryParseExact(stringDateOfBirth, "d", BrDate, DateTimeStyles.None, out DateTime dateOfBirth))
            {
                Console.WriteLine("Data inválida!");
                Console.WriteLine("\nPressione qualquer tecla para retornar ao menu principal.");
                Console.ReadKey();
                return;
            }

            Console.Write("Informe telefone: ");
            string phone = Console.ReadLine();

            Console.Write("Informe o endereço: ");
            string address = Console.ReadLine();

            Console.Write("Informe o bairro: ");
            string neighborhood = Console.ReadLine();

            Console.Write("Informe a cidade: ");
            string city = Console.ReadLine();

            Console.Write("Informe o estado: ");
            string state = Console.ReadLine();

            Console.Write("Informe o CEP: ");
            string zipCode = Console.ReadLine();

            Customer.CreateCustomer(new Customer
            {
                CPF = cpf,
                Name = name,
                DateOfBirth = dateOfBirth,
                Phone = phone,
                Address = new CustomerAddress
                {
                    Address = address,
                    Neighborhood = neighborhood,
                    City = city,
                    State = state,
                    ZipCode = zipCode
                }
            });

            Console.Clear();
            Console.WriteLine("\nCliente Cadastrado com sucesso!\n");
            Console.WriteLine("\nPressione qualquer tecla para retornar ao menu principal.");
            Console.ReadKey();            
        }
        static void CreateBookHandler()
        {
            Console.Clear();
            Console.Write("\tCADASTRO DE LIVRO\n");
            Console.Write("Informe o ISBN: ");
            string isbn = Console.ReadLine();

            if (Book.CheckISBN(isbn))
            {
                Console.WriteLine("Livro já cadastrado");
                Console.WriteLine("\nPressione qualquer tecla para retornar ao menu principal.");
                Console.ReadKey();
                return;
            }

            Console.Write("Informe o titulo: ");
            string title = Console.ReadLine();

            Console.Write("Informe o genero: ");
            string genre = Console.ReadLine();

            Console.Write("Informe a data de publicação (dd/MM/YYYY): ");
            string stringPublishDate = Console.ReadLine();
            if (!DateTime.TryParseExact(stringPublishDate, "d", BrDate, DateTimeStyles.None, out DateTime publishDate))
            {
                Console.WriteLine("Data inválida!");
                Console.WriteLine("\nPressione qualquer tecla para retornar ao menu principal.");
                Console.ReadKey();
                return;
            }
            Console.Write("Informe o autor: ");
            string author = Console.ReadLine();

            Book newBook =new Book
            {
                ISBN = isbn,
                Title = title,
                Genre = genre,
                PublishDate = publishDate,
                Author = author
            };
            Book.CreateBook(newBook);
            Console.Clear();
            Console.WriteLine($"\nLivro Cadastrado com sucesso!\nNumero do tombo:{newBook.TumbleNumber}\n");
            Console.WriteLine("\nPressione qualquer tecla para retornar ao menu principal.");
            Console.ReadKey();
        }

        static void CreateLoanHandler()
        {
            Console.Clear();
            Console.Write("\tCADASTRO DE EMPRESTIMO\n");
            Console.Write("Informe o Numero do Tombo do livro: ");
            string stringTumbleNumber = Console.ReadLine();
            if (!long.TryParse(stringTumbleNumber, out long tumbleNumber))
            {
                Console.WriteLine("Numero de tombo inválido!");
                Console.WriteLine("\nPressione qualquer tecla para retornar ao menu principal.");
                Console.ReadKey();
                return;
            }
            Book book = Book.GetBook(tumbleNumber);
            if (book is null)
            {
                Console.WriteLine("Livro indisponível para empréstimo");
                Console.WriteLine("\nPressione qualquer tecla para retornar ao menu principal.");
                Console.ReadKey();
                return;
            }
            Loan loan = Loan.GetLoan(book.TumbleNumber);
            if(!(loan is null))
            {
                Console.WriteLine("Livro indisponível para empréstimo");
                Console.WriteLine("\nPressione qualquer tecla para retornar ao menu principal.");
                Console.ReadKey();
                return;
            }

            Console.Write("Informe o CPF do cliente: ");
            string cpf = Console.ReadLine();
            Customer customer = Customer.GetCustomer(cpf);
            if (customer is null)
            {
                Console.WriteLine("Cliente não cadastrado");
                Console.WriteLine("\nPressione qualquer tecla para retornar ao menu principal.");
                Console.ReadKey();
                return;
            }

            Console.Write("Informe a data de devolução (dd/MM/YYYY): ");
            string stringReturnDate = Console.ReadLine();
            if(!DateTime.TryParseExact(stringReturnDate, "d", BrDate, DateTimeStyles.None, out DateTime returnDate))
            {
                Console.WriteLine("Formato de data inválida!");
                Console.WriteLine("\nPressione qualquer tecla para retornar ao menu principal.");
                Console.ReadKey();
                return;
            }
            if (!Loan.CheckReturnDate(returnDate))
            {
                Console.WriteLine("a data deve ser maior que a data de hoje!");
                Console.WriteLine("\nPressione qualquer tecla para retornar ao menu principal.");
                Console.ReadKey();
                return;
            }

            Loan.CreateLoan(new Loan
            {
                IdCustomer = customer.IdCustomer,
                TumbleNumber = book.TumbleNumber,
                ReturnDate = returnDate
            });
            Console.Clear();
            Console.WriteLine("\nEmprestimo Cadastrado com sucesso!\n");
            Console.WriteLine("\nPressione qualquer tecla para retornar ao menu principal.");
            Console.ReadKey();
        }
        static void CreateDevolutionHandler()
        {
            Console.Clear();
            Console.Write("Informe o Numero do Tombo do livro: ");
            string stringTumbleNumber = Console.ReadLine();
            if (!long.TryParse(stringTumbleNumber, out long tumbleNumber))
            {
                Console.WriteLine("Numero de tombo inválido!");
                Console.WriteLine("\nPressione qualquer tecla para retornar ao menu principal.");
                Console.ReadKey();
                return;
            }
            Loan loan = Loan.GetLoan(tumbleNumber);
            if (loan is null)
            {
                Console.WriteLine("Livro não encontrado para devolução");
                Console.WriteLine("\nPressione qualquer tecla para retornar ao menu principal.");
                Console.ReadKey();
                return;
            }
            double fee = loan.CalculateFee();
            if (fee == 0)
                Console.WriteLine("Sem multa!");
            else
                Console.WriteLine($"Entrega com atraso, multa de {fee.ToString("C")}");
            Loan.UpdateLoan(loan);
            Console.ReadKey();
        }
        static void ReportHandler()
        {
            Console.Clear();
            Console.WriteLine("\tEmprestimos\n");
            Console.WriteLine("----------------------");
            List<Loan> loanList = Loan.GetLoanList();
            loanList.ForEach(loan =>
            {
                Customer customer = Customer.GetCustomer(loan.IdCustomer);
                Book book = Book.GetBook(loan.TumbleNumber);
                Console.WriteLine($"\nCPF:\t\t\t{customer.CPF}" +
                    $"\nNumero de Tombo:\t{book.TumbleNumber}" +
                    $"\nTitulo:\t\t\t{book.Title}" +
                    $"\nStatus do Emprestimo:\t{loan.PrintStatus()}" +
                    $"\nData de emprestimo:\t{loan.LoanDate}" +
                    $"\nData de devolução:\t{loan.ReturnDate}");
                Console.WriteLine("----------------------");
            });
            Console.ReadKey();
        }
    }
}
