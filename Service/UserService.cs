using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Context;
using FinancialManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialManagement.Service
{
    public class UserService
    {
        private readonly FinancialContext _context;

        public UserService(FinancialContext context)
        {
            _context = context;
        }
        
        public async Task<User?> GetByUserNameAndPassWordAsync(string username, string password)
        {
            var user = await _context.Users
                .Where(u => u.Username == username)
                .SingleOrDefaultAsync();

            if (user != null && PasswordService.VerifyPassword(password, user.Password))
            {
                return user;
            }

            return null;
        }
    }
}