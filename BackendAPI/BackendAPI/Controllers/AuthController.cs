using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using users.database;

namespace BackendAPI.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static DBUser user = new();
        private UsersContext _usersContext;
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration, UsersContext context)
        {
            _configuration = configuration;
            _usersContext = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<DBUser>> Register(RegisterRequestUser requestUser)
        {
            if ( requestUser.password != requestUser.controlPassword ) return Unauthorized("La contraseña no coincide.");

            var newUser = _usersContext.Users.Find(requestUser.email);

            if (newUser is null)
            {
                newUser = new DBUser();

                CreatePasswordHash(requestUser.password, out byte[] passwordHash, out byte[] passwordSalt); //Creates encrypted password

                newUser.email = requestUser.email;
                newUser.passwordHash = passwordHash;
                newUser.passwordSalt = passwordSalt;

                _usersContext.Add(newUser);
                await _usersContext.SaveChangesAsync();
                return Ok("El usuario ha sido creado");
            }

            return Unauthorized("El usuario ya existe");
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(User requestUser)
        {
            var newUser = _usersContext.Users.Find(requestUser.email);

            if (newUser is null) return NotFound("User not found.");

            if (!VerifyPasswordHash(requestUser.password, newUser.passwordHash, newUser.passwordSalt))
            {   return Unauthorized("Wrong password."); }

            string token = CreateToken(newUser);
            return Ok("Auth Token: " + token);

        }

        [HttpPost("changePassword/{email}")]
        public IResult ChangePassword(string email, string newPassword)
        {
            var user = _usersContext.Users.Find(email);

            if (user is null) return Results.NotFound("El usuario no existe");

            CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);

            user.passwordHash = passwordHash;
            user.passwordSalt = passwordSalt;

            _usersContext.Users.Update(user);
            _usersContext.SaveChanges();
            return Results.Ok(user);
        }

        [HttpGet("getByEmail/{email}")]
        public IResult Get(string email)
        {
            var newUser = _usersContext.Users.Find(email);

            if (newUser is null) return Results.NotFound();

            return Results.Ok(newUser);
        }

        [HttpDelete("softDelete/{email}")]
        public IResult SoftDelete(string email)
        {
            var toDeleteUser = _usersContext.Users.Find(email);

            if (toDeleteUser is null) return Results.NotFound();

            if (toDeleteUser.deleted == false)
            {
                toDeleteUser.deleted = true;
            }
            else
            {
                toDeleteUser.deleted = false;
            }
     
            
            _usersContext.Users.Update(toDeleteUser);
            _usersContext.SaveChanges();

            return Results.Ok(toDeleteUser);
        }

        [HttpDelete("hardDelete/{email}")]
        public IResult HardDelete(string email)
        {
            var toDeleteUser = _usersContext.Users.Find(email);

            if (toDeleteUser is null) return Results.NotFound();

            _usersContext.Users.Remove(toDeleteUser);
            _usersContext.SaveChanges();
            return Results.Ok(toDeleteUser.email + " fue eliminado");
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

        private string CreateToken(DBUser user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.email)
            };          

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
