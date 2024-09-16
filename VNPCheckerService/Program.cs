using VNPCheckerService;

if (VPNCheckerService.IsVpnConnected())
{
    Console.WriteLine("VPN connected");
    Environment.Exit(0);
}
else
{
    Console.WriteLine("No VPN!!!");
    Environment.Exit(1);
}