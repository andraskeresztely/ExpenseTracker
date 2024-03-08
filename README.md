![Workflow status badge](https://github.com/andraskeresztely/ExpenseTracker/actions/workflows/expense-tracker.yml/badge.svg) [![Mutation testing badge](https://img.shields.io/endpoint?style=plastic&url=https%3A%2F%2Fbadge-api.stryker-mutator.io%2Fgithub.com%2Fandraskeresztely%2FExpenseTracker%2Fmain)](https://dashboard.stryker-mutator.io/reports/github.com/andraskeresztely/ExpenseTracker/main)

# Expense Tracker

Simple .NET web application for tracking expenses.

## Projects in the solution

1. Persistence (`ExpenseTracker.Persistence.LiteDb`)  
The application is using [LiteDb](https://www.litedb.org/) , a lightweight file-based document database, for persistence. It is configured to create the database files under `ExpenseTracker.Web.Api/Db`, this however can be configured in `ExpenseTracker.Web.Api/appsettings.json`.

2. API (`ExpenseTracker.Web.Api`)  
The API is a .NET 8 Web API. When debugging, it is supposed to start up in IIS Express, using the URL `https://localhost:44320`. This is configurable in `launchSettings.json`, however the configuration of the frontend, in `ExpenseTracker.Web.App.Blazor/wwwroot/appsettings.json`, needs to follow this setting. Logs can be found under `ExpenseTracker.Web.Api/Logs`.

3. UI (`ExpenseTracker.Web.App.Blazor`)  
The frontend is a Blazor WebAssembly app. When debugging, it should start up in IIS Express, using the URL `https://localhost:44367`. This is configurable in `launchSettings.json`, however the configuration of the API, in `ExpenseTracker.Web.Api/appsettings.json`, needs to follow this setting (for CORS reasons).

4. ViewModel (`ExpenseTracker.Web.Model`)  
Contains the viewmodel which facilitates data exchange between the API and the UI.

5. Domain (`ExpenseTracker.Domain`)  
Contains domain models, logic and validation.
