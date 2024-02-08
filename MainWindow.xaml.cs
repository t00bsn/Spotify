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
        private DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            
        }

       
       List<songinformationen> songliste = new List<songinformationen>();
        

       

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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
                Lb_Ausgabe.Items.Add(songliste[i].TiteldesSongs + " " + songliste[i].Künstler);
            }

           
            
            


            reader.Close();
            conn.Close();
            
            
            

        }

        private void SekundenBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
        }

        private void Btn_Tiger_Click(object sender, RoutedEventArgs e)
        {
            //TIGER
            MusikAbspielen(0);
            timer.Start();

            SekundenBar.Minimum = 0;
            SekundenBar.Maximum = songliste[0].DauerSEK; 
            SekundenBar.Value = 0;



            



        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Btn_ONE_NIGHT_IN_PARIS_DEEP_DOWN_Click(object sender, RoutedEventArgs e)
        {
            //ONE NIGHT IN PARIS/DEEP DOWN
            MusikAbspielen(1);
        }


        public void MusikAbspielen(int x)
        {
            SoundPlayer player = new SoundPlayer(songliste[x].Song);
            player.Play();
        }

        private void Btn_DRIPPED_OUT_IN_DESIGNER_Click(object sender, RoutedEventArgs e)
        {
            //DRIPPED OUT IN DESIGNER
            MusikAbspielen(2);
        }

        private void Btn_STEADY_Click(object sender, RoutedEventArgs e)
        {
            //STEADY
            MusikAbspielen(3);
        }

        
    }
}
