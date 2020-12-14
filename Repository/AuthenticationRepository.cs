using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Commander.DTOModels;
using Commander.Models;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace Commander.Repository
{
    public class AuthenticationRepository: IAuthenticateRepository
    {
        private readonly DataContext _context;
        private const string Key = "this is the token key";

        public AuthenticationRepository(DataContext context)
        {
            _context = context;
        }

        public UserModel GetUserById(Guid id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<UserModel> ListUsers()
        {
            var userModels = _context.Users.Where(u =>u.Role.Role == "User").Include(u => u.Role).ToList();
            Console.WriteLine(userModels.ToArray());
            return userModels;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
        
        public ResponseModelDTO Authenticate(UserModel user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.RoleId = _context.RoleModel.First(x => x.Role == "User").Id;
            var userFromDatabase = _context.Users.FirstOrDefault(x => x.Username == user.Username);
            if (userFromDatabase != null)
            {
               return Login(user, userFromDatabase, GenerateToken(user));
            }
            _context.Users.Add(user);
            return new ResponseModelDTO
            {
                Message = "User successfully created",
                Token = new JwtSecurityTokenHandler().WriteToken(GenerateToken(user))
            };
        }

        private static bool IsValidCredentials(UserModel user,UserModel userFromDatabase)
        {
            return user.Password == userFromDatabase.Password;
        }

        private static ResponseModelDTO Login(UserModel userToBeCreated, UserModel userFromDatabase, JwtSecurityToken token)
        {
            if (!IsValidCredentials(userToBeCreated,userFromDatabase))
            {
                return new ResponseModelDTO
                {
                    Message = "Invalid credentials",
                    Token = ""
                };
            }
            return new ResponseModelDTO
            {
                Message = "User already created",
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        private static JwtSecurityToken GenerateToken(UserModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimsIdentity.DefaultIssuer,"https://localhost:5002"),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString())
            };
            return new JwtSecurityToken(
                new JwtHeader(new SigningCredentials( new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key)), SecurityAlgorithms.HmacSha256Signature)),
                new JwtPayload(claims)
                );
        }
        
    }
}