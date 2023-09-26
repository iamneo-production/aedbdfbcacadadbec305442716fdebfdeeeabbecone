using System;
using System.Collections.Generic;

namespace BookStoreApp.Models;
public class Option
{
    public int OptionID { get; set; }

    public string? LoanType { get; set; }

    public string? Description { get; set; }

    public decimal? InterestRate { get; set; }

    public decimal? MaximumAmount  { get; set; }

}