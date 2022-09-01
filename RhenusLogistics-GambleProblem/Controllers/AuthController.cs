using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using GamblingGame.Models.DTO;
using GamblingGame.Service;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GamblingGame.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    protected ResponseDto _response;
    private IGambleService _service;
    private JwtSettings _jwtSettings;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IGambleService service, IOptions<JwtSettings> options, ILogger<AuthController> logger)
    {
        _service = service;
        _jwtSettings = options.Value;
        _logger = logger;
        _response = new ResponseDto();
    }
    /// <summary>
    /// Authenticate user by verifying login credentials
    /// </summary>
    /// <param name="loginInfo"></param>
    /// <returns>JWT token</returns>
    [HttpPost]
    public async Task<object> Authenticate([FromBody]Login loginInfo)
    {
        try
        {
            UsersDto usersDto = await _service.AuthenticateUser(loginInfo);
            
            if (usersDto == null)
                return Unauthorized();
            _logger.LogInformation("User authentication successful");
            _logger.LogDebug("User: " + loginInfo.UserName);

            //Generate token
            _logger.LogInformation("Generate token");
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[] { new Claim(ClaimTypes.Name, usersDto.Username) }
                    ),
                Expires = DateTime.Now.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256)

            };
            var generateToken = tokenHandler.CreateToken(tokenDescriptor);
            _logger.LogInformation("Token created");
            var token = tokenHandler.WriteToken(generateToken);
            return Ok(token);
        }
        catch (Exception ex)
        {
            _logger.LogError(exception: ex,"Exception occured in Authentication");
            _response.IsSuccess = false;
            _response.ErrorMessage = new List<string> { ex.Message };
        }
        return _response;
        
    }
}

