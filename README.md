# Goal
This project intent is for me to learn about a lot of aspects I didn't learn about in company like :
- DDD patterns usage
- deployment with GitHub Action
- configuring Azure environment from start
- having quick and maintainable acceptance tests
- ...

# Disclaimer
I leverage the time spent on tasks that don't help me to learn, so that I can focus on what I don't know

This has led this project to seem overengineered in some aspects and underengineered in others, which is intended

# Subject
A French loan application journey in a company that delivers credit to consumers.

![Consumer credit](Event-storming.png)
 
# Pre-requisite
- Run "install-dependencies.ps1" in powershell **as administrator** to install
  - .Net 8
  - Docker Desktop
  - Latest nswag version
  - Certificates for front end and backend

# API and tests launch pre-requisite
- Run Docker Desktop

# Environments configuration
By default, everything run locally

Even if there is a production configuration that is used to deploy on Azure, it is not supposed to be used by another people than me