using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Pedidos_web.Data;
using Pedidos_web.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Pedidos_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UsuarioLoginDto loginDto)
        {
            // Buscar usuario por email
            var usuario = _context.Usuario.FirstOrDefault(u => u.Email == loginDto.Email);
            if (usuario == null)
            {
                return Unauthorized("Credenciales inválidas.");
            }

            // Calcular hash de la contraseña ingresada
            string hashIngresado = GenerarHash(loginDto.Contraseña);

            // Comparar hashes
            if (usuario.Contraseña != hashIngresado)
            {
                return Unauthorized("Credenciales inválidas.");
            }

            // Crear token
            var claims = new[]
            {
        new Claim(ClaimTypes.Name, usuario.Email),
        new Claim("Id", usuario.Id.ToString())
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "PedidosAPI",
                audience: "PedidosAPI",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { token = tokenString });
        }

        private string GenerarHash(string input)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }

    }
}
