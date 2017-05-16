using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using static Weather;

namespace DiMuro.Arthur._5i.WeatherApi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void BN_GO_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            string result = await client.GetStringAsync(new Uri(@"http://api.wunderground.com/api/3a78f2550e232a37/conditions/q/IT/" + TB_City.Text + ".json"));

            RootObject meteo = JsonConvert.DeserializeObject<RootObject>(result);

            try
            {
                TB_TemperatureC.Text = meteo.current_observation.temp_c.ToString();
                TB_TemperatureF.Text = meteo.current_observation.temp_f.ToString();
                TB_Humidity.Text = meteo.current_observation.relative_humidity.ToString();
                ImgMeteo.Source = new BitmapImage(new Uri(meteo.current_observation.icon_url));
                TB_VelocitàVento.Text = meteo.current_observation.wind_kph.ToString() + " Km/h";
                TB_Visibilità.Text = meteo.current_observation.visibility_km.ToString() + " Km";
                /*TB_Altitudine.Text = meteo.observation_location.elevation.ToString();
                TB_Latitudine.Text = meteo.observation_location.latitude.ToString();
                TB_Longitudine.Text = meteo.observation_location.longitude.ToString();*/
            }
            catch { MessageBox.Show("Non tutti i dati sono stati recuperati. Prova a correggere il nome della città e riprova"); }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
