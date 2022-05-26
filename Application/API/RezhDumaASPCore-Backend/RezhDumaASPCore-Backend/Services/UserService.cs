using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RezhDumaASPCore_Backend.Model;
using RezhDumaASPCore_Backend.Model.Authentication;
using RezhDumaASPCore_Backend.Options;
using RezhDumaASPCore_Backend.Repositories;

namespace RezhDumaASPCore_Backend.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        User GetById(string id);
    }

    public class UserService : IUserService
    {
        private readonly AppSettings appSettings;
        private readonly IRepository<User> repository;
        private readonly IPasswordHasher<User> passwordHasher;

        public UserService(IOptions<AppSettings> appSettings, UserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            this.appSettings = appSettings.Value;
            repository = userRepository;
            this.passwordHasher = passwordHasher;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = repository.GetAll().Result.SingleOrDefault(u => u.Email == model.Username && CheckPassword(model, u));
            if (user == null) return null;
            var token = GenerateJwtToken(user);
            return new AuthenticateResponse(user, token);
        }

        public User GetById(string id)
        {
            return repository.GetAll().Result.FirstOrDefault(x => x.Id == id);
        }

        private bool CheckPassword(AuthenticateRequest authenticateRequest, User user)
        {
            var verification = passwordHasher.VerifyHashedPassword(user, user.Password, authenticateRequest.Password);
            return verification == PasswordVerificationResult.Success;
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
