using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryFive
{
    class Customer
    {
        private static FileHandler CustomerFile = new FileHandler { FileName = "CLIENTE.csv" };
        public long IdCustomer { get; private set; }
        public String CPF { get; set; }
        public String Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String Phone { get; set; }
        public CustomerAddress Address { get; set; }

        public Customer() => IdCustomer = NewID();
        private long NewID()
        {
            string lastCustomer = CustomerFile.GetLastLine();
            string[] customerAtributes = lastCustomer.Split(';');

            if (long.TryParse(customerAtributes[0], out long newID))
            {
                return newID + 1;
            }
            return 1;
        }
        public String ToCSV()
        {
            return $"{IdCustomer};{CPF};{Name};{DateOfBirth.ToString("dd/MM/yyyy")};{Phone};{Address.ToCSV()}";
        }
        static private List<Customer> GetCustomerList()
        {
            List<Customer> customerList = new List<Customer>();
            CustomerFile.CheckFile();
            String[] customerContent = CustomerFile.ReadFile();
            Boolean header = true;
            foreach (var customer in customerContent)
            {
                if (!header)
                {
                    String[] customerAtributes = customer.Split(';');
                    customerList.Add(new Customer
                    {
                        IdCustomer = long.Parse(customerAtributes[0]),
                        CPF = customerAtributes[1],
                        Name = customerAtributes[2],
                        DateOfBirth = DateTime.ParseExact(customerAtributes[3], "d", new CultureInfo(name: "pt-BT")),
                        Phone = customerAtributes[4],
                        Address = new CustomerAddress
                        {
                            Address = customerAtributes[5],
                            Neighborhood = customerAtributes[6],
                            City = customerAtributes[7],
                            State = customerAtributes[8],
                            ZipCode = customerAtributes[9],
                        }
                    });
                }
                header = false;
            }
            return customerList;
        }

        static private String[] SetCustomerList(List<Customer> customerList)
        {
            StringBuilder customerSB = new StringBuilder();
            customerSB.AppendLine("IdCliente;CPF;Nome;DataNascimento;Telefone;Logradouro;Bairro;Cidade;Estado;CEP");
            customerList.ForEach(customer =>
            {               
                customerSB.AppendLine(customer.ToCSV());
            });

            return customerSB.ToString().Split('\n');
        }
        static public Boolean CheckCPF(String cpf)
        {
            List<Customer> customerList = GetCustomerList();
            if (customerList.Exists(customer => customer.CPF.Equals(cpf)))
                return true;
            return false;
        }
        static public Customer GetCustomer(String cpf)
        {
            List<Customer> customerList = GetCustomerList();
            return customerList.Find(customer => customer.CPF.Equals(cpf));
        }
        static public void CreateCustomer(Customer newCustomer)
        {
            List<Customer> customerList = GetCustomerList();
            customerList.Add(newCustomer);
            CustomerFile.CheckFile();
            CustomerFile.WriteFile(SetCustomerList(customerList));
        }

    }

}
