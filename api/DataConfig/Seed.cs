using api.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace api.DataConfig
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            if (await context.Users.AnyAsync()) {
                return;
            }
            else
            {
                string userData = await File.ReadAllTextAsync("DataConfig/Seeds/UserSeedData.json");
                JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                List<UserModel> users = JsonSerializer.Deserialize<List<UserModel>>(userData, options);

                if(users == null || users.Count == 0) return;
                foreach (var user in users)
                {
                    using var hmac = new HMACSHA512();
                    user.UserName = user.UserName.ToLower();
                    user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Password"));
                    user.PasswordSalt = hmac.Key;
                    context.Users.Add(user);
                }
                await context.SaveChangesAsync();

            }
        }
    }
}
