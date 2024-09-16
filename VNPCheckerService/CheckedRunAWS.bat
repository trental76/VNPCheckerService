@echo off
VNPCheckerService.exe

IF %ERRORLEVEL% EQU 0 (
    echo VPN Connected, starting AWS Workspace
    start "Start AWS Workspace..." "C:\Program Files\Amazon Web Services, Inc\Amazon WorkSpaces\workspaces.exe"
) ELSE (
    echo VPN NOT CONNECTED!
    pause
)

exit