using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialManagement.Models.Records
{
    public record ExpensesRecord(string? Name, string? Description, decimal Amount, DateTime Date, int UserId);

}