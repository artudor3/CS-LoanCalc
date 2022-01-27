using LoanCalc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LoanCalc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [HttpGet]
        public IActionResult LoanCalc()
        {
            Loan mLoan = new()
            {
                LoanAmount = 15000,
                LoanTerm = 60,
                LoanRate = 3
            };

            return View(mLoan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LoanCalc(Loan mLoan)
        {
            double payment = mLoan.LoanAmount * (mLoan.LoanRate / 1200) / (1 - Math.Pow(1 + mLoan.LoanRate / 1200, -Convert.ToDouble(mLoan.LoanTerm)));
            double balance = mLoan.LoanAmount;

            double interest = 0;
            double principal = 0;
            double totalInterest = 0;
            double totalPrincipal = 0;
            

            for (int i = 1; i <= mLoan.LoanTerm; i++)
            {
                interest = balance * (mLoan.LoanRate / 1200);
                principal = payment - interest;
                totalInterest += interest;
                totalPrincipal += principal;
                balance -= principal;

                mLoan.Payment = string.Format("{0:C}", payment);
                mLoan.Principal.Add(string.Format("{0:C}", principal));
                mLoan.Interest.Add(string.Format("{0:C}", interest));
                mLoan.TotalInterest.Add(string.Format("{0:C}", totalInterest));
                mLoan.Balance.Add(string.Format("{0:C}", balance));


            }
            mLoan.TotInt = string.Format("{0:C}", totalInterest);
            mLoan.TotalPrincipal = string.Format("{0:C}", totalPrincipal);
            mLoan.TotalCost = string.Format("{0:C}", totalPrincipal + totalInterest);
            mLoan.Data = true;
            return View(mLoan);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}