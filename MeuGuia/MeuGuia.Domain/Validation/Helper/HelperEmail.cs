using System.Net;

namespace MeuGuia.Domain.Validation.Helper;

public static class HelperEmail
{
    public static bool ValidateEmail(string email)
    {
        if (!IsValidEmailFormat(email))
            return false;

        string domain = GetDomainFromEmail(email);

        // Verifica se o domínio possui registros MX
        return DomainHasMXRecords(domain);
    }

    private static bool IsValidEmailFormat(string email)
    {
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return System.Text.RegularExpressions.Regex.IsMatch(email, pattern);
    }

    private static string GetDomainFromEmail(string email)
    {
        int atIndex = email.IndexOf('@');
        return email.Substring(atIndex + 1);
    }

    private static bool DomainHasMXRecords(string domain)
    {
        try
        {
            IPHostEntry hostEntry = Dns.GetHostEntry(domain);
            return hostEntry != null && hostEntry.AddressList.Length > 0;
        }
        catch (System.Net.Sockets.SocketException)
        {
            // O domínio não possui registros MX
            return false;
        }
    }
}
