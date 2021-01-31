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
  * Maximum concurrency

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
