@ECHO OFF
set WEB_ROOT=%systemdrive%\_publishedemailtokenservice
set APPCMD=%systemroot%\system32\inetsrv\appcmd
echo %WEB_ROOT%
echo "skipping draining connections and wait for sometime...."
echo "stop app pool...."
%APPCMD% stop apppool /apppool.name:DefaultAppPool
echo "deleting existing service ..."
rd %WEB_ROOT% /S /Q
set /p DUMMY=Hit ENTER to continue...
echo "publishing service ..."
xcopy /I /F "..\emailtokenservice\bin\*.dll" "%WEB_ROOT%\bin\" /Y || goto Error
xcopy /I /F "..\emailtokenservice\web.config" "%WEB_ROOT%\" /Y || goto Error
xcopy /I /F "..\emailtokenservice\packages.config" "%WEB_ROOT%\" /Y || goto Error
xcopy /I /F "..\emailtokenservice\Global.asax" "%WEB_ROOT%\" /Y || goto Error

echo "deleting existing web app ..."
%APPCMD% delete site "emailtokenservice"
set /p DUMMY=Hit ENTER to continue...
echo "hosting service..."
%APPCMD% add site /name:emailtokenservice /id:4932 /physicalPath:%WEB_ROOT% /bindings:http/*:80:
set /p DUMMY=Hit ENTER to continue...
%APPCMD% start apppool /apppool.name:DefaultAppPool

goto Success
:Error
echo Site was not published 
 
goto End
 
:Success
echo Site published successfully

:End 


