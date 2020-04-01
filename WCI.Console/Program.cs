using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Globalization;
using System.Threading;
using WCI.DAL;
using WCI.BLL;

namespace WCI.ConsoleApp
{
    class Program
    {
        enum DayOfWeekRU { Воскресенье, Понедельник, Вторник, Среда, Четверг, Пятница, Суббота };
        static string message = null;
        static int messNumber = 0;

        static void ShowMessage()
        {
            string separatorLine = new string('=', 90);
            Console.WriteLine($"--{++messNumber}--");
            Console.WriteLine(message);
            Console.WriteLine(separatorLine);
        }


        static void Main(string[] args)
        {

            #region test without pattern Factory Method
            /*
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");

            #region test weather block

            Currency currency = DataLoad.GetCurrencyFromXML("https://nationalbank.kz/rss/rates_all.xml?switch=russian", out message);
            ShowMessage();

            Weather weather = DataLoad.GetWeatherFromXML("https://xml.meteoservice.ru/export/gismeteo/point/1.xml", out message);
            ShowMessage();

            WeatherItemDescription description = new WeatherItemDescription();

            string strTod;
            string strCloudiness;
            string strPrecipitation;
            string strRpower;
            string strSpower;
            string strDirection;

            Console.WriteLine($"\nПрогноз погоды в городе {weather.Sname}");
            Console.WriteLine("По данным от " + DateTime.Now.ToString("F", new CultureInfo("ru")));
            Console.WriteLine(new string('=', 90));

            foreach (Forecast forecast in weather.forecasts)
            {
                description.tod.TryGetValue(forecast.Tod, out strTod);
                description.cloudiness.TryGetValue(forecast.phenomena.Cloudiness, out strCloudiness);
                description.precipitation.TryGetValue(forecast.phenomena.Precipitation, out strPrecipitation);
                description.rpower.TryGetValue(forecast.phenomena.Rpower, out strRpower);
                description.spower.TryGetValue(forecast.phenomena.Spower, out strSpower);
                description.direction.TryGetValue(forecast.wind.Direction, out strDirection);

                //CultureInfo.GetCultureInfo("ru-RU");
                Enum.GetName(typeof(DayOfWeek), Convert.ToInt32(forecast.Weekday));

                Console.WriteLine(
                    forecast.GetDateTime().ToShortDateString() + "\n" +
                    //forecast.GetDateTime().DayOfWeek + "\n" +
                    Enum.GetName(typeof(DayOfWeekRU), Convert.ToInt32(forecast.Weekday) - 1) + "\n" +
                    strTod + "\n" +
                    strCloudiness + "\n" +
                    strPrecipitation + "\n" +
                    //strRpower + "\n" +
                    //strSpower + "\n" +
                    //strDirection + " " + forecast.wind.Min + " - " + forecast.wind.Max + " м/с" + "\n" +
                    forecast.wind.ToString() + "\n" +
                    forecast.temperature.ToString() + "\n" +
                    forecast.heat.ToString() + "\n" +
                    forecast.relwet.ToString() + "\n");

                Console.WriteLine(new string('=', 90));
            }

            #endregion test weather block

            #region test currency block

            Console.WriteLine($"Курс валюты НБРК на {currency.items[0].PubDate.ToString("dd MMMM yyyy", new CultureInfo("ru"))}");

            var usd = currency.items.Find((item) => item.Title == "USD");
            var eur = currency.items.Find((item) => item.Title == "EUR");

            Console.WriteLine($"USD {usd.Description} динамика {usd.Change}");
            Console.WriteLine($"EUR {eur.Description} динамика {eur.Change}");

            Console.WriteLine(new string('=', 90));

            #endregion test currency block

            */
            #endregion test without pattern Factory Method

            //=================================================================//

            #region test with patterns - Factory Method  and  MVC model

            ClassBuilder classBuilder = new WeatherDToBuilder("https://xml.meteoservice.ru/export/gismeteo/point/1.xml");
            WeatherDTO weatherDTO = (WeatherDTO)classBuilder.Create();

            Console.WriteLine($"\nПрогноз погоды в городе {weatherDTO.City}");
            Console.WriteLine("По данным от " + weatherDTO.currentDate.ToString("F", new CultureInfo("ru")));
            Console.WriteLine(new string('=', 90));

            foreach(var forecast in weatherDTO.forecastsDTO)
            {
                Console.WriteLine(forecast.Date);
                Console.WriteLine(forecast.TimesOfDay);
                Console.WriteLine(forecast.Temperature);
                Console.WriteLine(forecast.Heat);
                Console.WriteLine(forecast.phenomena.Cloudiness);
                Console.WriteLine(forecast.phenomena.Precipitation);
                Console.WriteLine(forecast.phenomena.Rpower);
                Console.WriteLine(forecast.phenomena.Spower);
                Console.WriteLine(forecast.Wind);
                Console.WriteLine(forecast.Relwet);
                Console.WriteLine(forecast.Pressure);

                Console.WriteLine("\n" + new string('=', 90));
            }

            classBuilder = new CurrencyDToBuilder("https://nationalbank.kz/rss/rates_all.xml?switch=russian");
            CurrencyDTO currencyDTO = (CurrencyDTO)classBuilder.Create();

            Console.WriteLine($"Курс валюты НБРК на {currencyDTO.items[0].PubDate.ToString("dd MMMM yyyy", new CultureInfo("ru"))}");

            var usd = currencyDTO.items.Find((item) => item.Title == "USD");
            var eur = currencyDTO.items.Find((item) => item.Title == "EUR");

            Console.WriteLine($"USD {usd.Description} динамика {usd.Change}");
            Console.WriteLine($"EUR {eur.Description} динамика {eur.Change}");

            Console.WriteLine(new string('=', 90));

            #endregion test with patterns - Factory Method  and  MVC model 

            Console.ReadKey();
        }
    }
}
