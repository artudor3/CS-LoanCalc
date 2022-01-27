namespace LoanCalc.Models
{
    public class Loan
    {
        public double LoanAmount { get; set; }
        public int LoanTerm { get; set; }
        public double LoanRate { get; set; }


        public string Payment { get; set; } = "";
        public string TotalPrincipal { get; set; } = "";
        public string TotInt { get; set; } = "";
        public string TotalCost { get; set; } = "";


        public List<string> Principal { get; set; } = new();
        public List<string> Interest { get; set; } = new();
        public List<string> TotalInterest { get; set; } = new();
        public List<string> Balance { get; set; } = new();


        public bool Data = false;
    }
}
