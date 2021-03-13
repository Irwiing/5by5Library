using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryFive
{
    class CustomerAddress
    {
        public String City { get; set; }
        public String State { get; set; }
        public String ZipCode { get; set; }
        public String Neighborhood { get; set; }
        public String Address { get; set; }

        public String ToCSV()
        {
            return $"{Address};{Neighborhood};{City};{State};{ZipCode}";
        }
    }
}
