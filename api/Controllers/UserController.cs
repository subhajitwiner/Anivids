using api.DataConfig;
using api.Dtos;
using api.Dtos.Brand;
using api.Interfaces;
using api.Interfaces.Repositories;
using api.Models;
using api.Reposetories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace api.Controllers
{

    
    public class UserController: BaseApiController
    {
        DataContext _context;
        IMapper _mapper;
        IUserRepository _repository;
        private readonly ITokenService _tokenService;
        public UserController(DataContext context, ITokenService tokenService, IUserRepository repository, IMapper mapper) { 
            _context = context;
            _tokenService = tokenService;
            _mapper = mapper;
            _repository = repository;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(CreateUserDto input)
        {
            if (await UserExists(input.UserName)) return BadRequest("UserName Taken");
            if (await EmailExists(input.Email)) return BadRequest("Email Taken");
            using var hmac = new HMACSHA512();
            CreateUserDto createUserDto = new CreateUserDto();
            UserModel user = new UserModel
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                Email = input.Email,
                Address = input.Address,
                Phone = input.Phone,
                City = input.City,
                Country = input.Country,
                varified = false,
                UserName = input.UserName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(input.Password)),
                PasswordSalt = hmac.Key,
                DateOfBirth = input.DateOfBirth,
                KnownAs = input.KnownAs,
                Created = DateTime.UtcNow,
                LastActive = DateTime.UtcNow,
                Gender = input.Gender,
                Interests = input.Interests,
                Introduction = input.Introduction,
                LookingFor = input.LookingFor,
            };
            
        _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
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
            return Ok();
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

        [HttpGet("GetUsers")]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            try
            {
                var Users = await _repository.GetUsersAsync();
                var userToReturn = _mapper.Map<IEnumerable<UserModel>>(Users);

                return Ok(userToReturn);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
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
