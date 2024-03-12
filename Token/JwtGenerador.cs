using Microsoft.IdentityModel.Tokens;
using NetKubernetes.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace NetKubernetes.Token
{
	public class JwtGenerador : IJwtGenerador
	{
		public string CrearToken(Usuario usuario)
		{
			var claims = new List<Claim> {
			 new(JwtRegisteredClaimNames.NameId, usuario.UserName!),
			 new("userId", usuario.Id),
			 new("email", usuario.Email!)
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Mi palabra secreta"));
			var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
			var tokenDescripcion = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddDays(30),
				SigningCredentials = credenciales
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescripcion);
			return tokenHandler.WriteToken(token);

		}
	}
}
