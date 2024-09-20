using MeuGuia.Domain.Entitie;
using MeuGuia.Domain.Interface;
using MeuGuia.Domain.JWT;
using MeuGuia.Infra.Context;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
    protected MeuGuiaContext _context;
    private readonly ILogger<RepositoryManagementAccount> _logger;

    public RepositoryManagementAccount(UserManager<IdentityUserCustom> userManager, SignInManager<IdentityUserCustom> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration iConfiguration, IOptions<JsonWebToken> jsonWebToken, MeuGuiaContext context, ILogger<RepositoryManagementAccount> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _iConfiguration = iConfiguration;
        _jsonWebToken = jsonWebToken.Value;
        _context = context;
        _logger = logger;
    }

    public async Task<LoginUser> AuthenticateAsync(string username, string password)
    {

        var userFindByEmail = await _userManager.FindByEmailAsync(username);

        if (userFindByEmail is null) return null;

        var result = await _signInManager.PasswordSignInAsync(username, password, false, false);

        if (!result.Succeeded)
            return null;

        return await GenerateJwtToken(username);
    }

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

    public async Task<IdentityUserClaimCustom?> RegisterUserClimAsync(string email, string claimType, string claimValue)
    {
        var user = await _userManager.FindByEmailAsync(email);
        IdentityUserClaimCustom? getUserClaim = new IdentityUserClaimCustom();

        if (user is null)
        {
            return null;
        }
        else
        {
            var claim = new Claim(claimType, claimValue);
            var result = await _userManager.AddClaimAsync(user, claim);

            if (result.Succeeded)
            {
                getUserClaim = _context.UserClaims.FirstOrDefault(x => x.UserId == user.Id && x.ClaimType == claim.Type && x.ClaimValue == claim.Value);
                getUserClaim.ClaimType = claim.Type;
                getUserClaim.ClaimValue = claimValue;
            }

            return getUserClaim;
        }

    }

    public async Task<IdentityUserClaimCustom?> UpdateUserClimAsync(int id, string claimType, string claimValue)
    {
        try
        {
            var claim = await _context.UserClaims.FirstOrDefaultAsync(x => x.Id == id);
            if (claim is not null)
            {
                claim.ClaimType = claimType;
                claim.ClaimValue = claimValue;

                await _context.SaveChangesAsync(); 
                return claim;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Não foi possível atualizar a claim: {ex.Message}");
            return null;
        }
    }

    private static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalMicroseconds);
}
