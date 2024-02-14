using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Media;
using System.IO;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Windows.Threading;
using System.Data;
using System.Timers;

namespace Spotify
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

        List<songinformationen> songliste = new List<songinformationen>();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Bei Programmstart wird der Bildschirm auf FullScreen gezogen
            Application.Current.MainWindow.WindowState = WindowState.Maximized;

            //Bei Programmstart steht oben links der Name des Users! FUNKTÌONIERT NOCH NICHT MUSS NOCH GEMERGED WERDEN
            //Lbl_WillkommenUser.Content = "Willkommen" + TB_BenutzerName.Text;

            //Labels von den Künstlern ausblenden
            Canvas_Reezy.Visibility = Visibility.Hidden;
            Canvas_Reezy.Visibility = Visibility.Hidden;

            string connectionString = "datasource = 127.0.0.1; port = 3306; username = root; password = root; database = it-woche-2024";

            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("SELECT * from songinformationen", conn);

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                songliste.Add(new songinformationen(Convert.ToInt32(reader[0]), reader[1].ToString(), reader[2].ToString(),
                                                    reader[3].ToString(), Convert.ToDateTime(reader[4]), Convert.ToInt32(reader[5]),
                                                    reader[6].ToString()));
            }

            for (int i = 0; i < songliste.Count; i++)
            {
                Lb_AusgabeReezy.Items.Add(songliste[i].TiteldesSongs + " ~ " + songliste[i].Künstler);
                Lb_AusgabeReezy.FontSize = 30;
            }

            reader.Close();
            conn.Close();


        }

        private void El_reezy_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Canvas_TravisScott.Visibility = Visibility.Hidden;
            Canvas_Reezy.Visibility = Visibility.Visible;
            Canvas_Startbildschirm.Visibility = Visibility.Hidden;
            Lb_AusgabeReezy.Visibility = Visibility.Visible;
            Lb_AusgabeTravis.Visibility = Visibility.Hidden;
            TitelAlbumReezy.Text = songliste[0].Albumname;
            TitelAlbumReezy.Text.ToUpper(); 
            
        }

        private void El_Profilbild_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Canvas_Reezy.Visibility = Visibility.Hidden;
            Canvas_TravisScott.Visibility = Visibility.Hidden;
            Canvas_Startbildschirm.Visibility = Visibility.Visible;
            

        }

        private void El_Travis_Scott_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Canvas_TravisScott.Visibility = Visibility.Visible;
            Canvas_Reezy.Visibility = Visibility.Hidden;
            Canvas_Startbildschirm.Visibility = Visibility.Hidden;
            Lb_AusgabeReezy.Visibility = Visibility.Hidden;
            Lb_AusgabeTravis.Visibility = Visibility.Visible;

        }

        private void Lb_AusgabeReezy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MusikAbspielen(Lb_AusgabeReezy.SelectedIndex);
        }

        public void MusikAbspielen(int x)
        {
            SoundPlayer player = new SoundPlayer(songliste[x].Song);
            player.Play();
        }
    }
}
