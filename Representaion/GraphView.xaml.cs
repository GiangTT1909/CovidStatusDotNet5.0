using BusinessLayer;
using DataAccess.API;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Representaion
{
    /// <summary>
    /// Interaction logic for Graph.xaml
    /// </summary>
    public partial class GraphView : Window
    {
        List<CountryPieDataDTO> Countries = new List<CountryPieDataDTO>();
        int interval { get; set; }
        public GraphView()
        {
            InitializeComponent();
            this.DataContext = this;
            
        }

        private void ComboBoxCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            DataSumaryModel data = new DataSumaryModel();
            List<CountryByDay> dataByDay = new List<CountryByDay>();
            dataByDay = await API.GetCountryByDayAsync("2020-01-20", "2022-03-27","vietnam");
            data = await API.GetCountryDataAsync();
            foreach (var item in data.Countries)
            {
                CountryPieDataDTO country = new CountryPieDataDTO();
                country.ID = item.ID;
                country.country = item.country;
                country.CountryCode = item.CountryCode;
                country.TotalConfirmed = item.TotalConfirmed;
                country.TotalDeaths = item.TotalDeaths;
                country.TotalRecovered = item.TotalRecovered;
                country.Date = item.Date;
                country.Slug = item.Slug;
                Countries.Add(country);
            }
           
          
            ComboBoxCountries.ItemsSource = Countries;
            ComboBoxCountries.SelectedIndex =190;

           
            
            List<KeyValuePair<DateTime, int>> dataGraph = new List<KeyValuePair<DateTime, int>>();
            foreach (var item in dataByDay) {
                dataGraph.Add(new KeyValuePair<DateTime, int>(item.Date, item.Cases));
            }
          
            
            ((LineSeries)MyChart.Series[0]).ItemsSource = dataGraph;

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string fromDate = From.SelectedDate.Value.ToShortDateString();
            string toDate = To.SelectedDate.Value.ToShortDateString();
            CountryPieDataDTO selected = (CountryPieDataDTO)ComboBoxCountries.SelectedItem;
            string name = selected.Slug;
            List<CountryByDay> dataByDay = new List<CountryByDay>();
            dataByDay = await API.GetCountryByDayAsync(fromDate, toDate, name);
            List<KeyValuePair<DateTime, int>> dataGraph = new List<KeyValuePair<DateTime, int>>();
            foreach (var item in dataByDay)
            {
                dataGraph.Add(new KeyValuePair<DateTime, int>(item.Date, item.Cases));
            }
            ((LineSeries)MyChart.Series[0]).ItemsSource = dataGraph;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Map mapView = new Map();
            mapView.Show();
            this.Close();
        }

        private void ButtonCovidDashBoard_Click(object sender, RoutedEventArgs e)
        {
            Home View = new Home();
            View.Show();
            this.Close();
        }
    }
}
