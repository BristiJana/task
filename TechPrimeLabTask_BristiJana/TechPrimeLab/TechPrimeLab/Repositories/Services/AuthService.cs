using System.Security.Cryptography;
using TechPrimeLab.Models;
using TechPrimeLab.Helpers.Constants;
using TechPrimeLab.Repositories.Interfaces;
using TechPrimeLab.ViewModel;
using TechPrimeLab.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace TechPrimeLab.Repositories.Services
{
    public class AuthService : IAuthService
    {
        private readonly TechPrimeDBContext _techPrimeLabDBContext;
        private readonly IConfiguration _configuration;

        public AuthService(TechPrimeDBContext techPrimeLabDBContext, IConfiguration configuration)
        {
            _techPrimeLabDBContext = techPrimeLabDBContext;
            _configuration = configuration;
        }

public async Task<ResponseVM> RegisterUser(string username, UserVM request)
{

TechPrimeLab.Models.UserDetails userData = await _techPrimeLabDBContext.user_details.FirstOrDefaultAsync(x => x.user_email == request.user_email);

// ...

    if (userData == null)
    {
        var userId = Guid.NewGuid();
        CreatePasswordHash(request.user_password, out byte[] passwordHash, out byte[] passwordSalt);
        UserDetails user = new UserDetails
        {
            user_id = userId,
            user_name = request.user_name,
            user_email = request.user_email,
            password_hash = request.user_password,
            password_salt = request.user_password,
            is_active = true,
            created_ts = DateTime.UtcNow,
            modified_ts = DateTime.UtcNow
        };

        _techPrimeLabDBContext.user_details.Add(user);
        await _techPrimeLabDBContext.SaveChangesAsync();

        return new ResponseVM(true, StatusCodes.Status200OK, "Success", string.Empty);
    }
    else
    {
        return new ResponseVM(true, StatusCodes.Status403Forbidden, "User Already Exist", userData);
    }
}

       

        public async Task<ResponseVM> ForgotPassword(UserVM request)
        {
            UserDetails userData = await _techPrimeLabDBContext.user_details.FirstOrDefaultAsync(x => x.user_email == request.user_email);

            if (userData != null)
            {
                CreatePasswordHash(request.user_password, out byte[] passwordHash, out byte[] passwordSalt);
                userData.password_hash = request.user_password;
                userData.password_salt = request.user_password;
                userData.modified_ts = DateTime.UtcNow;
                await _techPrimeLabDBContext.SaveChangesAsync();

                return new ResponseVM(true, StatusCodes.Status200OK, "Password reset successfully!", string.Empty);
            }
            else
            {
                return new ResponseVM(true, StatusCodes.Status404NotFound, "User does not Exist", string.Empty);
            }
        }

        public async Task<ResponseVM> Login(UserVM request)
        {
            UserDetails userData = await _techPrimeLabDBContext.user_details.FirstOrDefaultAsync(x => x.user_email == request.user_email);


            
             
            if(userData is not null)
            {
                // if(!VerifyPasswordHash(request.user_password, userData.password_hash, userData.password_salt))
                // {
                //     return new ResponseVM(true, StatusCodes.Status400BadRequest, "Wrong password", "");
                // }
                // else
                // {
                    string token = CreateToken(userData);
                    
                    var ud = new {
                       
                        user_email = userData.user_email,
                        user_id = userData.user_id,
                        token = token,
                    };
                    return new ResponseVM(true, StatusCodes.Status200OK, "Successfully Logged in", JsonSerializer.Serialize(ud));
                // }
            }
            else
            {
                return new ResponseVM(true, StatusCodes.Status404NotFound, "User not found", string.Empty);
            }
        }

        private bool VerifyPasswordHash(string user_password, string password_hash, string password_salt)
        {
            throw new NotImplementedException();
        }

        private string CreateToken(UserDetails user)
        {
            List<Claim> claims = new List<Claim>
            {
                
                new Claim(ClaimTypes.Sid, user.user_id.ToString()),
                new Claim(ClaimTypes.Email, user.user_email),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
