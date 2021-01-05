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
    /// 
    /// </summary>
    ///
    //Stoped at adding Role Icons
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        ToolTip tooltip = new ToolTip();

        //static string plattform = "pc";
        //static string region = "us";
        //static string name = "GamersCCCP";
        //static string tag = "1569";

        static string plattform = "pc";
        static string region = "eu";
        static string name = "Stephan";
        static string tag = "22420";

        //static string plattform = "pc";
        //static string region = "eu";
        //static string name = "hoani";
        //static string tag = "21496";

        //static string plattform = "pc";
        //static string region = "eu";
        //static string name = "Björn1545";
        //static string tag = "2833";

        //###
        //static string plattform = "pc";
        //static string region = "eu";
        //static string name = "schwertluchs";
        //static string tag = "2748";

        //###
        //static string plattform = "pc";
        //static string region = "eu";
        //static string name = "t00thless";
        //static string tag = "21703";

        //static string url = $"https://best-overwatch-api.herokuapp.com/player/{plattform}/{region}/{name}-{tag}";
        //static string url = $"https://ow-api.com/v1/stats/{plattform}/{region}/{name}-{tag}/profile";
        static string url = $"https://ow-api.com/v1/stats/{plattform}/{region}/{name}-{tag}/complete";

        JObject json = Get(url);
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            json = Get(url);
            icon.Source = new BitmapImage(new Uri(json["icon"].ToString()));
            try
            {
                stars.Source = new BitmapImage(new Uri(json["prestigeIcon"].ToString()));
            }
            catch (Exception ex)
            {
                stars.Source = null;
            }
            
            cb_changeModi.SelectedIndex = 0;
            lb_name.Content = json["name"].ToString().Split("#")[0];
            lb_tag.Content = "#"+json["name"].ToString().Split("#")[1];
            lb_level.Content = Convert.ToInt32(json["level"])+(Convert.ToInt32(json["prestige"])*100);
            lb_won.Content = json["quickPlayStats"]["games"]["won"];
            lb_lose.Content = Math.Abs(Convert.ToInt32(json["quickPlayStats"]["games"]["played"].ToString()) - Convert.ToInt32(lb_won.Content.ToString()));
            lb_total.Content = json["quickPlayStats"]["games"]["played"];
            lb_winRate.Content = Math.Round(((Convert.ToDouble(lb_won.Content.ToString()) / (Convert.ToDouble(json["quickPlayStats"]["games"]["played"].ToString()))) * 100), 2) + "%";
            lb_kills.Content = json["quickPlayStats"]["careerStats"]["allHeroes"]["average"]["eliminationsAvgPer10Min"];
            lb_damage.Content = json["quickPlayStats"]["careerStats"]["allHeroes"]["average"]["allDamageDoneAvgPer10Min"];
            lb_death.Content = json["quickPlayStats"]["careerStats"]["allHeroes"]["average"]["deathsAvgPer10Min"];
            lb_heal.Content = json["quickPlayStats"]["careerStats"]["allHeroes"]["average"]["healingDoneAvgPer10Min"];
            lb_soloKills.Content = json["quickPlayStats"]["careerStats"]["allHeroes"]["average"]["soloKillsAvgPer10Min"];
       
            //tank_icon.Source = new BitmapImage(new Uri(json["ratings"][0]["roleIcon"].ToString()));
            tank_icon.Source = new BitmapImage(new Uri(" https://static.playoverwatch.com/img/pages/career/icon-tank-8a52daaf01.png"));
        }

        public static JObject Get(string uri)
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window_Loaded(null,null);
        }

        private void changeModi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cb_changeModi.SelectedIndex == 0)
                {
                    lb_won.Content = json["quickPlayStats"]["games"]["won"];
                    lb_lose.Content = Math.Abs(Convert.ToInt32(json["quickPlayStats"]["games"]["played"].ToString()) - Convert.ToInt32(lb_won.Content.ToString()));
                    lb_total.Content = json["quickPlayStats"]["games"]["played"];
                    lb_winRate.Content = Math.Round(((Convert.ToDouble(lb_won.Content.ToString()) / (Convert.ToDouble(json["quickPlayStats"]["games"]["played"].ToString()))) * 100), 2) + "%";
                    lb_kills.Content = json["quickPlayStats"]["careerStats"]["allHeroes"]["average"]["eliminationsAvgPer10Min"];
                    lb_damage.Content = json["quickPlayStats"]["careerStats"]["allHeroes"]["average"]["allDamageDoneAvgPer10Min"];
                    lb_death.Content = json["quickPlayStats"]["careerStats"]["allHeroes"]["average"]["deathsAvgPer10Min"];
                    lb_heal.Content = json["quickPlayStats"]["careerStats"]["allHeroes"]["average"]["healingDoneAvgPer10Min"];
                    lb_soloKills.Content = json["quickPlayStats"]["careerStats"]["allHeroes"]["average"]["soloKillsAvgPer10Min"];
                }
                else
                {
                    lb_won.Content = json["competitiveStats"]["games"]["won"];
                    lb_lose.Content = Math.Abs(Convert.ToInt32(json["competitiveStats"]["games"]["played"].ToString()) - Convert.ToInt32(lb_won.Content.ToString()));
                    lb_total.Content = json["competitiveStats"]["games"]["played"];
                    lb_winRate.Content = Math.Round(((Convert.ToDouble(lb_won.Content.ToString()) / (Convert.ToDouble(json["competitiveStats"]["games"]["played"].ToString()))) * 100), 2) + "%";
                    lb_kills.Content = json["competitiveStats"]["careerStats"]["allHeroes"]["average"]["eliminationsAvgPer10Min"];
                    lb_damage.Content = json["competitiveStats"]["careerStats"]["allHeroes"]["average"]["allDamageDoneAvgPer10Min"];
                    lb_death.Content = json["competitiveStats"]["careerStats"]["allHeroes"]["average"]["deathsAvgPer10Min"];
                    lb_heal.Content = json["competitiveStats"]["careerStats"]["allHeroes"]["average"]["healingDoneAvgPer10Min"];
                    lb_soloKills.Content = json["competitiveStats"]["careerStats"]["allHeroes"]["average"]["soloKillsAvgPer10Min"];
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Nutzer hat seine Einstellungen auf 'Privat' oder Nur für 'Freunde'");
            }
        }
    }
}
