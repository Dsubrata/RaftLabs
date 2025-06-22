#How to run the application

1. Open Solution RaftLabs.Enterprise.sln using visual studio
2. Set RaftLabs.Enterprise.WebAPI project as startup
3. Hit F10 or run using visual studio run icon
4. Swagger will open RaftLabs Web API docs
5. Select desired API and hit "try it out"
6. Provide desired input/parameters and hit execute
7. Check/verify the API response data


#How to check Polly retry 

1. Edit appsettings.json/appsettings.Development.json by changing "ApiBaseUrl" values to wrong data/incorrect url
2. Save file & hit run
3. Check logs in "output" windows, API will be made 3 retry before failing
