using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStore.Controllers
{
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService userService;

        public AuthController(
             IUserService _userService,
            IConfiguration configuration)
        {
            _configuration = configuration;
            userService = _userService;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (!this.userService.TryGetUser(model.Username, model.Password, out var user))
            {
                return BadRequest("user not found");
            }

            var token = GenerateJwtToken(user?.Id ?? 0, user?.Role ?? string.Empty, model.Username);
            return Ok(new { token });
        }

        private string GenerateJwtToken(int userId, string role, string username)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // JWT ID
                new Claim(ClaimTypes.Name, username),
                new Claim("userId", userId.ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            var keyString = _configuration["Jwt:Key"];
            if (keyString == null || string.IsNullOrEmpty(keyString))
            {
                throw new Exception("jwt key must not be empty");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1), // 设置过期时间
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token); // 返回 JWT 字符串
        }
    }
}
