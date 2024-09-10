# Goal
Experiment (meaning that it is not about doing a perfect project but more a learning project) :
- Event Storming (alone...)
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

# API launch pre-requisite
- Run Docker Desktop
- Run "launch database.bat"

# Tests launch pre-requisite
- Run Docker Desktop