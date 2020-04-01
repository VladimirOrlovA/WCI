using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WCI.BLL;

namespace WCI.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FillWeatherBlock();
        }

        public void FillWeatherBlock()
        {
            Controller controller = new Controller();

            WeatherDTO weatherDTO = controller.GetWeatherDTO();


            int count = 0;
            foreach (var elem in weatherDTO.forecastsDTO)
            {
                WeatherBlock weatherBlock = new WeatherBlock();
                weatherBlock.DataContext = elem;

                Grid.SetColumn(weatherBlock, count);
                gridWeatherBlocks.Children.Add(weatherBlock);
                count++;
            }
        }
    }
}
