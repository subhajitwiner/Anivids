using api.DataConfig;
using api.Dtos;
using api.Interfaces;
using api.Migrations;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace api.Controllers
{

    
    public class UserController: BaseApiController
    {
        DataContext _context;
        private readonly ITokenService _tokenService;
        public UserController(DataContext context, ITokenService tokenService) { 
            _context = context;
            _tokenService = tokenService;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(CreateUserDto input)
        {
            if (await UserExists(input.UserName)) return BadRequest("UserName Taken");
            if (await EmailExists(input.Email)) return BadRequest("Email Taken");
            using var hmac = new HMACSHA512();
            CreateUserDto createUserDto = new CreateUserDto();
            UserModel user =  new UserModel
            { 
                FirstName = input.FirstName,
                LastName = input.LastName,
                Email = input.Email,
                Address = input.Address,
                Phone = input.Phone,
                UserName = input.UserName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(input.Password)),
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            
            return new UserDto {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Address = user.Address,
                Phone = user.Phone,
                UserName = user.UserName,
                varified = user.varified,
                Token = _tokenService.CreateToken(user)
            };
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.UserName);
            if (user == null) return Unauthorized();
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash =hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for (int i =0;i< computedHash.Length;i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }

            return new UserDto
            {
                Id  = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Address = user.Address,
                Phone = user.Phone,
                UserName = user.UserName,
                varified = user.varified,
                Token   = _tokenService.CreateToken(user)
            };
        }

         private async Task<bool> UserExists(string UserName)
        {
            return await _context.Users.AnyAsync(x =>x.UserName == UserName.ToLower());
        }
        private async Task<bool> EmailExists(string Email)
        {
            return await _context.Users.AnyAsync(x =>x.Email == Email.ToLower());
        }

    }
}
