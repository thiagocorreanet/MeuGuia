using MeuGuia.Domain.Entitie;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace MeuGuia.Application.Helper;

public class HelperIdentity
{
    readonly UserManager<IdentityUserCustom> _userManager;
    readonly IHttpContextAccessor _httpContextAccessor;

    public HelperIdentity(UserManager<IdentityUserCustom> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Méotodo responsável por obter dados do usuário logado.
    /// </summary>
    /// <returns>Retorna os dados do usuário.</returns>
    public async Task<IdentityUserCustom?> GetLoggedInUser()
    {
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

        return user;
    }

    public async Task<IdentityUserCustom?> GetUserByEmail(string email)
    {
        var userByEmail = await _userManager.FindByEmailAsync(email);

        return userByEmail;
    }
}
