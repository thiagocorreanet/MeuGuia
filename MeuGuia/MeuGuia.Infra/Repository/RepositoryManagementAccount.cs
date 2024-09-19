using MeuGuia.Domain.Entitie;
using MeuGuia.Domain.Interface;
using MeuGuia.Domain.JWT;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MeuGuia.Infra.Repository;

public class RepositoryManagementAccount : IRepositoryManagementAccount
{
    readonly UserManager<IdentityUserCustom> _userManager;
    readonly SignInManager<IdentityUserCustom> _signInManager;
    readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _iConfiguration;
    private readonly JsonWebToken _jsonWebToken;

    public RepositoryManagementAccount(UserManager<IdentityUserCustom> userManager, SignInManager<IdentityUserCustom> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration iConfiguration, IOptions<JsonWebToken> jsonWebToken)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _iConfiguration = iConfiguration;
        _jsonWebToken = jsonWebToken.Value;
    }

    /// <summary>
    /// Autentica um usuário de forma assíncrona com base no nome de usuário e senha fornecidos.
    /// </summary>
    /// <param name="username">O nome de usuário ou e-mail do usuário a ser autenticado.</param>
    /// <param name="password">A senha do usuário a ser autenticado.</param>
    /// <returns>
    /// Uma tarefa que representa a operação assíncrona. O resultado da tarefa contém um valor booleano indicando se a autenticação foi bem-sucedida.
    /// </returns>
    /// <remarks>
    /// Este método primeiro tenta encontrar um usuário pelo e-mail usando o método <see cref="UserManager.FindByEmailAsync"/>.
    /// Se o usuário for encontrado, ele tenta fazer o login do usuário usando o método <see cref="SignInManager.PasswordSignInAsync"/>.
    /// </remarks>
    public async Task<LoginUser> AuthenticateAsync(string username, string password)
    {

        var userFindByEmail = await _userManager.FindByEmailAsync(username);

        if (userFindByEmail is null) return null;

        var result = await _signInManager.PasswordSignInAsync(username, password, false, false);

        if (!result.Succeeded)
            return null;

        return await GenerateJwtToken(username);
    }

    /// <summary>
    /// Registra um usuário de forma assíncrona com base nos dados fornecidos.
    /// </summary>
    /// <param name="user">O objeto <see cref="UserIdentity"/> que contém os dados do usuário a ser registrado.</param>
    /// <returns>
    /// Uma tarefa que representa a operação assíncrona. O resultado da tarefa contém um valor booleano indicando se o registro foi bem-sucedido.
    /// </returns>
    /// <remarks>
    /// Este método cria um novo usuário usando o método <see cref="UserManager.CreateAsync"/> com uma senha padrão.
    /// Se a criação for bem-sucedida, o usuário é automaticamente logado usando o método <see cref="SignInManager.SignInAsync"/>.
    /// Além disso, uma permissão é atribuída ao usuário com base no seu nome de usuário.
    /// </remarks>
    public async Task<bool> RegisterUserAsync(IdentityUserCustom user)
    {
        string standardPassword = "Mudar@123456789";
        user.UserName = user.Email;

        var result = await _userManager.CreateAsync(user, standardPassword);

        if (!result.Succeeded)
        {
            return false;
        }
        else
        {
            await _signInManager.SignInAsync(user, false);
        }

        return result.Succeeded;
    }

    /// <summary>
    /// Faz o logout de um usuário de forma assíncrona.
    /// </summary>
    /// <returns>
    /// Uma tarefa que representa a operação assíncrona de logout.
    /// </returns>
    /// <remarks>
    /// Este método utiliza o <see cref="SignInManager.SignOutAsync"/> para realizar o logout do usuário atual.
    /// </remarks>
    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<LoginUser> GenerateJwtToken(string userName)
    {
        var user = await _userManager.FindByEmailAsync(userName);
        var claims = await _userManager.GetClaimsAsync(user);
        var userRoles = await _userManager.GetRolesAsync(user);

        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
        foreach (var role in userRoles)
            claims.Add(new Claim("role", role));

        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jsonWebToken.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = identityClaims,
            Issuer = _jsonWebToken.Issuer,
            Audience = _jsonWebToken.ValidIn,
            Expires = DateTime.UtcNow.AddHours(_jsonWebToken.ExpirationHours),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var encodedToken = tokenHandler.WriteToken(token);

        var response = new LoginUser
        {
            AccessToken = encodedToken,
            ExpiresIn = TimeSpan.FromHours(_jsonWebToken.ExpirationHours).TotalMicroseconds,
            UserToken = new UserToken
            {
                Id = user.Id,
                Email = user.Email,
                Claims = claims.Select(x => new ClaimUser { Type = x.Type, Value = x.Value }),

            }
        };

        return response;
    }

    private static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalMicroseconds);
}
