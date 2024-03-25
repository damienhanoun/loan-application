# loan-application

```
dotnet ef migrations add InitialCreate --project Acquisition.Infrastructure --startup-project Acquisition.Api
dotnet ef database update --project Acquisition.Infrastructure --startup-project Acquisition.Api
```


Todo
- Generate open api specification from code to allow to generate http request on demand
- to explore : application/problem+json