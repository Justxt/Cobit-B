using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cobit.Models;
using Cobit_.Data;
using Cobit.Deto;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cobit_.Controllers
{
    public class PeopleController : Controller
    {
        private readonly Cobit_Context _context;
        private readonly IConfiguration _configuration;

        public PeopleController(Cobit_Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Método para registrar un usuario
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] PersonDto personDto)
        {

            if (_context.Person.Any(u => u.Cedula == personDto.Cedula))
            {
                return BadRequest("Cedula ya registrada.");
            }

            if (_context.Person.Any(u => u.Email == personDto.Email))
            {
                return BadRequest("El correo ya esta registrado.");
            }

            if (personDto.Password != personDto.ConfirmPassword)
            {
                return BadRequest("Las contraseñas no coinciden");
            }

            // si no hay usuarios registrados, el primer usuario registrado será admin
            var role = _context.Person.Any() ? "user" : "admin";

            try
            {
                var user = new Person
                {
                    Cedula = personDto.Cedula,
                    Name = personDto.Name,
                    LastName = personDto.LastName,
                    Email = personDto.Email,
                    Phone = personDto.Phone,
                    Password = personDto.Password,
                    Role = role
                };

                _context.Person.Add(user);
                await _context.SaveChangesAsync();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // Método para loguear un usuario
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {

            var person = await _context.Person.SingleOrDefaultAsync(u => u.Cedula == loginDto.Cedula && u.Password == loginDto.Password);

            if (person == null)
            {
                return BadRequest("Cedula o contraseña incorrecta!");
            }

            var token = GenerateJwtToken(person);
            return Ok(new { token, person });
        }


        // Método para generar el token JWT cuando el usuario se loguea
        private string GenerateJwtToken(Person person)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, person.PersonID.ToString()),
                    new Claim(ClaimTypes.Name, person.Name),
                    new Claim(ClaimTypes.Role, person.Role), // Añadir rol al token
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Tiempo de expiración del token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
