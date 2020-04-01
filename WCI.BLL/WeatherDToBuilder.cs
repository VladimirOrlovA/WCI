using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using WCI.DAL;

namespace WCI.BLL
{
    public class WeatherDToBuilder : ClassBuilder
    {
        public WeatherDToBuilder(string resourceAddress) : base(resourceAddress) { }

        public override ModelDTO Create()
        {
            string message;
            Weather weather = DataLoad.GetWeatherFromXML(ResourceAddress, out message);

            WeatherDTO weatherDTO = new WeatherDTO();

            weatherDTO.City = weather.Sname;
            weatherDTO.currentDate = DateTime.Now;

            // для получения описания полей класса модели из источника
            WeatherItemDescription description = new WeatherItemDescription();
            string value;

            foreach (Forecast forecast in weather.forecasts)
            {
                WeatherDTO.ForecastDTO item = new WeatherDTO.ForecastDTO();

                // дата прогноза
                DateTime dateTime = forecast.GetDateTime();
                item.Date = dateTime.ToString("dd MMMM yyyy", new CultureInfo("ru"));

                // день недели
                item.DayOfWeek = dateTime.ToString("dddd", new CultureInfo("ru"));

                // время суток 
                description.tod.TryGetValue(forecast.Tod, out value);
                item.TimesOfDay = value;

                //заблаговременность прогноза в часах
                item.Predict = forecast.Predict;

                // атмосферные явления
                Phenomena phenomena = new Phenomena();

                // атмосферные явления -  облачность
                description.cloudiness.TryGetValue(forecast.phenomena.Cloudiness, out value);
                phenomena.Cloudiness = value;

                // атмосферные явления -  тип осадков
                description.precipitation.TryGetValue(forecast.phenomena.Precipitation, out value);
                phenomena.Precipitation = value;

                // атмосферные явления - если они есть.
                if(forecast.phenomena.Precipitation != "10")
                {
                    // интенсивность осадков.
                    description.rpower.TryGetValue(forecast.phenomena.Rpower, out value);
                    phenomena.Rpower = value;

                    // вероятность грозы.
                    description.rpower.TryGetValue(forecast.phenomena.Rpower, out value);
                    phenomena.Rpower = value;
                }

                item.Phenomena = phenomena;

                string pressureStr = forecast.pressure.ToString();
                item.Pressure = pressureStr.Substring(12);
                item.Temperature = forecast.temperature.Min + " - " + forecast.temperature.Max + "C";
                item.Wind = "ветер " + forecast.wind.Min + "-" + forecast.wind.Max + " м/с" + forecast.wind.Direction;
                item.Relwet = "влажность " + forecast.relwet.Min + "-" + forecast.relwet.Max + " %";
                item.Heat = forecast.heat.Min + " - " + forecast.heat.Max + "C";


                weatherDTO.forecastsDTO.Add(item);

            }


            return weatherDTO;
        }
    }
}
