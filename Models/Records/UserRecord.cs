using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialManagement.Models.Records
{
    public record UserRecord(string? Name, string? Username, string? Password, string? Email, decimal FinancialIncoming);

}