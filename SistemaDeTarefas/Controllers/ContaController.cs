using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SistemaDeTarefas.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SistemaDeTarefas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login([FromBody] LoginModel login)
        {
            if (login.Login == "admin" && login.Senha == "admin")
            {
                var token = GerarTokenJwt();
                return Ok(new { token });
            }

            return BadRequest(new { mensagem = "Credenciais inválidas. Verifique seu nome de usuário e senha." });
        }

        private string GerarTokenJwt()
        {
            string chaveSecreta = "3b7ce3cd-9da3-4b21-92e0-8471ad607335";
            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta));

            var credencial = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("login", "admin"),
                new Claim("nome", "Administrador do Sistema"),
            };

            var token = new JwtSecurityToken(
                issuer: "sua_empresa",
                audience: "sua_aplicacao",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credencial
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
