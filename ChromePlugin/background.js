chrome.runtime.onInstalled.addListener(() => {
    console.log("Extension installed.");

    var port = chrome.runtime.connectNative('com.example.vpn_checker');

    port.postMessage({ text: "Hello from Chrome Extension" });

    port.onMessage.addListener(function (response) {
        console.log("Received response from native app:", response);

        if (response && response.vpnStatus !== undefined) {
            if (response.vpnStatus) {
                console.log("VPN is ON. Blocking access.");
            } else {
                console.log("VPN is OFF. Allowing access.");
            }
        } else {
            console.error("Invalid response format:", response);
        }
    });

    port.onDisconnect.addListener(function () {
        if (chrome.runtime.lastError && chrome.runtime.lastError.message !== "Native host has exited.") {
            console.error("Error during connection:", chrome.runtime.lastError.message);
        } else {
            console.log("Native app disconnected. Expected behavior if the app finished its work.");
        }
    });
});