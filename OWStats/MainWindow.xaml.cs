using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace OWStats
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

        static string plattform="pc";//pc
        static string region="eu";//us
        static string name="Stephan";//GamersCCCP
        static string tag="22420";//1569
        static string url = $"https://best-overwatch-api.herokuapp.com/player/{plattform}/{region}/{name}-{tag}";
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           JObject json=Get(url);
            
        }

        public JObject Get(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                JObject o = JObject.Parse(reader.ReadToEnd());
                return o;
            }
        }
    }
}
