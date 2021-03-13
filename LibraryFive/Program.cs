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
                           // CreateBookHandler();
                            break;
                        case 3:
                        //    CreateLoanHandler();
                            break;
                        case 4:
                        //CreateDevolutionHandler();
                            break;
                        case 5:
                        //    ReportHandler();
                            break;
                    }
                }
            }
        }

        static void CreateCustomerHandler()
        {
            Console.Clear();
            Console.Write("Informe o CPF do cliente: ");
            string cpf = Console.ReadLine();

            if (Customer.CheckCPF(cpf))
            {
                Console.WriteLine("Cliente já cadastrado");
                Console.WriteLine("\nPressione qualquer tecla para retornar ao menu principal.");
                Console.ReadKey();
                return;
            }

            Console.Write("Informe o nome do cliente: ");
            string name = Console.ReadLine();

            Console.Write("Informe a data de nascimento do cliente: ");
            string dateOfBirth = Console.ReadLine();

            Console.Write("Informe telefone do cliente: ");
            string phone = Console.ReadLine();

            Console.Write("Informe o endereço cliente: ");
            string address = Console.ReadLine();

            Console.Write("Informe o bairro do cliente: ");
            string neighborhood = Console.ReadLine();

            Console.Write("Informe a cidade do cliente: ");
            string city = Console.ReadLine();

            Console.Write("Informe o estado do cliente: ");
            string state = Console.ReadLine();

            Console.Write("Informe o CEP do cliente: ");
            string zipCode = Console.ReadLine();

            Customer.CreateCustomer(new Customer
            {
                CPF = cpf,
                Name = name,
                DateOfBirth = DateTime.ParseExact(dateOfBirth, "d", new CultureInfo(name: "pt-BR")),
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

        }
    }
}
