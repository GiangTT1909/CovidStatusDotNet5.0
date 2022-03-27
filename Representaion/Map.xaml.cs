using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
using DataAccess.Model;
using DataAccess.API;
using BusinessLayer;
using GMap.NET.WindowsPresentation;
using GMap.NET;

namespace Representaion
{
    /// <summary>
    /// Interaction logic for Map.xaml
    /// </summary>
    public partial class Map : Window
    {
        List<CountryPieDataDTO> Countries = new List<CountryPieDataDTO>();
        public Map()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void ComboBoxCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            CountryPieDataDTO selected = (CountryPieDataDTO)ComboBoxCountries.SelectedItem;
            gMap.Position = new PointLatLng(selected.latitude, selected.longitude);
            gMap.Zoom =6;
            TextBlockConfirmNumbers.Text = selected.TotalConfirmed.ToString();
            TextBlockRecoverNumbers.Text = selected.TotalRecovered.ToString();
            TextBlockDeathNumbers.Text = selected.TotalDeaths.ToString();
        }

        private async  void gMap_Loaded(object sender, RoutedEventArgs e)
        {
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
           
            gMap.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            gMap.MinZoom = 2;
            gMap.MaxZoom = 17;
            
            gMap.Zoom = 2;
            
            gMap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
           
            gMap.CanDragMap = true;
           
            gMap.DragButton = MouseButton.Left;

            StreamReader r = new StreamReader(@"..//..//..//..//DataAccess//countries.json");
            string json = r.ReadToEnd();
            DataAccess.Model.Location items = JsonConvert.DeserializeObject<DataAccess.Model.Location>(json);
            DataSumaryModel data = new DataSumaryModel();
           
            data = await API.GetCountryDataAsync();
            foreach (var item in data.Countries)
            {
                CountryLocation cl = items.ref_country_codes.Find(i => String.Equals(i.alpha2, item.CountryCode));
                CountryPieDataDTO country = new CountryPieDataDTO();
                country.latitude = cl.latitude;
                country.longitude = cl.longitude;
                country.ID = item.ID;
                country.country = item.country;
                country.CountryCode = item.CountryCode;
                country.TotalConfirmed = item.TotalConfirmed;
                country.TotalDeaths = item.TotalDeaths;
                country.TotalRecovered = item.TotalRecovered;
                country.Date = item.Date;
                
                Countries.Add(country);
                GMapMarker marker = new GMapMarker(new PointLatLng(country.latitude,country.longitude));
                                   
                string ToolTipText = $"Country : {country.country}\nTotal Confirmed: {country.TotalConfirmed }\nTotal Death: {country.TotalDeaths}\nTotal Recovered: {country.TotalRecovered}";                 
                marker.Shape = new CustomMarkerRed(this,marker,ToolTipText);
                marker.Shape.Visibility = Visibility.Visible;
                
                
                gMap.Markers.Add(marker);


            }
            ComboBoxCountries.ItemsSource = Countries;
        }

        private void ButtonCovidDashBoard_Click(object sender, RoutedEventArgs e)
        {
            Home view = new Home();
            view.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GraphView view = new GraphView();
            view.Show();
            this.Close();
        }
    }
    }

