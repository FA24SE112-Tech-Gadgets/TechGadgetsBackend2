using System.Net;
using System.Net.Sockets;

namespace WebApi.Common.Utils;

public static class UtilityExtensions
{
    public static Guid ConvertToGuid(this string @this)
    {
        if (!Guid.TryParse(@this, out var result))
        {
            throw new ArgumentException("The string is not a valid Guid", @this);
        }
        return result;
    }
    public static string GetIpAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());

        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                Console.WriteLine(ip.ToString());
                return ip.ToString();
            }
        }

        return "127.0.0.1";
    }
}
