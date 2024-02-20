using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Context;
using FinancialManagement.Models;
using FinancialManagement.Models.Records;
using FinancialManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FinancialManagement.Controllers
{
    [Route("api/v1/[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly FinancialContext _context;

        public UserController(ILogger<UserController> logger, FinancialContext context)
        {
            _logger = logger;
            _context = context;
        }

        [EnableCors("Policy")]
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToArrayAsync();
            foreach(var user in users)
            {
                user.Expenses = await _context.Expenses.Where(e => e.UserId == user.Id).ToListAsync();
            }
            return Ok(users);
        }

        [EnableCors("Policy")]
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetAllUserByIdAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            user.Expenses = await _context.Expenses.Where(e => e.UserId == user.Id).ToListAsync();
            return Ok(user);
        }

        [EnableCors("Policy")]
        //[Authorize]
        [HttpPost]
        public async Task<ActionResult<User>> CreateUserAsync([FromBody] UserRecord userRecord)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Username == userRecord.Username);
            if (userExists)
            {
                return BadRequest("Username already exists");
            }
            var user = new User
            {
                Name = userRecord.Name,
                Username = userRecord.Username,
                Password = PasswordService.HashPassword(userRecord.Password),
                Email = userRecord.Email,
                FinancialIncoming = userRecord.FinancialIncoming,
                CreateDate = DateTime.Now,
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        [EnableCors("Policy")]
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUserAsync(int id, [FromBody] UserRecord userRecord)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Username == userRecord.Username && u.Id != id);
            if (userExists)
            {
                return BadRequest("Username already exists");
            }

            var userToUpdate = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (userToUpdate == null)
            {
                return NotFound("User not found");
            }
            userToUpdate.Username = userRecord.Username;
            userToUpdate.Password = PasswordService.HashPassword(userRecord.Password);
            userToUpdate.Email = userRecord.Email;
            userToUpdate.FinancialIncoming = userRecord.FinancialIncoming;
            await _context.SaveChangesAsync();
            return Ok(userToUpdate);
        }

        [EnableCors("Policy")]
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}