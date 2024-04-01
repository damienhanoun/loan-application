# Goal
Experiment :
- Event Storming
- DDD :
  - Domain event / Integration event
  - Entity
- Tools :
    - Mediator: a MediatR-like library that runs at build time
    - SpecFlow: for acceptance tests
    - Minimal API: and find a way to split each route into a separate file
    - Mapperly: a mapping library that runs at build time, which prevents adding unit tests for mapping and can be inlined
    - Docker: to mount a fake database for integration and acceptance tests
    - Entity Framework: to manage entities

# Subject

A French loan application journey in a company that delivers credit to consumers.

![Consumer credit](Event-storming.png)

 
# Pre-requisite
- .Net 8
- Docker
- PostgreSQL with `postgres`/`password` identifiers + creation of `acquisition` database
- From the `ConsumerCredit` folder: ```dotnet ef database update --project Acquisition.Infrastructure --startup-project Acquisition.Api```

# Todo
- [ ] Find a way to configure acceptance tests
- [ ] Generate open api specification from code to allow to generate http request on demand
- [ ] Using application/problem+json as return value to have a standard error format
- [ ] Use OneOf to return many results from Application to web API layer and handle them to map to http status code
- [ ] Create an http client manually (not with NSwag for resilience purpose)
- [ ] Find a way to handle versioning of the API
