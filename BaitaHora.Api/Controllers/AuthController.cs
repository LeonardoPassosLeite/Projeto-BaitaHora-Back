using BaitaHora.Application.IServices.Auth;
using Microsoft.AspNetCore.Mvc;
using BaitaHora.Application.DTOs.Auth.Requests;
using BaitaHora.Application.DTOs.Auth;

namespace BaitaHora.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(ITokenService tokenService, IAuthService authService, ILogger<AuthController> logger)
    {
        _tokenService = tokenService;
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new RegisterResponse("Dados inválidos."));

        try
        {
            var result = await _authService.RegisterAsync(request);

            if (!result.IsSuccess)
                return BadRequest(new RegisterResponse(result.Error ?? "Erro desconhecido"));

            return Ok(new RegisterResponse("Usuário registrado com sucesso"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao registrar usuário");
            return StatusCode(500, new RegisterResponse("Erro interno ao registrar usuário."));
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new LoginResponse("Dados inválidos", string.Empty));

        try
        {
            var result = await _authService.AuthenticateAsync(request);

            if (!result.IsSuccess || result.Value is null)
                return Unauthorized(new LoginResponse(result.Error ?? "Usuário ou senha inválidos", string.Empty));

            var token = _tokenService.GenerateToken(result.Value);
            return Ok(new LoginResponse("Login efetuado com sucesso", token));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao fazer login");
            return StatusCode(500, new LoginResponse("Erro interno ao autenticar", string.Empty));
        }
    }
}
