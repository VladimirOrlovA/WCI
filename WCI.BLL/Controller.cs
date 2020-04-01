using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WCI.DAL;

namespace WCI.BLL
{
    public class Controller
    {

        public WeatherDTO GetWeatherDTO()
        {
            ClassBuilder classBuilder = new WeatherDToBuilder("https://xml.meteoservice.ru/export/gismeteo/point/1.xml");

            WeatherDTO weatherDTO = (WeatherDTO)classBuilder.Create();

            return weatherDTO;
        }
    }
}
