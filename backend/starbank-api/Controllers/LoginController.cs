
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using starbank_api.Domain.Models;
using starbank_api.Domain.Services;

namespace starbank_api.Domain.Models;


[ApiController]
[Route("v1/customer")]
public class LoginController : ControllerBase
{
    private readonly IAutenticacaoService _autenticacaoService;
    private readonly TokenServices _tokenServices;
    // private readonly SignInManager<IdentityUser> _signInManager;
    // SignInManager<IdentityUser> signInManager

    public LoginController(IAutenticacaoService autenticacaoService, TokenServices tokenServices)
    {
        _autenticacaoService = autenticacaoService;
        _tokenServices = tokenServices;
        // _signInManager = signInManager;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginModel loginModel)
    {
        var customer = await _autenticacaoService.AutenticarCustomer(loginModel.Email, loginModel.Password);

        if (customer == null)
        {
            return Unauthorized("Email e/ou senha incorretos.");
        }

        var token = _tokenServices.GenerateToken(customer);

        return Ok(new { message = "Login bem-sucedido.", token = token });
    }


    // [HttpPost("logout")]
    // public async Task<IActionResult> Logout()
    // {
    //     await _signInManager.SignOutAsync();
    //     return RedirectToAction("Index", "Home");
    // }

    // [HttpPost("login")]
    // public async Task<IActionResult> Login([FromBody] LoginDto login)
    // {
    //     if (string.IsNullOrEmpty(login.CpfCnpj) || string.IsNullOrEmpty(login.Password))
    //     {
    //         return BadRequest("CPF/CNPJ e senha precisam ser informados");
    //     }

    //     var user = await _userService.GetUserByLoginAsync(login.CpfCnpj, login.Password);
    //     if (user == null)
    //     {
    //         return BadRequest("CPF/CNPJ ou senha inválidos");
    //     }

    //     var token = _tokenServices.GenerateTokenJwt(user);

    //     return Ok(new { token });
    // }



    // private string GenerateToken(Customer customer)
    // {
    //     {
    //         var tokenHandler = new JwtSecurityTokenHandler();
    //         var key = Encoding.ASCII.GetBytes(_appSettings["Jwt:Key"]);
    //         var tokenDescriptor = new SecurityTokenDescriptor
    //         {
    //             Subject = new ClaimsIdentity(new[]
    //             {
    //         new Claim(JwtRegisteredClaimNames.Sub, customer.Name),
    //         new Claim(JwtRegisteredClaimNames.Email, customer.Email),
    //         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    //     }),
    //             Expires = DateTime.UtcNow.AddHours(2),
    //             Issuer = _appSettings["Jwt:Issuer"],
    //             Audience = _appSettings["Jwt:Audience"],
    //             SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
    //         };
    //         var token = tokenHandler.CreateToken(tokenDescriptor);
    //         return tokenHandler.WriteToken(token);
    //     }
    // }




}
