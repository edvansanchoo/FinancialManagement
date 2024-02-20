using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialManagement.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public decimal FinancialIncoming { get; set; }
        public DateTime CreateDate { get; set; }
        public ICollection<Expenses>? Expenses { get; set; }
    }
}