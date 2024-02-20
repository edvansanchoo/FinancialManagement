using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Context;
using FinancialManagement.Models;
using FinancialManagement.Models.Records;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FinancialManagement.Controllers
{
    [Route("api/v1/[controller]")]
    public class ExpensesController : Controller
    {
        private readonly ILogger<ExpensesController> _logger;
        private readonly FinancialContext _context;

        public ExpensesController(ILogger<ExpensesController> logger, FinancialContext context)
        {
            _logger = logger;
            _context = context;
        }

        [EnableCors("Policy")]
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Expenses>>> GetAllExpensesAsync()
        {
            return Ok(await _context.Expenses.ToArrayAsync());
        }

        [EnableCors("Policy")]
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Expenses>> GetAllExpensesByIdAsync(int id)
        {
            var expenses = await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id);
            if (expenses == null)
            {
                return NotFound("Expenses not found");
            }
            return Ok(expenses);
        }

        [EnableCors("Policy")]
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Expenses>> CreateExpensesAsync([FromBody] Expenses expenses)
        {
            _context.Expenses.Add(expenses);
            await _context.SaveChangesAsync();
            return Ok(expenses);
        }

        [EnableCors("Policy")]
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Expenses>> UpdateExpensesAsync(int id, [FromBody] Expenses expenses)
        {
            var expensesToUpdate = await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id);
            if (expensesToUpdate == null)
            {
                return NotFound("Expenses not found");
            }

            expensesToUpdate.Description = expenses.Description;
            expensesToUpdate.Amount = expenses.Amount;
            expensesToUpdate.Date = expenses.Date;
            await _context.SaveChangesAsync();
            return Ok(expensesToUpdate);
        }

        [EnableCors("Policy")]
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteExpensesAsync(int id)
        {
            var expenses = await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id);
            if (expenses == null)
            {
                return NotFound("Expenses not found");
            }
            _context.Expenses.Remove(expenses);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}