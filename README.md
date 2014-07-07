CountdownScreensaver
====================

A simple countdown screensaver in Winodows 8.1 style optimized for use with Screensaver grace period. 

With correctly set environment the screensaver will count down seconds before workstation lock on all connected displays. Useful for mitigating the impact of setting a strict worstation lock policy to user experience. A recommended value of the grace period is 5 - 10 seconds, with 10 - 15 minutes of idle time before the lock occurs. 

Requirements:
- All power plans are set to sleep after more than idletime + grace period 
```
powercfg.exe /SETACVALUEINDEX SCHEME_ALL SUB_SLEEP STANDBYIDLE 930
powercfg.exe /SETDCVALUEINDEX SCHEME_ALL SUB_SLEEP STANDBYIDLE 930
```
- All power plans are set to turn display off after longer period of time than idletime + grace period
```
powercfg.exe /SETACVALUEINDEX SCHEME_ALL SUB_SLEEP STANDBYIDLE 930
powercfg.exe /SETDCVALUEINDEX SCHEME_ALL SUB_SLEEP STANDBYIDLE 930
```
- Grace period is set using registry key, or group policy preference sets the registry value
```
HKLM\Software\Microsoft\Windows NT\CurrentVersion\Winlogon\ScreenSaverGracePeriod : REG_SZ : 10
```
- Screensaver is set to the CountdownScreensaver.scr (build using x64 on 64-bit system, then rename the .exe to .scr)
