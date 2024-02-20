using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Context;
using FinancialManagement.Models;
using FinancialManagement.Models.Records;
using FinancialManagement.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FinancialManagement.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly TokenService _tokenService;
        private readonly UserService _userService;

        public LoginController(ILogger<LoginController> logger, UserService userService, TokenService tokenService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _userService = userService;
        }

        [EnableCors("Policy")]
        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] LoginRecord loginRecord)
        {
            var autenticated = await _userService.GetByUserNameAndPassWordAsync(loginRecord.Username, loginRecord.Password);
            if (autenticated == null)
            {
                return Unauthorized("Usuário ou senha inválidos");
            }
            return Ok(_tokenService.Generate(autenticated));
        }
    }
}