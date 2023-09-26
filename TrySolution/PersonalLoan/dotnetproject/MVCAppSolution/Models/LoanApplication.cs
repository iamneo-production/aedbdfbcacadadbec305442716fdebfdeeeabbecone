using System;
using System.Collections.Generic;

namespace BookStoreApp.Models;
public class LoanApplication
{
    public int LoanApplicationID { get; set; }
    public string Name { get; set; }
    public decimal RequestedAmount  { get; set; }
    public DateTime SubmissionDate { get; set; }
    public string? LoanType { get; set; }
    public string EmploymentStatus  { get; set; }
    public decimal Income  { get; set; }
    public int CreditScore   { get; set; }

}
