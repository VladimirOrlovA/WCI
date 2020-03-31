using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCI.DAL
{
    public class Weather
    {
        public int Town { get; set; }
        public string Sname { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        public List<Forecast> forecasts = new List<Forecast>();
    }

    public class Forecast
    {
        //<FORECAST day = "27" month="03" year="2020" hour="03" tod="0" predict="0" weekday="6">
        public string Day { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string Hour { get; set; }
        public string Tod { get; set; }
        public string Predict { get; set; }
        public string Weekday { get; set; }
        public Phenomena phenomena;
        public Pressure pressure;
        public Temperature temperature;
        public Wind wind;
        public Relwet relwet;
        public Heat heat;

        public DateTime GetDateTime()
        {
            DateTime forecastDayTime = new DateTime();
            DateTime.TryParse($"{Year}-{Month}-{Day}", out forecastDayTime);
            return forecastDayTime;
        }

        public class TimesOfDay
        {
            public string Number { get; set; }

            public override string ToString()
            {
                WeatherItemDescription description = new WeatherItemDescription();
                string value;
                description.tod.TryGetValue(Number, out value);
                return value;
            }
        }
    }

    public class TimesOfDay
    {
        public string Number { get; set; }

        public override string ToString()
        {
            WeatherItemDescription description = new WeatherItemDescription();
            string value;
            description.tod.TryGetValue(Number, out value);
            return value;
        }
    }

    public class Phenomena
    {
        //<PHENOMENA cloudiness = "1" precipitation="10" rpower="0" spower="0"/>
        public string Cloudiness { get; set; }
        public string Precipitation { get; set; }
        public string Rpower { get; set; }
        public string Spower { get; set; }
    }

    public class Pressure
    {
        //<PRESSURE max = "770" min="768"/>
        public string Max { get; set; }
        public string Min { get; set; }

        public override string ToString()
        {
            string str = $"aтмосферное давление от {Min} до {Max} мм.рт.ст.";
            return str;
        }
    }

    public class Temperature
    {
        //<TEMPERATURE max = "2" min="1"/>
        public string Max { get; set; }
        public string Min { get; set; }

        public override string ToString()
        {
            string str = $"температура воздуха от {Min} до {Max} C";
            return str;
        }
    }

    public class Wind
    {
        //<WIND min = "0" max="0" direction="3"/>
        public string Max { get; set; }
        public string Min { get; set; }
        public string Direction { get; set; }

        public override string ToString()
        {
            WeatherItemDescription description = new WeatherItemDescription();
            string directionToWord;
            description.direction.TryGetValue(Direction, out directionToWord);
            string str = $"ветер скорость {Min}-{Max} м/с, направление {directionToWord}";
            return str;
        }
    }

    public class Relwet
    {
        //<RELWET max = "73" min="67"/>
        public string Max { get; set; }
        public string Min { get; set; }

        public override string ToString()
        {
            string str = $@"относительная влажность воздуха {Min}-{Max} %";
            return str;
        }
    }

    public class Heat
    {
        //<HEAT min = "-2" max="-2"/>
        public string Max { get; set; }
        public string Min { get; set; } 

        public override string ToString()
        {
            string str = $"температура воздуха по ощущению одетого по сезону человека, выходящего на улицу от {Min} до {Max}";
            return str;
        }
    }

    public class WeatherItemDescription
    {
        //tod - время суток, для которого составлен прогноз: 0 - ночь 1 - утро, 2 - день, 3 - вечер
        public Dictionary<string, string> tod = new Dictionary<string, string>
        {
            {"0", "ночь"},
            {"1", "утро"},
            {"2", "день"},
            {"3", "вечер"},
        };


        #region PHENOMENA - атмосферные явления:

        //cloudiness - облачность по градациям: -1 - туман, 0 - ясно, 1 - малооблачно, 2 - облачно, 3 - пасмурно
        public Dictionary<string, string> cloudiness = new Dictionary<string, string>
        {
            {"-1", "туман" },
            {"0", "ясно" },
            {"1", "малооблачно" },
            {"2", "облачно" },
            {"3", "пасмурно" },
        };

        //precipitation - тип осадков: 3 - смешанные, 4 - дождь, 5 - ливень, 6,7 – снег, 8 - гроза, 
        //                              9 - нет данных, 10 - без осадков
        public Dictionary<string, string> precipitation = new Dictionary<string, string>
        {
            {"3", "смешанные" },
            {"4", "дождь" },
            {"5", "ливень" },
            {"6", "снег" },
            {"7", "снег" },
            {"8", "гроза" },
            {"9", "нет данных" },
            {"10", "без осадков" },
        };

        //rpower - интенсивность осадков, если они есть. 0 - возможен дождь/снег, 1 - дождь/снег
        public Dictionary<string, string> rpower = new Dictionary<string, string>
        {
            {"0", "возможен дождь/снег" },
            {"1", "дождь/снег" },
        };
        //spower - вероятность грозы, если прогнозируется: 0 - возможна гроза, 1 - гроза
        public Dictionary<string, string> spower = new Dictionary<string, string>
        {
            {"0", "возможна гроза" },
            {"1", "гроза" },
        };

        #endregion

        #region PRESSURE - атмосферное давление, в мм.рт.ст.
        #endregion

        #region TEMPERATURE - температура воздуха, в градусах Цельсия
        #endregion

        #region WIND - приземный ветер
        //min, max - минимальное и максимальное значения средней скорости ветра, без порывов(м/с)
        //direction - направление ветра в румбах, 0 - северный, 1 - северо-восточный, и т.д.
        public Dictionary<string, string> direction = new Dictionary<string, string>
        {
            {"0","северный" },
            {"1","северо-восточный" },
            {"2","восточный" },
            {"3","юго-восточный" },
            {"4","южный" },
            {"5","юго-западный" },
            {"6","западный" },
            {"7","северо-западный" },

            //{"0","северный" },
            //{"1","северо-восточный" },
            //{"2","" },
            //{"3","" },
            //{"4","" },
            //{"5","" },
            //{"6","" },
            //{"7","юго-западный"},
        };
        #endregion

        #region RELWET - относительная влажность воздуха, в %
        #endregion

        #region HEAT - комфорт - температура воздуха по ощущению одетого по сезону человека, выходящего на улицу
        #endregion
    }
}
