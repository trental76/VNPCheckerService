using System.Net.NetworkInformation;

namespace VNPCheckerService;

internal class VPNCheckerService
{
    private const string VPNConnectionName = "Cisco AnyConnect";

    public static bool IsVpnConnected()
    {
        NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

        return interfaces.Any(x =>
            x.OperationalStatus == OperationalStatus.Up
            && x.Description.StartsWith(VPNConnectionName));
    }
}
