# Papirus - Lawsuit Management Automation

Papirus is designed to revolutionize the management of lawsuits within law firms through the power of OCR and AI for operational efficiency.

## Technologies

- [ASP.NET Core 8](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core)
- [Entity Framework Core 8](https://docs.microsoft.com/en-us/ef/core/)
- [MediatR](https://github.com/jbogard/MediatR)
- [AutoMapper](https://automapper.org/)
- [FluentValidation](https://fluentvalidation.net/)
- [NUnit](https://nunit.org/), [FluentAssertions](https://fluentassertions.com/), [Moq](https://github.com/moq)

## Overview

### Domain

This section contains all entities, enums, exceptions, interfaces, types, and logic specific to the domain layer.

### Application

The application layer contains all application logic. It depends on the domain layer but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application needs to access a notification service, a new interface would be added to the application, and an implementation would be created within the infrastructure.

### Infrastructure

The infrastructure layer contains classes for accessing external resources such as file systems, web services, SMTP, and so on. These classes should be based on interfaces defined within the application layer.

### API

The API layer is an ASP.NET Web API Core 8. This layer depends on both the Application and Infrastructure layers. However, the dependency on Infrastructure is only to support dependency injection. Therefore, only *Program.cs* should reference Infrastructure.

This layer will be acting as backend exposing all services to clients.

## Health Check

The service includes a health check endpoint that can be accessed to monitor the status of the application. The specific URL for the health check will vary depending on the environment in which the service is deployed.

For example, in a local development environment, the health check might be available at: https://localhost:<port>/health

Replace `<port>` with the appropriate port number assigned during the local setup.

## Database

The project is configured to use SQL Server.

Running database migrations is easy. Ensure you add the following flags to your command (values assume you are executing from repository root)

- `--project src\WebApi\Infrastructure` (optional if in this folder)
- `--startup-project src\WebApi\API`
- `--output-dir Data/Migrations`

For example, to add a new migration from the root folder:

`dotnet ef migrations add "SampleMigration" --project src\WebApi\Infrastructure --startup-project src\WebApi\API --output-dir Data\Migrations`

To apply the migration from the root folder:

`dotnet ef database update --project src\WebApi\Infrastructure --startup-project src\WebApi\API`

For a detailed context on the project's architecture and design principles, please visit our [project's Wiki](https://dev.azure.com/arroyoconsultingco/Papirus/_wiki/wikis/Papirus.wiki/22/Papirus).

## Configuring Database Connections for CI/CD Deployment

For CI/CD processes, specifically within Azure Pipelines, Papirus leverages an idempotent SQL script approach for database migrations. This method ensures that database changes are applied reliably and consistently across all environments without duplicating efforts or causing errors due to already applied migrations.

### Azure Pipeline Configuration

To facilitate this, the pipeline is configured to dynamically set the database connection string for executing migrations. This is particularly important for deploying migrations in different environments (development, staging, production) where each environment might have its own dedicated database.

1. **Set up a Variable Group in Azure Pipelines**: Create a variable group that contains your environment-specific settings, including the database connection string. This allows for secure and centralized management of your deployment settings.

2. **Reference the Variable Group in Your Pipeline Definition**: In your `.yml`, reference the variable group to make the connection string available to tasks that require database access.

3. **Use the Connection String to Execute Migrations**: During the deployment stage, use the connection string to run the Entity Framework Core migrations. For an idempotent SQL script approach, you can use the `dotnet ef migrations script` command to generate a script that applies any pending migrations.

**Example**:

```yaml
steps:
- script: dotnet ef migrations script --idempotent --output $(Build.ArtifactStagingDirectory)/migrations.sql --project src/WebApi/Infrastructure --startup-project src/WebApi/Api
  displayName: 'Generate Migration Script'

- script: sqlcmd -S $(DatabaseServer) -d $(DatabaseName) -U $(DatabaseUsername) -P $(DatabasePassword) -i $(Build.ArtifactStagingDirectory)/migrations.sql
  displayName: 'Apply Migrations'
```

4. **Securely Handle Credentials**: Ensure that sensitive information, such as the database connection string or individual components like username and password, are stored securely in Azure Key Vault or as secret variables in Azure Pipelines and referenced appropriately in your pipeline definitions.

### Ensuring Idempotency

The idempotent script generated by Entity Framework Core ensures that migrations can be safely re-run without causing errors. This script checks the current state of the database and applies only the necessary changes, making it ideal for CI/CD pipelines where you want to automate deployment without manual intervention.

## Project Description

Papirus uses OCR and AI to automate the data extraction from documents, streamlining the generation of legal documents for lawsuits.