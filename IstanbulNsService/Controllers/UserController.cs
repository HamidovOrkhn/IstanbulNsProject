using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CryptoHelper;
using IstanbulNs.Data;
using IstanbulNs.Models;
using IstanbulNs.Repositories;
using IstanbulNs.Repositories.Enums;
using IstanbulNs.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace IstanbulNs.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IndexContext _db;
        private readonly IConfiguration _configuration;
        public UserController
        (
            IndexContext db,
            IConfiguration config
        )
        {
            _db = db;
            _configuration = config;

        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User request)
        {
            #region FunctionBody
            bool existEmail = _db.Users.Any(a => a.Email == request.Email);
            if (existEmail)
            {
                return StatusCode(400, new ReturnErrorMessage((int)ErrorTypes.Errors.AlreadyExists, message: "This Email is exists"));
            }
            string hashedPassword = Crypto.HashPassword(request.Password);
            string token = Guid.NewGuid().ToString();
            _db.Users.Add(new User
            {
                Role = request.Role,
                Email = request.Email,
                Name = request.Name,
                Password = hashedPassword,
                SexId = request.SexId,
                Token = token
            });
            _db.SaveChanges();
            return StatusCode(200, new ReturnMessage(message: "User Created", data: new { Token = token, Password = hashedPassword }));
            #endregion
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin request)
        {
            #region FunctionBody
            bool existEmail = _db.Users.Any(a => a.Email == request.Email && a.IsDisabled == 0);
            if (!existEmail)
            {
                return StatusCode(400, new ReturnErrorMessage((int)ErrorTypes.Errors.WrongUser, message: "This User is not exists"));
            }
            var user = _db.Users.FirstOrDefault(a => a.Email == request.Email);
            if (!Crypto.VerifyHashedPassword(user.Password, request.Password))
            {
                return StatusCode(400, new ReturnErrorMessage((int)ErrorTypes.Errors.WrongPassword, message: "Wrong Password"));
            }
            #region Jwt created and Refresh token updated
            var claim = new[] { 
                new Claim(ClaimTypes.Name, user.Name),
                new Claim("Role",user.Role.ToString())
            
            };
            var symmetric = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signing = new SigningCredentials(symmetric, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(10),
                claims: claim,
                signingCredentials: signing
                );

            user.Token = Guid.NewGuid().ToString();
            _db.Users.Update(user);
            _db.SaveChanges();
            user.Password = null;
            #endregion
            return Ok(new ReturnMessage(
                data: new
                {
                    jwtToken = new JwtSecurityTokenHandler().WriteToken(token),
                    userData = user
                }
                ));
            #endregion
        }     
        [HttpGet("refresh/{refreshToken}")]
        public object RefreshToken(string refreshToken)
        {
            #region FunctionBody
            User user = _db.Users.FirstOrDefault(a => a.Token == refreshToken && a.IsDisabled == 0);
            if (user == null)
            {
                return BadRequest();
            }
            var claim = new[] { new Claim(ClaimTypes.Name, user.Name) };
            var symmetric = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signing = new SigningCredentials(symmetric, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(10),
                claims: claim,
                signingCredentials: signing
                );
            //user.Token = Guid.NewGuid().ToString();

            //_db.SaveChanges();
            return new { token = user.Token, jwtToken = new JwtSecurityTokenHandler().WriteToken(token) };
            #endregion
        }
        [HttpGet("getuser/{Id}")]
        public IActionResult GetUser(int Id)
        {
            #region FunctionBody
            var userData = _db.Users.Where(a=>a.Id==Id).FirstOrDefault();
            return Ok(new ReturnMessage(userData));
            #endregion
        }
        [HttpGet("disable/{id}")]
        public IActionResult Disable(int id)
        {
            var user = _db.Users.Where(a => a.Id == id).FirstOrDefault();
            user.IsDisabled = 1;
            _db.SaveChanges();
            return Ok(new ReturnMessage());
        }
        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var user = _db.Users.Where(a => a.Id == id).FirstOrDefault();
            if (user !=null)
            {
                _db.Users.Remove(user);
            }
            _db.SaveChanges();
            return Ok(new ReturnMessage());
        }
        [HttpGet("enable/{id}")]
        public IActionResult Enable(int id)
        {
            var user = _db.Users.Where(a => a.Id == id).FirstOrDefault();
            user.IsDisabled = 0;
            _db.SaveChanges();
            return Ok(new ReturnMessage());
        }
        [HttpPost("find/email")]
        public IActionResult FindByEmail([FromBody]UserLogin request)
        {
            var user = _db.Users.Where(a => a.Email == request.Email && a.IsDisabled == 0).FirstOrDefault();
            if (user is null)
            {
                return NotFound(new ReturnErrorMessage(errortype: (int)ErrorTypes.Errors.WrongUser));
            }
            else
            {
                return Ok(new ReturnMessage(data: new { Token = user.Token, Password = "" }));
            }
        }
        [HttpGet("identity/{token}")]
        public IActionResult IdenTifyToken(string token)
        {
            var user = _db.Users.Where(a => a.Token == token).FirstOrDefault();
            if (user is null)
            {
                return NotFound(new ReturnErrorMessage(errortype: (int)ErrorTypes.Errors.WrongUser));
            }
            else
            {
                return Ok(new ReturnMessage());
            }
        }
        [HttpPost("change/forgot/{token}")]
        public IActionResult ChangePasswordForgot([FromBody]UserLogin request,string token)
        {
            var user = _db.Users.Where(a => a.Token == token).FirstOrDefault();
            if (user is null)
            {
                return NotFound(new ReturnErrorMessage(errortype: (int)ErrorTypes.Errors.WrongUser));
            }
            else
            {
                user.Password = Crypto.HashPassword(request.Password);
                user.Token = Guid.NewGuid().ToString();
                _db.SaveChanges();
                return Ok(new ReturnMessage());
            }
        }
        [HttpPost("change/password/{id}")]
        public IActionResult ChangePasswordForgot([FromBody] ChangePassword request, int id)
        {
            var user = _db.Users.Where(a => a.Id == id).FirstOrDefault();
            if (user is null)
            {
                return NotFound(new ReturnErrorMessage(errortype: (int)ErrorTypes.Errors.WrongUser));
            }
            else if (Crypto.VerifyHashedPassword(user.Password, request.OldPassword))
            {
                return NotFound(new ReturnErrorMessage(errortype: (int)ErrorTypes.Errors.WrongPassword));
            }
            else
            {
                user.Password = Crypto.HashPassword(request.Password);
                _db.SaveChanges();
                return Ok(new ReturnMessage());
            }
        }
        [HttpGet("all")]
        public IActionResult Users()
        {
            List<User> users = _db.Users.ToList();
            return Ok(new ReturnMessage(users));
        }

    }
}
