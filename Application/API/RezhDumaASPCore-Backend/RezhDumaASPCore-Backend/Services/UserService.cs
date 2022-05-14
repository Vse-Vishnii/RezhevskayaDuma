using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
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

        public UserService(IOptions<AppSettings> appSettings, UserRepository userRepository)
        {
            this.appSettings = appSettings.Value;
            repository = userRepository;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = repository.GetAll().Result.SingleOrDefault(x => x.Email == model.Username && x.Password == model.Password);
            if (user == null) return null;
            var token = GenerateJwtToken(user);
            return new AuthenticateResponse(user, token);
        }

        public User GetById(string id)
        {
            return repository.GetAll().Result.FirstOrDefault(x => x.Id == id);
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
