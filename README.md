# Goal
This project intent is for me to learn about :
- DDD common pattern and see relation with the database storage of entities
- Make a project ready to use almost already after downloading it
- Make an example of business tests first application where business use case are in the center of the application development

# Subject
A French loan application journey in a company that delivers credit to consumers.

![Consumer credit](Event-storming.png)
 
# Pre-requisite
- Run "install-dependencies.ps1" in powershell **as administrator** to install
  - .Net 8
  - Docker Desktop
  - Latest nswag version
  - Certificate required for front end in trusted certificates

# API and tests launch pre-requisite
- Run Docker Desktop

# Using Azure resources manually created
- Given an AppConfiguration with all expected configuration (See `appsettings.Development.json`)
  - In `Acquisition.Api` folder, after replacing each `<...>`, run : `dotnet user-secrets set "AppConfig:ConnectionString" "Endpoint=https://<app configuration path>.azconfig.io;Id=<your id>;Secret=<your secret>"`