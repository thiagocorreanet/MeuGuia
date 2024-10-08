﻿using MeuGuia.Domain.Entitie;
using MeuGuia.Domain.JWT;

namespace MeuGuia.Domain.Interface;

public interface IRepositoryManagementAccount
{
    Task<LoginUser> AuthenticateAsync(string username, string password);
    Task<bool> RegisterUserAsync(IdentityUserCustom userIdentityCustom);
    Task Logout();
    Task<LoginUser> GenerateJwtToken(string userName);
}
