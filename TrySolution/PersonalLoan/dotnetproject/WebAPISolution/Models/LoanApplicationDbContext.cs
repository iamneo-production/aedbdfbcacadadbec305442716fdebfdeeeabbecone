using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookStoreDBFirst.Models;

public class LoanApplicationDbContext : DbContext
{

    public LoanApplicationDbContext(DbContextOptions<LoanApplicationDbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<LoanApplication> LoanApplications { get; set; }
    public virtual DbSet<Option> Options { get; set; }
}
