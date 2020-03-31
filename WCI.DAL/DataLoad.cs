using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Globalization;
using System.Web;

namespace WCI.DAL
{
    public class DataLoad
    {
        public static XmlDocument FromXML(string resourceAddress, out string message)
        {
            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                xmlDoc.Load(resourceAddress);
                message = $"Data successfully loaded \n from {resourceAddress} \n to XMLdoc";
                return xmlDoc;
            }
            catch (Exception eXML)
            {
                message = eXML.Message;
                return xmlDoc;
            }
        }

        public static Currency GetCurrencyFromXML(string resourceAddress, out string message)
        {
            XmlDocument xmlDocument = FromXML(resourceAddress, out message);

            Currency currency = new Currency();

            XmlElement rss = xmlDocument.DocumentElement;

            foreach (XmlNode rssChild in rss)
                foreach (XmlNode channelChild in rssChild)
                {
                    if (channelChild.Name == "title") currency.Title = channelChild.InnerText;
                    if (channelChild.Name == "link") currency.Link = channelChild.InnerText;
                    if (channelChild.Name == "item")
                    {
                        Item item = new Item();

                        foreach (XmlNode itemChild in channelChild)
                        {
                            if (itemChild.Name == "title") item.Title = itemChild.InnerText;
                            if (itemChild.Name == "pubDate") item.PubDate = Convert.ToDateTime(itemChild.InnerText, new CultureInfo("ru-RU"));
                            if (itemChild.Name == "description") item.Description = Convert.ToDouble(itemChild.InnerText, new CultureInfo("en-US"));
                            if (itemChild.Name == "quant") item.Quant = Convert.ToInt32(itemChild.InnerText);
                            if (itemChild.Name == "index") item.Index = itemChild.InnerText;
                            if (itemChild.Name == "change") item.Change = itemChild.InnerText;
                        }
                        currency.items.Add(item);
                    }
                }
            //   < title > AUD </ title >
            //   < pubDate > 27.03.20 </ pubDate >
            //   < description > 266.16 </ description >
            //   < quant > 1 </ quant >
            //   < index > UP </ index >
            //   < change > +3.33 </ change >
            //   < link />
            return currency;
        }

        public static Weather GetWeatherFromXML(string resourceAddress, out string message)
        {
            XmlDocument xmlDocument = FromXML(resourceAddress, out message);

            Weather weather = new Weather();

            XmlElement mmWeather = xmlDocument.DocumentElement;

            foreach (XmlNode report in mmWeather)
                foreach (XmlNode town in report)
                {
                    weather.Town = Convert.ToInt32(town.Attributes.GetNamedItem("index").Value);

                    string encode = HttpUtility.UrlDecode(town.Attributes.GetNamedItem("sname").Value);
                    weather.Sname = encode;

                    weather.Latitude = Convert.ToInt32(town.Attributes.GetNamedItem("latitude").Value);
                    weather.Longitude = Convert.ToInt32(town.Attributes.GetNamedItem("longitude").Value);

                    foreach (XmlNode forecast in town)
                    {
                        Forecast forecastC = new Forecast();

                        forecastC.Day = forecast.Attributes.GetNamedItem("day").Value;
                        forecastC.Month = forecast.Attributes.GetNamedItem("month").Value;
                        forecastC.Year = forecast.Attributes.GetNamedItem("year").Value;
                        forecastC.Hour = forecast.Attributes.GetNamedItem("hour").Value;
                        forecastC.Tod = forecast.Attributes.GetNamedItem("tod").Value;
                        forecastC.Predict = forecast.Attributes.GetNamedItem("predict").Value;
                        forecastC.Weekday = forecast.Attributes.GetNamedItem("weekday").Value;

                        //< FORECAST day = "27" month = "03" year = "2020" hour = "03" tod = "0" predict = "0" weekday = "6" >

                        foreach (XmlNode item in forecast)
                        {
                            if (item.Name == "PHENOMENA")
                            {
                                Phenomena phenomena = new Phenomena();
                                phenomena.Cloudiness = item.Attributes.GetNamedItem("cloudiness").Value;
                                phenomena.Precipitation = item.Attributes.GetNamedItem("precipitation").Value;
                                phenomena.Rpower = item.Attributes.GetNamedItem("rpower").Value;
                                phenomena.Spower = item.Attributes.GetNamedItem("spower").Value;

                                forecastC.phenomena = phenomena;

                                //<PHENOMENA cloudiness = "1" precipitation="10" rpower="0" spower="0"/>
                            }

                            if (item.Name == "PRESSURE")
                            {
                                Pressure pressure = new Pressure();
                                pressure.Max = item.Attributes.GetNamedItem("max").Value;
                                pressure.Min = item.Attributes.GetNamedItem("min").Value;

                                forecastC.pressure = pressure;

                                //<PRESSURE max = "770" min="768"/>
                            }

                            if (item.Name == "TEMPERATURE")
                            {
                                Temperature temperature = new Temperature();
                                temperature.Max = item.Attributes.GetNamedItem("max").Value;
                                temperature.Min = item.Attributes.GetNamedItem("min").Value;

                                forecastC.temperature = temperature;

                                //<TEMPERATURE max = "2" min="1"/>
                            }

                            if (item.Name == "WIND")
                            {
                                Wind wind = new Wind();
                                wind.Max = item.Attributes.GetNamedItem("max").Value;
                                wind.Min = item.Attributes.GetNamedItem("min").Value;
                                wind.Direction = item.Attributes.GetNamedItem("direction").Value;

                                forecastC.wind = wind;

                                //<WIND min = "0" max="0" direction="3"/>
                            }

                            if (item.Name == "RELWET")
                            {
                                Relwet relwet = new Relwet();
                                relwet.Max = item.Attributes.GetNamedItem("max").Value;
                                relwet.Min = item.Attributes.GetNamedItem("min").Value;

                                forecastC.relwet = relwet;

                                //<RELWET max = "73" min="67"/>
                            }

                            if (item.Name == "HEAT")
                            {
                                Heat heat = new Heat();
                                heat.Max = item.Attributes.GetNamedItem("max").Value;
                                heat.Min = item.Attributes.GetNamedItem("min").Value;

                                forecastC.heat = heat;

                                //<HEAT min = "-2" max="-2"/>
                            }
                        }

                        weather.forecasts.Add(forecastC);
                    }
                }


            return weather;
        }
    }
}
