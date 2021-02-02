# Weather ETL
This project downloads current weather information from the OpenWeather API and transforms it into SQL data.

[[_TOC_]]

## User Scripts
- Download weather data files to disk:
```bash
./local/download.sh
```

- Import weather data files to SQL:
```bash
./local/import.sh
```

- Aggregate weather data in SQL:
```bash
./local/import.sh
```

## Developer notes
To run this project locally:
* Setup a SQL Server instance to connect to or use docker `./local/sqlServerDocker.sh
* Connect to SQL Server to create tables from files in the `sql` folder, see section below for ordering
 * Install .NET 5.0 if not installed
 * Set/Configure environment variables (section below)
 * Run from the command line using dotnet run or configure your IDE
 * Entry point is project ParityFactory.Weather
 * Program.cs is expecting an argument (download,import,aggregate)
 
### Environment variables
* DATA_DIRECTORY
  * Directory to save files to
* WEATHER_API_ENDPOINT
  * API endpoint to call
* WEATHER_API_ENDPOINT_KEY
  * API key to use
* MAX_CONCURRENCY
  * Maximum concurrency for download from weather API
* DB_CONNECTION
  * Database connection string

## Architecture
![Architecture Diagram](APIGatewayLambdaAuthArchitecture.png)

### Explanation
1. Client makes a request with an Authorization Bearer token.
1. AWS API Gateway invokes ApiGatewayLambdaAuthorizer for protected routes
1. ApiGatewayLambdaAuthorizer:
   - Verifies the JWT signature based on issuer public key
     - Public keys are loaded into memory on application startup.  
     - Lambda is typically kept warm based on volume.
   - Pulls user data from Redis or OnFarm (ext.B2CUserProfile, dbo.tSECUsers) based on issuer
   - Caches response to Redis if pulled from OnFarm
   - Builds Policy (explicit allow)
   - Exception("Unauthorized") if failures (validating token, etc.) results in 401 unauthorized to client
1. Response/requested resource is evaluated against policy:
   - Allowed = request forwarded to destination lambda, response sent back to client
   - Denied = 403/forbidden sent back to client

## Tokens/Claims

## Execute unit tests
- Using local dotnet SDK (note you will need to set environment variables, see above):
```
dotnet test \
    /p:CollectCoverage=true \
    /p:Threshold=80 \
    /p:CoverletOutputFormat=lcov \
    /p:CoverletOutput="../../lcov.info"
```

- Using a script:
```bash
./local/run_unit_tests.sh
```

- To view the line coverage report in html, you'll need `lcov`:
```bash
./local/gen_html.sh
```
