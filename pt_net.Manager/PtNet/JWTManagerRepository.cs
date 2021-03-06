using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using pt_net.Entity.EntityModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace pt_net.Manager.PtNet
{
	public class JWTManagerRepository : IJWTManagerRepository
	{
		Dictionary<string, string> UsersRecords = new Dictionary<string, string>
	{
		{ "user1","password1"},
		{ "user2","password2"},
		{ "user3","password3"},
	};

		private readonly IConfiguration iconfiguration;
		private readonly IUserService userService;

		public JWTManagerRepository(IConfiguration iconfiguration, IUserService _userService)
		{
			this.iconfiguration = iconfiguration;
			this.userService = _userService;
		}
		public Tokens Authenticate(User users)
		{
			/*
			 * No user exists with this USERNAME
			 */
			if (userService.user(users.username) == null)
            {
				return new Tokens { Token = "not_found"};
            }

			User userDetails = userService.authenticate(users);
			if (userDetails == null)
            {
				return null;
			}
			else if (userDetails.status == 0)
            {
				return new Tokens { Token = "inactive" };
            }

			// Else we generate JSON Web Token
			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
			  {
			 new Claim(ClaimTypes.Name, users.username)
			  }),
				Expires = DateTime.UtcNow.AddMinutes(10),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return new Tokens { Token = tokenHandler.WriteToken(token) };

		}

    }
}
