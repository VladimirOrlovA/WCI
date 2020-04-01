using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCI.BLL
{
    // Weather Data Transfer Object 
    public class WeatherDTO : ModelDTO
    {
        // город
        public string City { get; set; }

        // текущее время
        public DateTime currentDate { get; set; }

        // информация о сроке прогнозирования
        public List<ForecastDTO> forecastsDTO = new List<ForecastDTO>();

        public class ForecastDTO
        {
            // дата прогноза
            public string Date { get; set; }

            // день недели
            public string DayOfWeek { get; set; }

            // время суток
            public string TimesOfDay { get; set; }

            //заблаговременность прогноза в часах
            public string Predict { get; set; }

            // атмосферные явления
            public Phenomena Phenomena { get; set; }

            // атмосферное давление, в мм.рт.ст.
            public string Pressure { get; set; }

            //температура воздуха, в градусах Цельсия
            public string Temperature { get; set; }

            // приземный ветер
            public string Wind { get; set; }

            // относительная влажность воздуха, в %
            public string Relwet { get; set; }

            // комфорт - температура воздуха по ощущению одетого по сезону человека, выходящего на улицу
            public string Heat { get; set; }




            //public class Pressure
            //{
            //    //<PRESSURE max = "770" min="768"/>
            //    public string Max { get; set; }
            //    public string Min { get; set; }

            //    public override string ToString()
            //    {
            //        string str = $"aтмосферное давление от {Min} до {Max} мм.рт.ст.";
            //        return str;
            //    }
            //}

            //public class Temperature
            //{
            //    //<TEMPERATURE max = "2" min="1"/>
            //    public string Max { get; set; }
            //    public string Min { get; set; }

            //    public override string ToString()
            //    {
            //        string str = $"температура воздуха от {Min} до {Max} C";
            //        return str;
            //    }
            //}

            //public class Wind
            //{
            //    //<WIND min = "0" max="0" direction="3"/>
            //    public string Max { get; set; }
            //    public string Min { get; set; }
            //    public string Direction { get; set; }

            //    public override string ToString()
            //    {
            //        //WeatherItemDescription description = new WeatherItemDescription();
            //        string directionToWord = "ЮЗ";
            //        //description.direction.TryGetValue(Direction, out directionToWord);
            //        string str = $"ветер скорость {Min}-{Max} м/с, направление {directionToWord}";
            //        return str;
            //    }
            //}

            //public class Relwet
            //{
            //    //<RELWET max = "73" min="67"/>
            //    public string Max { get; set; }
            //    public string Min { get; set; }

            //    public override string ToString()
            //    {
            //        string str = $@"относительная влажность воздуха {Min}-{Max} %";
            //        return str;
            //    }
            //}

            //public class Heat
            //{
            //    //<HEAT min = "-2" max="-2"/>
            //    public string Max { get; set; }
            //    public string Min { get; set; }

            //    public override string ToString()
            //    {
            //        string str = $"температура воздуха по ощущению одетого по сезону человека, выходящего на улицу от {Min} до {Max}";
            //        return str;
            //    }
            //}


        }
    }

    public class Phenomena
    {
        //облачность по градациям
        public string Cloudiness { get; set; }

        // тип осадков
        public string Precipitation { get; set; }

        // интенсивность осадков, если они есть.
        public string Rpower { get; set; }

        // вероятность грозы, если прогнозируется
        public string Spower { get; set; }
    }

}
