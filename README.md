## User Scripts
- Download weather data files to SQL:
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

## Environment variables
* DATA_DIRECTORY
  * Directory to save files to
* WEATHER_API_ENDPOINT
  * API endpoint to call
* WEATHER_API_ENDPOINT_KEY
  * API key to use
* MAX_CONCURRENCY
  * Maximum concurrency


## Local debugging


## Execute unit tests
- Using local dotnet SDK:
```bash
./local/run_unit_tests.sh
```

- To view the line coverage report in html, you'll need `lcov`:
```bash
./local/gen_html.sh
```
