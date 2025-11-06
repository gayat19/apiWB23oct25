using FirstAPI.Contexts;
using FirstAPI.Interfaces;
using FirstAPI.Models;
using FirstAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace FirstAPI.Services
{
    public class UserService : IUserService
    {
        private readonly ShoppingContext28Oct25 _context;
        private readonly ITokenService _tokenService;

        public UserService(ShoppingContext28Oct25 context,ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }
        public async Task<bool> Register(CustomerRegisterRequest customer)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var newCustomer = new Customer
                {
                    Name = customer.Name,
                    Age = customer.Age
                };
                _context.Customers.Add(newCustomer);
                await _context.SaveChangesAsync();
                HMACSHA512 hmac = new HMACSHA512();
                var user = new User
                {
                    Username = customer.Username,
                    Password = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(customer.Username + customer.Age)),
                    HashKey = hmac.Key,
                    CustomerId = newCustomer.Id,
                    Role = "Customer"

                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return true;

            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw e;
            }
            return false;

        }

        public async Task<UserLoginResponse> ValidateCredentials(UserLoginRequest user)
        {
            var result = _context.SpLoginReturn
                        .FromSqlRaw("exec proc_Login {0}", user.Username)
                        .AsEnumerable()
                        .FirstOrDefault();
            if (result == null)
                throw new Exception("Invalid username or password");
            byte[] password = null;
            HMACSHA512 hmac = new HMACSHA512(result.HashKey);
            password = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
            for (int i = 0; i < password.Length; i++)
            {
                if(password[i] != result.Password[i])
                    throw new Exception("Invalid username or password");
            }
            var userResponse = new UserLoginResponse
            {
                Username = user.Username,
                Role = result.Role ?? "Customer",
            };
            userResponse.Token = _tokenService.CreateToken(userResponse);
            return userResponse;


        }
    }
}
