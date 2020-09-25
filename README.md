# parity-project-1

## Goal

The goal of this project is to determine how you approach solving data and code
problems. The project should not take more than four to eight hours to complete.

## Deliverables

We would like you to download some weather data (from individual weather
stations anywhere in the world - your choice - but must be from the same
country!) using a script you author. There must be data from at least
**10** locations that are all within the same country.

Take that downloaded data, transform it into a more simplified format,
and store it in a SQL table(s). How you decide to do that is up to you,
but be prepared to justify any data simplifications you make!

Additionally, create a new table that stores aggregations of the downloaded data, for
example, average rainfall or average humidity across the region of weather stations.

So in summary, at the very least you must provide:

1. Data download script
2. Data transformation script
3. SQL table(s) storing the transformed data
4. Data aggregation script
5. SQL table(s) storing the newly aggregated data

## Data source

Get free weather data using the [OpenWeather API](https://openweathermap.org/api).

We will provide you with an API key for use during this project.

Example API call:

```
http://api.openweathermap.org/data/2.5/weather?q=London,uk&APPID=<APIKEY>
```

This should give you a response similar to:

```
{
  "coord": {
    "lon":-0.13,
    "lat":51.51
  },
  "weather":[
    {
      "id":803,
      "main":"Clouds",
      "description":"broken clouds",
      "icon":"04d"
    }
  ],
  "base":"stations",
  "main":{
    "temp":286.33,
    "feels_like":278.88,
    "temp_min":285.37,
    "temp_max":287.04,
    "pressure":1007,
    "humidity":42
  },
  "visibility":10000,
  "wind":{
    "speed":7.93,
    "deg":314
  },
  "clouds":{"all":77},
  "dt":1601055181,
  "sys": {
    "type":3,
    "id":268730,
    "country":"GB",
    "sunrise":1601013115,
    "sunset":1601056333
  },
  "timezone":3600,
  "id":2643743,
  "name":"London",
  "cod":200
}
```

## Repository format

Pick your own repository format (directories/folders, download scripts, SQL tables)

## Rubric

We will score your project on the following criteria:

1. Organization
2. Readability
3. Scalability
4. Performance
