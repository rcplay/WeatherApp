﻿using System.Linq;
using System.Text.Json;
using WeatherApp.Core.Models.OpenWeatherMap;

namespace WeatherApp.Core.Models
{
    public class Weather
    {
        public string CityName { get; }

        public string ConditionIcon { get; }

        public string ConditionIconUrl => $"https://openweathermap.org/img/w/{ConditionIcon}.png";

        public string Condition { get; }

        public decimal Temperature { get; }

        public Weather(string cityName, string condition, string conditionIcon, decimal temperature)
            => (CityName, ConditionIcon, Condition, Temperature) = (cityName, conditionIcon, condition, temperature);

        public Weather(string openWeatherMapResponse)
        {
            using var jsonDocument = JsonDocument.Parse(openWeatherMapResponse);
            var jsonRootElement = jsonDocument.RootElement;
            var weatherElement = jsonRootElement.GetProperty("weather")[0];

            CityName = jsonRootElement.GetProperty("name").ToString();
            Condition = weatherElement.GetProperty("description").ToString();
            ConditionIcon = weatherElement.GetProperty("icon").ToString();
            Temperature = jsonRootElement.GetProperty("main").GetProperty("temp").GetDecimal();
        }

        public Weather(CurrentWeather weather)
        {
            CityName = weather.Name;
            Condition = weather.Conditions.First().Condition;
            ConditionIcon = weather.Conditions.First().ConditionIcon;
            Temperature = weather.Detail.Temperature;
        }
    }
}
