﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryFive
{
    class Loan
    {
        private static FileHandler LoanFile = new FileHandler { FileName = "EMPRESTIMO.csv" };
        public long IdCustomer { get; set; }
        public long TumbleNumber { get; set; }
        public DateTime LoanDate { get; private set; }
        public DateTime ReturnDate { get; set; }
        public int LoanStatus { get; set; }

        public Loan()
        {
            LoanDate = DateTime.Now;
            LoanStatus = 1;
        }
        public String ToCSV()
        {
            return $"{IdCustomer};{TumbleNumber};{LoanDate.ToString("dd/MM/yyyy")};{ReturnDate.ToString("dd/MM/yyyy")};{LoanStatus}";
        }
        static private List<Loan> GetLoanList()
        {
            List<Loan> loanList = new List<Loan>();
            LoanFile.CheckFile();
            String[] loanContent = LoanFile.ReadFile();
            Boolean header = true;
            foreach (var loan in loanContent)
            {
                if (!header)
                {
                    String[] loanAtributes = loan.Split(';');
                    loanList.Add(new Loan
                    {
                        IdCustomer = long.Parse(loanAtributes[0]),
                        TumbleNumber = long.Parse(loanAtributes[1]),
                        LoanDate = DateTime.ParseExact(loanAtributes[2], "d", new CultureInfo(name: "pt-BT")),
                        ReturnDate = DateTime.ParseExact(loanAtributes[3], "d", new CultureInfo(name: "pt-BT")),
                        LoanStatus = int.Parse(loanAtributes[4])
                    });
                }
                header = false;
            }
            return loanList;
        }

        static private String[] SetLoanList(List<Loan> loanList)
        {
            StringBuilder loanSB = new StringBuilder();
            loanSB.AppendLine("IdCliente;NumeroTombo;DataEmprestimo;DataDevolucao;StatusEmprestimo");
            loanList.ForEach(loan =>
            {
                loanSB.AppendLine(loan.ToCSV());
            });

            return loanSB.ToString().Split('\n');
        }
        static public Loan GetLoan(long tumbleNumber)
        {
            List<Loan> loanList = GetLoanList();
            return loanList.Find(loan => loan.TumbleNumber.Equals(tumbleNumber) && loan.LoanStatus.Equals(1));
        }
        static public void CreateLoan(Loan newLoan)
        {
            List<Loan> loanList = GetLoanList();
            loanList.Add(newLoan);
            LoanFile.CheckFile();
            LoanFile.WriteFile(SetLoanList(loanList));
        }
        static public void UpdateLoan(Loan loanUpdate)
        {
            List<Loan> loanList = GetLoanList();
            loanUpdate = loanList.Find(loan => loan.IdCustomer.Equals(loanUpdate.IdCustomer) && loan.TumbleNumber.Equals(loanUpdate.TumbleNumber));
            loanUpdate.LoanStatus = 2;
            LoanFile.WriteFile(SetLoanList(loanList)) ;
            
        }
    }
}
