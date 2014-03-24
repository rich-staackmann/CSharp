using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    //our facade to make things simple
    //the GetTemperature method encapsulates lots of calls to several APIs
    //this make things simpler for someone trying to calculate the temperature
    public class Facade
    {
        readonly WeatherService weatherService;
        readonly GeoLookupService geoLookupService;
        readonly EnglishMetricConverter englishMatricConverter;

        public Facade()
        {
            this.weatherService = new WeatherService();
            this.geoLookupService = new GeoLookupService();
            this.englishMatricConverter = new EnglishMetricConverter();
        }

        public LocalTemperature GetTemperature(int zipcode)
        {
            var coords = geoLookupService.GetCoordinatesForZipCode(zipcode);
            var city = geoLookupService.GetCityForZipCode(zipcode);
            var state = geoLookupService.GetStateForZipCode(zipcode);

            var fahrenheit = weatherService.GetTempFahrenheit(0, 0);
            var celsius = englishMatricConverter.FahrenheitToCelsius(fahrenheit);

            var localTemp = new LocalTemperature() 
            { 
                Fahrenheit = fahrenheit, 
                Celsius = celsius,
                City = city,
                State = state
            };

            return localTemp;
        }
    }
    //random model we need
    public class LocalTemperature
    {
        public double Fahrenheit { get; set; }
        public double Celsius { get; set; }
        public string Coords { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
    //----------------------------------------------------------------------------------------
    //stub classes to represent the complex API we are using
    public class GeoLookupService
    {
        public string GetCityForZipCode(int zipcode)
        {
            return "Lake Zurich";
        }
        public string GetStateForZipCode(int zipcode)
        {
            return "Illinois";
        }
        public string GetCoordinatesForZipCode(int zipcode)
        {
            return "23N 74W";
        }
    }
    public class WeatherService
    {
        public double GetTempFahrenheit(int lat, int longitude)
        {
            return 32.0;
        }
    }
    public class EnglishMetricConverter
    {
        public double FahrenheitToCelsius(double temp)
        {
            return 0.0;
        }
    }
}
