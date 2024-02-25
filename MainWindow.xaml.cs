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
using System.Diagnostics;

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
        private delegate void UpdateProgressBarDelegate(
        System.Windows.DependencyProperty dp, Object value);
        List<songinformationen> songlisteReezy = new List<songinformationen>();
        List<songinformationen> songlisteTravis = new List<songinformationen>();
        List<songinformationen> lieblingssongs = new List<songinformationen>();
        List<songinformationen> songs = new List<songinformationen>();

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
                songlisteReezy.Add(new songinformationen(Convert.ToInt32(reader[0]), reader[1].ToString(), reader[2].ToString(),
                                                    reader[3].ToString(), Convert.ToDateTime(reader[4]), Convert.ToInt32(reader[5]),
                                                    reader[6].ToString()));
            }

            for (int i = 0; i < songlisteReezy.Count; i++)
            {
                Lb_AusgabeReezy.Items.Add(songlisteReezy[i].TiteldesSongs + " ~ " + songlisteReezy[i].Künstler);
                Lb_AusgabeReezy.FontSize = 24;
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
            PB_SongsReezy.Visibility = Visibility.Visible;
            Btn_ProcessReezy.Visibility = Visibility.Visible;

            TitelAlbumReezy.Text = songlisteReezy[0].Albumname;
            TitelAlbumReezy.Text.ToUpper();
            KünstlerNameAlbumReezy.Text = "- " + songlisteReezy[0].Künstler;
            
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
            Img_TravisScott.Visibility = Visibility.Visible;

            TitelAlbumTravisScott.Text = songlisteTravis[0].Albumname;
            TitelAlbumTravisScott.Text.ToUpper();
            KünstlerNameAlbumTravisScott.Text = "- " + songlisteTravis[0].Künstler ;
        }

        private void Lb_AusgabeReezy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        public void MusikAbspielenReezy(int x)
        {
            SoundPlayer player = new SoundPlayer(songlisteReezy[x].Song);
            player.Play();
        }

        public void MusikAbspielenTravis(int x)
        {
            SoundPlayer player = new SoundPlayer(songlisteTravis[x].Song);
            player.Play();
        }

        private void Canvas_Loaded(object sender, RoutedEventArgs e)
        {
            string connectionString = "datasource = 127.0.0.1; port = 3306; username = root; password = root; database = it-woche-2024";

            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("SELECT * from songinformationentravis", conn);

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                songlisteTravis.Add(new songinformationen(Convert.ToInt32(reader[0]), reader[1].ToString(), reader[2].ToString(),
                                                    reader[3].ToString(), Convert.ToDateTime(reader[4]), Convert.ToInt32(reader[5]),
                                                    reader[6].ToString()));
            }

            for (int i = 0; i < songlisteTravis.Count; i++)
            {
                Lb_AusgabeTravis.Items.Add(songlisteTravis[i].TiteldesSongs + " ~ " + songlisteTravis[i].Künstler);
                Lb_AusgabeTravis.FontSize = 24;
            }
        }

        private void Lb_AusgabeTravis_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            
        }

        private void Process(ProgressBar PB)
        {


            PB.Minimum = 0;

            PB.Maximum = short.MaxValue;

            PB.Value = 0;

            
            double value = 0;

            
            UpdateProgressBarDelegate updatePbDelegate =
                new UpdateProgressBarDelegate(PB.SetValue);

           
            do
            {
                value += 0.2;

                Dispatcher.Invoke(updatePbDelegate,
                    System.Windows.Threading.DispatcherPriority.Background,
                    new object[] { ProgressBar.ValueProperty, value });
            }
            while (PB.Value != PB.Maximum);


            
        }

        private void Btn_ProcessReezy_Click(object sender, RoutedEventArgs e)
        {
            MusikAbspielenReezy(Lb_AusgabeReezy.SelectedIndex);
            PB_SongsReezy.Maximum = songlisteReezy[Lb_AusgabeReezy.SelectedIndex].DauerSEK;
            //MessageBox.Show(PB_SongsReezy.Maximum.ToString());
            int x = songlisteReezy[Lb_AusgabeReezy.SelectedIndex].DauerSEK;
            //MessageBox.Show(x.ToString());
            
            Process(PB_SongsReezy);

         
        }

        

        private void Btn_ProcessTravis_Click(object sender, RoutedEventArgs e)
        {
            MusikAbspielenTravis(Lb_AusgabeTravis.SelectedIndex);
            PB_SongsTravis.Maximum = songlisteTravis[Lb_AusgabeTravis.SelectedIndex].DauerSEK;
            //MessageBox.Show(PB_SongsTravis.Maximum.ToString());
            int x = songlisteTravis[Lb_AusgabeTravis.SelectedIndex].DauerSEK;
            //MessageBox.Show(x.ToString());

            Process(PB_SongsTravis);
        }

        private void Img_Herz_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            // Den Pfad zum neuen Bild festlegen
            string newImagePath = "heart_filled_icon_125259.png";

            // Das Bild ändern
            ChangeImage(newImagePath);


            string connectionString = "datasource = 127.0.0.1; port = 3306; username = root; password = root; database = it-woche-2024";

            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

           

           

            MySqlConnection cmd = new MySqlConnection("Insert Into lieblingssongs Values (@Titel des Songs,@Künstler/Interpret,@Albumname,@Erscheinungsjahr,@Dauer,@Song)");


            // Füge Parameter hinzu und setze ihre Werte
           
            cmd.Parameters.AddWithValue("@Titel", songlisteReezy[Lb_AusgabeReezy.SelectedIndex].TiteldesSongs);
            cmd.Parameters.AddWithValue("@Interpret", songlisteReezy[Lb_AusgabeReezy.SelectedIndex].Künstler);
            cmd.Parameters.AddWithValue("@Albumname", songlisteReezy[Lb_AusgabeReezy.SelectedIndex].Albumname);
            cmd.Parameters.AddWithValue("@Erscheinungsjahr", songlisteReezy[Lb_AusgabeReezy.SelectedIndex].Erscheinungsjahr);
            cmd.Parameters.AddWithValue("@Dauer", songlisteReezy[Lb_AusgabeReezy.SelectedIndex].DauerSEK);
            cmd.Parameters.AddWithValue("@Song", songlisteReezy[Lb_AusgabeReezy.SelectedIndex].Song);


           
            
            conn.Close();






        }

        private void ChangeImage(string imagePath)
        {
            try
            {
                // Load the image from file
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagePath, UriKind.Relative);
                bitmap.EndInit();

                // Set the image control's source to the loaded image
                Img_Herz.Source = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading image: " + ex.Message);
            }
        }

       
    }
}
