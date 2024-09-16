document.addEventListener('DOMContentLoaded', function () {
    const vpnStatusEl = document.getElementById('vpnStatus');
    const toggleVpnButton = document.getElementById('toggleVpn');

    chrome.storage.local.get(['vpnStatus'], function (result) {
        vpnStatusEl.textContent = result.vpnStatus ? "VPN enabled" : "VPN disabled";
    });

    toggleVpnButton.addEventListener('click', function () {
        chrome.storage.local.get(['vpnStatus'], function (result) {
            const newStatus = !result.vpnStatus;
            chrome.storage.local.set({ vpnStatus: newStatus }, function () {
                vpnStatusEl.textContent = newStatus ? "VPN enabled" : "VPN disabled";
            });
        });
    });
});
