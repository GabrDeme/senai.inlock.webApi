using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using senai.inlock.webApi_.Domains;
using senai.inlock.webApi_.Interfaces;
using senai.inlock.webApi_.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace senai.inlock.webApi_.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository;
        public UsuarioController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        [HttpPost]
        public IActionResult Login(UsuarioDomain usuario)
        {
            try
            {
                UsuarioDomain usuarioBuscar = _usuarioRepository.Login(usuario.Email, usuario.Senha);

                if (usuarioBuscar == null)
                {
                    return NotFound("Email ou Senha Inválidos!");
                }
                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Jti,usuarioBuscar.IdUsuario.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,usuarioBuscar.Email),
                new Claim(ClaimTypes.Role, usuario.IdTipoUsuario.ToString())
            };
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("inlock-chave-autenticado-webapi-dev"));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken
                    (
                        issuer: "senai.inlock.webApi",
                        audience: "senai.inlock.webApi",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: creds

                    );
                return Ok(new
                {

                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });

            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

    }
}