using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryFive
{
    class Customer
    {
        public long IdCustomer { get; private set; }
        public String CPF { get; set; }
        public String Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String Phone { get; set; }
        public CustomerAddress Address { get; set; }

        public Customer() => IdCustomer = DateTime.Now.Ticks;
        public String ToCSV()
        {
            return $"{IdCustomer};{CPF};{Name};{DateOfBirth.ToString("dd/MM/yyyy")};{Phone};{Address.ToCSV()}";
        }

        static public Boolean CheckCPF(String cpf)
        {
            List<Customer> customerList = new List<Customer>();
            if (customerList.Exists(customer => customer.CPF.Equals(cpf)))
                return true;
            return false;
        }

        static public void CreateCustomer(Customer newCustomer)
        {
            List<Customer> customerList = new List<Customer>();
            customerList.Add(newCustomer);
        }
    }

}
