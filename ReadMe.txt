Readme for HEALTHFIRST EMAIL TOKEN SERVICE

Instructions for download, build and deploy- 

1) Open Admin command prompt, go to C:\ and run following command
git clone --recursive https://github.com/ParulManchanda/HealthFirst.git healthfirst
2) Go To C:\healthfirst
3) Open EmailTokenService.sln in visual studio 2013 in admin mode
4) Rebuild solution
5) After sln builds successfully, go to command prompt and run - cd C:\healthfirst\Deployment
6) Before deployment go to inetmgr and confirm there is no website that has binding to port 80. If there is one stop it. The deployment script doesn't handle this currently.
7) Run install.bat(Note you might see some errors but those will only happen when you are running for first time since you have no service to delete- ignore these as log as you see "Site published successfully" message at the end
8) See fiddler trace in "C:\healthfirst\sample" folder as reference for building your own request to email token service

Run unit test-

1) In visual studio where you have sln opened, right click EmailTokenService.Tests project and select "Run Unit Tests" 
2) All tests should pass.

Please contact blah@healthfirst.com for any issues you may encounter.