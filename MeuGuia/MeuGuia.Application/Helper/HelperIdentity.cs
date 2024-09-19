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

    public async Task<IdentityUserCustom?> GetLoggedInUser()
    {
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

        return user;
    }
}
