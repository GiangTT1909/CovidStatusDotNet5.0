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
using System.Windows.Shapes;
using BusinessLayer;
using DataAccess;
using DataAccess.Model;
using DataAccess.API;

namespace Representaion
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        int selectedCountry;
        List<Object> ChartdetailsList;
        List<CountryPieDataDTO> Countries = new List<CountryPieDataDTO>();
        public Home()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataSumaryModel data = new DataSumaryModel();

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
                    Countries.Add(country);
                }
                CountryPieDataDTO global = new CountryPieDataDTO();
                global.ID = "0";
                global.country = "Global";
                global.CountryCode = "Global";
            global.TotalConfirmed = data.Global.TotalConfirmed;
            global.TotalDeaths = data.Global.TotalDeaths;
            global.TotalRecovered = data.Global.TotalRecovered;
            global.Date = data.Global.Date;
            Countries.Insert(0, global);
            ComboBoxCountries.ItemsSource = Countries;
            ChartdetailsList = new List<Object>();
            float deathPer = (float)global.TotalDeaths / (float)global.TotalConfirmed * 100;
            float recoverPer = (float)global.TotalRecovered / (float)global.TotalConfirmed * 100;
            if (recoverPer == 0) recoverPer = 0.001f;
            ChartdetailsList.Add(new { Description = "Recovery Rate", Value=recoverPer });
            ChartdetailsList.Add(new { Description = "Death Rate", Value=deathPer });
            ComboBoxCountries.SelectedIndex = 0;
            TextBlockConfirmNumbers.Text = global.TotalConfirmed.ToString();
            TextBlockRecoverNumbers.Text = global.TotalRecovered.ToString();
            TextBlockDeathNumbers.Text = global.TotalDeaths.ToString();
            chart.ItemsSource = ChartdetailsList;
            TextBlockDATE.Text = global.Date.ToLongDateString()+" "+ global.Date.ToShortTimeString();
        }

        private void  OuterGrid_Loaded(object sender, RoutedEventArgs e)
        {

          
        }

        private void ComboBoxCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CountryPieDataDTO selected = (CountryPieDataDTO)ComboBoxCountries.SelectedItem;
            ChartdetailsList = new List<Object>();
            float deathPer = (float)selected.TotalDeaths / (float)selected.TotalConfirmed * 100;
            float recoverPer = (float)selected.TotalRecovered / (float)selected.TotalConfirmed * 100;
            if (recoverPer == 0) recoverPer = 0.001f;
            ChartdetailsList.Add(new { Description = "Recovery Rate", Value = recoverPer });
            ChartdetailsList.Add(new { Description = "Death Rate", Value = deathPer });


            chart.ItemsSource = ChartdetailsList;
            TextBlockConfirmNumbers.Text = selected.TotalConfirmed.ToString();
            TextBlockRecoverNumbers.Text = selected.TotalRecovered.ToString();
            TextBlockDeathNumbers.Text = selected.TotalDeaths.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GraphView view = new GraphView();
            view.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Map view = new Map();
            view.Show();
            this.Close();
        }
    }
}
